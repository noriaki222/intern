using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy_Spider : MonoBehaviour
{
    //通常弾のプレハブ入れる用
    [SerializeField] private GameObject Bullet;
    //追尾弾のプレハブ入れる用
    [SerializeField] private GameObject HomingBullet;
    //狙撃弾のプレハブ入れる用
    [SerializeField] private GameObject SnipingBullet;
    //罠弾のプレハブ入れる用
    [SerializeField] private GameObject TrapBullet;
    //火山のプレハブ入れる用
    [SerializeField] private GameObject Volcano;

    //通常弾の出現場所を格納する用
    [SerializeField] private Transform BulletPoint;
    //追尾弾の出現場所を格納する用
    [SerializeField] private Transform HomingPoint;
    //狙撃弾の出現場所を格納する用
    [SerializeField] private Transform SnipingPoint;
    //罠弾の出現場所を格納する用
    [SerializeField] private Transform TrapPoint;

    //各弾の発射に使うカウント
    private float Bulletcnt;
    private float HomingBulletcnt;
    private float SnipingBulletcnt;
    private float TrapBulletcnt;
    private float Volcanocnt;
    //各弾が一回出るのにかかる時間
    [SerializeField] private float BulletTiming = 3.0f;
    [SerializeField] private float HomingBulletTiming = 3.0f;
    [SerializeField] private float SnipingBulletTiming = 3.0f;
    [SerializeField] private float TrapBulletTiming = 3.0f;
    [SerializeField] private float VolcanoTiming = 3.0f;
    //各弾が出るか出ないかの確率
    private int BulletRnd;
    //狙撃対象（Player）の座標を入れる用
    [SerializeField] private GameObject PlayerPos;

    //ダメージを食らっているかどうかのフラグ
    private bool DamageFlag = false;
    //点滅用に点滅させたいオブジェクトを入れる
    [SerializeField] private SpriteRenderer sp;
    //敵のHPを管理しているUIを入れる用
    [SerializeField] private EnemyHpBar enemyHp;
    //ダメージを食らったとき、どれくらいHPが削れるかを入力
    [SerializeField] private float decreaseHp = 5.0f;
    //敵の現在のHPを格納する用
    [SerializeField] private float NowHP;
    //必殺技
    [SerializeField] private SpiderSpecialAttack SPattack;
    //必殺技フラグ（1回しか打たないようにするため）
    private bool SPFlag1 = false;
    private bool SPFlag2 = false;
    private bool SPFlag3 = false;

    //SE出す用
    AudioSource audioSource;
    [SerializeField] private AudioClip sound1;
    [SerializeField] private AudioClip sound2;
    //色々揺らす用
    [SerializeField] private Shake shake;
    //クリア演出用TimeLine
    [SerializeField] private TimelinePlayer ClearLine;
    //クリア演出は一度だけ
    private bool ClearFlag = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("DamageFlag" + DamageFlag);
        //今のHPを格納
        NowHP = enemyHp.GetNowHp();
        //ダメージ判定を受けているとき、点滅する
        if (DamageFlag)
        {
            //Mathf.Absは絶対値を返す、Mathf.Sinは＋なら１，－なら0を返す
            float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            //上の式で0と1が交互に来るので、それを透明度に入れて反転させている
            sp.color = new Color(1.0f, 0.0f, 0.0f, level);
        }
        if(NowHP > 70.0f)
        {
            Bulletcnt += Time.deltaTime;
            HomingBulletcnt += Time.deltaTime;
            //通常弾
            if(Bulletcnt>BulletTiming)
            {
                //↓これは試しで火山出したやつ。使いたかったらここから
                //Instantiate(Volcano, new Vector3(PlayerPos.transform.position.x, PlayerPos.transform.position.y - 2.0f), Quaternion.identity);
                Instantiate(Bullet, BulletPoint.position, Quaternion.identity);
                audioSource.PlayOneShot(sound1);
                Bulletcnt = 0;
            }
            //追尾弾
            if(HomingBulletcnt > HomingBulletTiming)
            {
                BulletRnd = Random.Range(1, 4);
                if(BulletRnd == 3)
                {
                    Instantiate(HomingBullet, HomingPoint.position, Quaternion.identity);
                    audioSource.PlayOneShot(sound1);
                }
                HomingBulletcnt = 0;
            }
        }
        if(NowHP <= 70 && NowHP > 50)
        {
            Bulletcnt += Time.deltaTime;
            HomingBulletcnt += Time.deltaTime;
            SnipingBulletcnt += Time.deltaTime;
            TrapBulletcnt += Time.deltaTime;
            //通常弾
            if (Bulletcnt > BulletTiming)
            {
                Instantiate(Bullet, BulletPoint.position, Quaternion.identity);
                audioSource.PlayOneShot(sound1);
                Bulletcnt = 0;
            }
            //追尾弾
            if (HomingBulletcnt > HomingBulletTiming)
            {
                BulletRnd = Random.Range(1, 3);
                if (BulletRnd == 2)
                {
                    Instantiate(HomingBullet, HomingPoint.position, Quaternion.identity);
                    audioSource.PlayOneShot(sound1);
                }
                HomingBulletcnt = 0;
            }
            //狙撃弾
            if(SnipingBulletcnt > SnipingBulletTiming)
            {
                BulletRnd = Random.Range(1, 5);
                if (BulletRnd == 4)
                {
                    float Snipingrad = GetAim();
                    Instantiate(SnipingBullet, SnipingPoint.position, Quaternion.AngleAxis(Snipingrad, Vector3.forward));
                }
                SnipingBulletcnt = 0;
            }
            //罠弾
            if(TrapBulletcnt>TrapBulletTiming)
            {
                BulletRnd = Random.Range(1, 5);
                if(BulletRnd == 4)
                {
                    float Aimrad = Random.Range(180.0f, 270.0f);
                    Instantiate(TrapBullet, TrapPoint.position, Quaternion.AngleAxis(Aimrad, Vector3.forward));
                }
                TrapBulletcnt = 0;
            }
        }
        if(NowHP <= 50 && NowHP > 0)
        {
            Bulletcnt += Time.deltaTime;
            HomingBulletcnt += Time.deltaTime;
            SnipingBulletcnt += Time.deltaTime;
            TrapBulletcnt += Time.deltaTime;
            //通常弾
            if (Bulletcnt > BulletTiming)
            {
                BulletRnd = Random.Range(1, 3);
                if (BulletRnd == 1)
                {
                    Instantiate(Bullet, BulletPoint.position, Quaternion.identity);
                    audioSource.PlayOneShot(sound1);
                }
                else if (BulletRnd == 2)
                {
                    Instantiate(Bullet, BulletPoint.position, Quaternion.identity);
                    audioSource.PlayOneShot(sound1);
                    Invoke("FirstBullet", 0.7f);
                }
                Bulletcnt = 0;
            }
            //追尾弾
            if (HomingBulletcnt > HomingBulletTiming)
            {
                BulletRnd = Random.Range(1, 3);
                if (BulletRnd == 2)
                {
                    Instantiate(HomingBullet, HomingPoint.position, Quaternion.identity);
                    audioSource.PlayOneShot(sound1);
                }
                HomingBulletcnt = 0;
            }
            //狙撃弾
            if (SnipingBulletcnt > SnipingBulletTiming)
            {
                BulletRnd = Random.Range(1, 3);
                if (BulletRnd == 2)
                {
                    float Snipingrad = GetAim();
                    Instantiate(SnipingBullet, SnipingPoint.position, Quaternion.AngleAxis(Snipingrad, Vector3.forward));
                }
                SnipingBulletcnt = 0;
            }
            //罠弾
            if (TrapBulletcnt > TrapBulletTiming)
            {
                BulletRnd = Random.Range(1, 4);
                if (BulletRnd == 3)
                {
                    float Aimrad = Random.Range(180.0f, 270.0f);
                    Instantiate(TrapBullet, TrapPoint.position, Quaternion.AngleAxis(Aimrad, Vector3.forward));
                }
                TrapBulletcnt = 0;
            }
            if (NowHP==50.0f)
            {
                if(SPFlag1==false)
                {
                    SPattack.StartAttack();
                    SPFlag1 = true;
                }
            }
            if (NowHP == 30.0f)
            {
                if (SPFlag2 == false)
                {
                    SPattack.StartAttack();
                    SPFlag2 = true;
                }
            }
            if (NowHP == 10.0f)
            {
                if (SPFlag3 == false)
                {
                    SPattack.StartAttack();
                    SPFlag3 = true;
                }
            }
        }
        if(NowHP <= 0)
        {
            if (ClearFlag == false)
            {
                ClearLine.PlayTimeline();
                ClearFlag = true;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerAttackPoint"))
        {
            //ダメージフラグがfalseだったらダメージ
            if (DamageFlag == false)
            {
                EnemyDamage(10);
            }

        }
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("PlayerBullet"))
    //    {
    //        Debug.Log("当たった");
    //        //ダメージフラグがfalseだったらダメージ
    //        if (DamageFlag == false)
    //        {
    //            //EnemyDamage();
    //        }
    //        collision.gameObject.SetActive(false);
    //    }
    //}
    public void EnemyDamage(float damage)
    {
        if (DamageFlag == false)
        {
            DamageFlag = true;
            // 敵体力減少
            enemyHp.DecHp(damage);
            audioSource.PlayOneShot(sound2);
            //ダメージ判定が終わった後、3秒後に無敵を解除する
            Invoke("InvincibleEnd", 3.0f);
            shake.PlayShake(2, 0);
        }
    }
    void InvincibleEnd()
    {
        DamageFlag = false;
        sp.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }
    private float GetAim()
    {
        Vector2 p1 = SnipingPoint.transform.position;
        Vector2 p2 = PlayerPos.transform.position;
        float dx = p2.x - p1.x;
        float dy = p2.y - p1.y;
        float rad = Mathf.Atan2(dy, dx);

        return rad * Mathf.Rad2Deg;
    }

    private void FirstBullet()
    {
        Instantiate(Bullet, BulletPoint.position, Quaternion.identity);
        audioSource.PlayOneShot(sound1);
        Invoke("LastBullet", 0.7f);
    }

    private void LastBullet()
    {
        Instantiate(Bullet, BulletPoint.position, Quaternion.identity);
        audioSource.PlayOneShot(sound1);
    }
}
