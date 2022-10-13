using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy_Boon : MonoBehaviour
{
    //狙撃弾のプレハブ入れる用
    [SerializeField] private GameObject SnipingBullet;
    //骨雲のプレハブ入れる用
    [SerializeField] private GameObject BoonCloud;
    //墓穴のプレハブ入れる用
    [SerializeField] private GameObject Grave;

    //狙撃弾の出現場所を格納する用
    [SerializeField] private Transform SnipingPoint;
    //骨雲の出現場所を格納する用
    [SerializeField] private Transform CloudPoint;
    //墓穴の出現場所を格納する用
    [SerializeField] private Transform GravePoint;

    //各弾の発射に使うカウント
    private float SnipingBulletcnt;
    private float BoonCloudcnt;
    //各弾が一回出るのにかかる時間
    [SerializeField] private float SnipingBulletTiming = 3.0f;
    [SerializeField] private float BoonCloudTiming = 3.0f;
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
    //[SerializeField] private SpiderSpecialAttack SPattack;
    //必殺技フラグ（1回しか打たないようにするため）
    private bool SPFlag1 = false;
    private bool SPFlag2 = false;
    //private bool SPFlag3 = false;

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
            SnipingBulletcnt += Time.deltaTime;
            //狙撃弾
            if (SnipingBulletcnt > SnipingBulletTiming)
            {
                float Snipingrad = GetAim();
                Instantiate(SnipingBullet, SnipingPoint.position, Quaternion.AngleAxis(Snipingrad, Vector3.forward));
                
                SnipingBulletcnt = 0;
            }
        }
        if(NowHP <= 70 && NowHP > 50)
        {
            SnipingBulletcnt += Time.deltaTime;
            BoonCloudcnt += Time.deltaTime;
            //狙撃弾
            if (SnipingBulletcnt > SnipingBulletTiming)
            {
                float Snipingrad = GetAim();
                Instantiate(SnipingBullet, SnipingPoint.position, Quaternion.AngleAxis(Snipingrad, Vector3.forward));

                SnipingBulletcnt = 0;
            }
            //骨雲
            if(BoonCloudcnt > BoonCloudTiming)
            {
                BulletRnd = Random.Range(1, 4);
                if(BulletRnd == 3)
                {
                    Instantiate(BoonCloud, CloudPoint.position, Quaternion.identity);
                }
                BoonCloudcnt = 0;
            }
            if(NowHP == 70)
            {
                if(SPFlag1==false)
                {
                    Instantiate(Grave, GravePoint.position, Quaternion.identity);
                    SPFlag1 = true;
                }
            }
        }
        if(NowHP <= 50 && NowHP > 40)
        {
            SnipingBulletcnt += Time.deltaTime;
            BoonCloudcnt += Time.deltaTime;
            //狙撃弾
            if (SnipingBulletcnt > SnipingBulletTiming)
            {
                BulletRnd = Random.Range(1, 3);
                if (BulletRnd == 1)
                {
                    float Snipingrad = GetAim();
                    Instantiate(SnipingBullet, SnipingPoint.position, Quaternion.AngleAxis(Snipingrad, Vector3.forward));
                }
                else
                {
                    float Snipingrad = GetAim();
                    Instantiate(SnipingBullet, SnipingPoint.position, Quaternion.AngleAxis(Snipingrad, Vector3.forward));
                    Invoke("LastBullet", 0.5f);
                }
                SnipingBulletcnt = 0;
            }
            //骨雲
            if (BoonCloudcnt > BoonCloudTiming)
            {
                BulletRnd = Random.Range(1, 4);
                if (BulletRnd == 3)
                {
                    Instantiate(BoonCloud, CloudPoint.position, Quaternion.identity);
                }
                BoonCloudcnt = 0;
            }
        }
        if (NowHP <= 40 && NowHP > 0)
        {
            SnipingBulletcnt += Time.deltaTime;
            BoonCloudcnt += Time.deltaTime;
            //狙撃弾
            if (SnipingBulletcnt > SnipingBulletTiming)
            {
                BulletRnd = Random.Range(1, 3);
                if (BulletRnd == 1)
                {
                    float Snipingrad = GetAim();
                    Instantiate(SnipingBullet, SnipingPoint.position, Quaternion.AngleAxis(Snipingrad, Vector3.forward));
                }
                else
                {
                    float Snipingrad = GetAim();
                    Instantiate(SnipingBullet, SnipingPoint.position, Quaternion.AngleAxis(Snipingrad, Vector3.forward));
                    Invoke("LastBullet", 0.5f);
                }
                SnipingBulletcnt = 0;
            }
            //骨雲
            if (BoonCloudcnt > BoonCloudTiming)
            {
                BulletRnd = Random.Range(1, 3);
                if (BulletRnd == 2)
                {
                    Instantiate(BoonCloud, CloudPoint.position, Quaternion.identity);
                }
                BoonCloudcnt = 0;
            }
            if (NowHP == 30)
            {
                if (SPFlag2 == false)
                {
                    Instantiate(Grave, GravePoint.position, Quaternion.identity);
                    SPFlag2 = true;
                }
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
                EnemyDamage();
            }

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            //ダメージフラグがfalseだったらダメージ
            if (DamageFlag == false)
            {
                EnemyDamage();
            }
            collision.gameObject.SetActive(false);
        }
    }
    void EnemyDamage()
    {
        DamageFlag = true;
        // 敵体力減少
        enemyHp.DecHp(decreaseHp);
        //ダメージ判定が終わった後、3秒後に無敵を解除する
        Invoke("InvincibleEnd", 3.0f);
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

    private void LastBullet()
    {
        float Snipingrad = GetAim();
        Instantiate(SnipingBullet, SnipingPoint.position, Quaternion.AngleAxis(Snipingrad, Vector3.forward));
    }
}
