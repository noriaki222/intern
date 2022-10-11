using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy_Boon : MonoBehaviour
{
    //狙撃弾のプレハブ入れる用
    [SerializeField] private GameObject SnipingBullet;
    //骨雲のプレハブ入れる用
    [SerializeField] private GameObject BoonCloud;

    //狙撃弾の出現場所を格納する用
    [SerializeField] private Transform SnipingPoint;
    //骨雲の出現場所を格納する用
    [SerializeField] private Transform TrapPoint;

    //弾の発射に使うカウント
    private float Bulletcnt;
    //弾が一回出るのにかかる時間
    [SerializeField] private float BulletTiming = 3.0f;
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
    private float NowHP;

    // Update is called once per frame
    void Update()
    {
        //マイフレームカウントを増加
        Bulletcnt += Time.deltaTime;
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
        if (Bulletcnt > BulletTiming)
        {
            //通常弾
            Instantiate(Bullet, BulletPoint.position, Quaternion.identity);
            if (NowHP <= 85.0f)
            {
                //追尾弾
                Instantiate(HomingBullet, HomingPoint.position, Quaternion.identity);
            }
            if(NowHP <= 50.0f)
            {
                //狙撃弾
                Instantiate(SnipingBullet, SnipingPoint.position, Quaternion.AngleAxis(GetAim(), Vector3.forward));
            }
            if(NowHP <= 30)
            {
                float Aimrad = Random.Range(180.0f, 270.0f);
                //狙撃弾
                Instantiate(TrapBullet, TrapPoint.position, Quaternion.AngleAxis(Aimrad, Vector3.forward));
            }
            Bulletcnt = 0.0f;
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
        if (collision.gameObject.CompareTag("Bullet"))
        {
            //ダメージフラグがfalseだったらダメージ
            if (DamageFlag == false)
            {
                EnemyDamage();
            }
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
}
