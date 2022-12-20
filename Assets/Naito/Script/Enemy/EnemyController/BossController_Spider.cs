using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController_Spider : MonoBehaviour
{
    //通常弾のプレハブ入れる用
    [SerializeField] private GameObject Bullet;
    //追尾弾のプレハブ入れる用
    [SerializeField] private GameObject HomingBullet;
    //狙撃弾のプレハブ入れる用
    [SerializeField] private GameObject SnipingBullet;
    //波打つ弾の発射オブジェクトのプレハブ入れる用
    [SerializeField] private WaveController WaveObj;
    //子蜘蛛を出現させるオブジェクトのプレハブ入れる用
    [SerializeField] private SpideSpawn kidSpawn;
    //火山のプレハブ入れる用
    [SerializeField] private GameObject Volcano;

    //通常弾の出現場所を格納する用
    [SerializeField] private Transform BulletPoint;
    //追尾弾の出現場所を格納する用
    [SerializeField] private Transform HomingPoint1;
    [SerializeField] private Transform HomingPoint2;
    [SerializeField] private Transform HomingPoint3;
    [SerializeField] private Transform HomingPoint4;
    [SerializeField] private Transform HomingPoint5;
    //狙撃弾の出現場所を格納するよう
    [SerializeField] private Transform SnipingPoint1;
    [SerializeField] private Transform SnipingPoint2;
    [SerializeField] private Transform SnipingPoint3;
    [SerializeField] private Transform SnipingPoint4;

    //各弾の発射に使うカウント
    private float Bulletcnt;
    //各弾が何回出るかのカウント用
    private int HomingCnt;
    private int SnipingCnt;
    //各弾が出るまでの間隔
    [SerializeField] private float HomingTime = 0.8f;
    [SerializeField] private float SnipingTime = 0.8f;
    //狙撃対象（Player）の座標を入れる用
    [SerializeField] private GameObject PlayerPos;

    //各弾が一回出るのにかかる時間
    [SerializeField] private float BulletTiming = 3.0f;

    //ダメージを食らっているかどうかのフラグ
    private bool DamageFlag = false;
    //点滅用に点滅させたいオブジェクトを入れる
    [SerializeField] private SpriteRenderer sp;
    //敵の現在のHPを格納する用
    [SerializeField] private float NowHP;
    //敵のHPを管理しているUIを入れる用
    [SerializeField] private EnemyHpBar enemyHp;

    //攻撃パターン変更用
    private int AttackChanger = 0;
    //デバックモードの切り替え用
    private bool DebugFlag = false;
    //敵が攻撃しているかどうかの判断をする用
    private bool AttackFlag = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //今のHPを格納
        NowHP = enemyHp.GetNowHp();
        if(AttackFlag==false)
        {
            AttackChanger = Random.Range(1, 4);
        }
        //ダメージ判定を受けているとき、点滅する
        if (DamageFlag)
        {
            //Mathf.Absは絶対値を返す、Mathf.Sinは＋なら１，－なら0を返す
            float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            //上の式で0と1が交互に来るので、それを透明度に入れて反転させている
            sp.color = new Color(1.0f, 0.0f, 0.0f, level);
        }

        //if (Bulletcnt > BulletTiming)
        //{
        //    Instantiate(Bullet, BulletPoint.position, Quaternion.identity);
        //    Bulletcnt = 0;
        //}

        //敵の攻撃パターン管理
        switch (AttackChanger)
        {
            case 1:
                HomingBulletTable1();
                AttackFlagChanger();
                AttackChanger = 0;
                break;
            case 2:
                WaveBulletTable();
                AttackFlagChanger();
                AttackChanger = 0;
                break;
            case 3:
                SnipingBulletTable1();
                AttackFlagChanger();
                AttackChanger = 0;
                break;
        }
        //デバック用に数字キーで攻撃を変化させるやつ
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AttackChanger = 1;
            HomingCnt = 0;
        }
        //デバック用に数字キーで攻撃を変化させるやつ
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AttackChanger = 2;
        }
        //デバック用に数字キーで攻撃を変化させるやつ
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            AttackChanger = 3;
        }
    }

    public void EnemyDamage(float damage)
    {
        if (DamageFlag == false)
        {
            DamageFlag = true;
            // 敵体力減少
            //enemyHp.DecHp(damage);
            //audioSource.PlayOneShot(sound2);
            //ダメージ判定が終わった後、3秒後に無敵を解除する
            Invoke("InvincibleEnd", 3.0f);
            //shake.PlayShake(2, 0);
        }
    }

    void InvincibleEnd()
    {
        DamageFlag = false;
        sp.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    private void HomingBulletTable1()
    {
        HomingBulletCreate(HomingPoint1.position);
        Invoke("HomingBulletTable2", HomingTime);
        HomingCnt += 1;
    }

    private void HomingBulletTable2()
    {
        HomingBulletCreate(HomingPoint2.position);
        Invoke("HomingBulletTable3", HomingTime);
        HomingCnt += 1;
    }

    private void HomingBulletTable3()
    {
        HomingBulletCreate(HomingPoint3.position);
        Invoke("HomingBulletTable4", HomingTime + 1.0f);
        HomingCnt += 1;
    }

    private void HomingBulletTable4()
    {
        HomingBulletCreate(HomingPoint4.position);
        Invoke("HomingBulletTable5", HomingTime);
        HomingCnt += 1;
    }

    private void HomingBulletTable5()
    {
        HomingBulletCreate(HomingPoint5.position);
        if (NowHP <= 50 && NowHP > 0)
        {
            Instantiate(Volcano, new Vector3(PlayerPos.transform.position.x, PlayerPos.transform.position.y - 2.0f), Quaternion.identity);
        }
        HomingCnt += 1;
        if (HomingCnt < 15)
        {
            Invoke("HomingBulletTable1", HomingTime + 1.0f);
        }
        else
        {
            Invoke("AttackFlagChanger", 1.0f);
        }
    }



    private void WaveBulletTable()
    {
        WaveObj.WaveExistenceOn();
        kidSpawn.SpawnExistenceOn();
    }

    private void HomingBulletCreate(Vector3 position)
    {
        Instantiate(HomingBullet, position, Quaternion.identity);
    }

    private void SnipingBulletTable1()
    {
        float AimRad = GetAim(SnipingPoint1);
        Instantiate(SnipingBullet, SnipingPoint1.position, Quaternion.AngleAxis(AimRad, Vector3.forward));
        Invoke("SnipingBulletTable2", SnipingTime);
        SnipingCnt += 1;
    }

    private void SnipingBulletTable2()
    {
        float AimRad = GetAim(SnipingPoint2);
        Instantiate(SnipingBullet, SnipingPoint2.position, Quaternion.AngleAxis(AimRad, Vector3.forward));
        Invoke("SnipingBulletTable3", SnipingTime);
        SnipingCnt += 1;
    }
    private void SnipingBulletTable3()
    {
        float AimRad = GetAim(SnipingPoint3);
        Instantiate(SnipingBullet, SnipingPoint3.position, Quaternion.AngleAxis(AimRad, Vector3.forward));
        Invoke("SnipingBulletTable4", SnipingTime);
        SnipingCnt += 1;
    }

    private void SnipingBulletTable4()
    {
        float AimRad = GetAim(SnipingPoint4);
        Instantiate(SnipingBullet, SnipingPoint4.position, Quaternion.AngleAxis(AimRad, Vector3.forward));
        SnipingCnt += 1;
        if(SnipingCnt < 12)
        {
            Invoke("SnipingBulletTable1", SnipingTime);
        }
        else
        {
            Invoke("AttackFlagChanger", 1.0f);
        }
    }

    private float GetAim(Transform SnipingPoint)
    {
        Vector2 p1 = SnipingPoint.position;
        Vector2 p2 = PlayerPos.transform.position;
        float dx = p2.x - p1.x;
        float dy = p2.y - p1.y;
        float rad = Mathf.Atan2(dy, dx);

        return rad * Mathf.Rad2Deg;
    }

    public void AttackFlagChanger()
    {
        if(AttackFlag)
        {
            AttackFlag = false;
        }
        else
        {
            AttackFlag = true;
        }
    }
}
