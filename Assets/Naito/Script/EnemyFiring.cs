using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFiring : MonoBehaviour
{
    //弾のプレハブ入れる用
    [SerializeField] private GameObject bullet;
    //バレットポイント格納用
    [SerializeField] private Transform attackPoint;
    //弾の出現頻度
    float bulletcount;
    //敵のHP
    [SerializeField] private int EnemyHP = 5;
    //当たり判定フラグ
    private bool CollisionFlag = false;
    //点滅用変数
    [SerializeField] private SpriteRenderer sp;
    void Update()
    {
        bulletcount += Time.deltaTime;
        //ダメージ判定を受けているとき、点滅する
        if (CollisionFlag)
        {
            //Mathf.Absは絶対値を返す、Mathf.Sinは＋なら１，－なら0を返す
            float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            //上の式で0と1が交互に来るので、それを透明度に入れて反転させている
            sp.color = new Color(1.0f, 0.0f, 0.0f, level);
        }
        if (bulletcount>3.0f)
        {
            Instantiate(bullet, attackPoint.position, Quaternion.identity);
            bulletcount = 0.0f;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)

    {
        if(other.gameObject.CompareTag("PlayerAttackPoint"))
        {
            //ダメージフラグがfalseだったらダメージ
            if (CollisionFlag == false)
            {
                EnemyDamage();
            }

        }
    }

    void EnemyDamage()
    {
        EnemyHP--;
        CollisionFlag = true;
        //ダメージ判定が終わった後、3秒後に無敵を解除する
        Invoke("InvincibleEnd", 3.0f);
    }

    void InvincibleEnd()
    {
        CollisionFlag = false;
        sp.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
    }
}
