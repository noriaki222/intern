using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    //弾の速度
    [SerializeField] private float bulletSpeed;
    //弾の上昇速度
    [SerializeField] private float UpSpeed;
    //弾の速度制限
    [SerializeField] private float limitSpeed;
    //弾の速度制限
    [SerializeField] private float RefectionSpeed;
    //弾のRigidbody2D
    private Rigidbody2D rb;
    //弾のTransform
    private Transform bulletTrans;
    //追いかける対象
    [SerializeField] private GameObject Player;
    //ボスのダメージ判定を分ける用
    [SerializeField] private BossController_Spider Boss;
    //ダメージ量
    [SerializeField] private float Damage = 20.0f;
    //反射先
    private GameObject Enemy;
    //ホーミング使い分け
    private bool HomingChang = false;
    //反射使い分け
    private bool BulletRefection = false;
    //反射を一回だけに
    private bool BoolRefection = false;
    //SE出す用
    AudioSource audioSource;
    [SerializeField] private AudioClip sound1;
    //攻撃エフェクト用
    [SerializeField] private GameObject Parry;

    // ポーズ用
    [SerializeField] private PauseManager pause;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bulletTrans = GetComponent<Transform>();
        Enemy = GameObject.Find("BossEnemy_Spider");
        Invoke("HBulletChang", 3.0f);
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // ポーズ中
        if (pause != null && pause.GetPause())
        {
            return;
        }

        if (BulletRefection == false)
        {
            if (HomingChang == false)
            {
                //rb.AddForce(transform.right * -bulletSpeed);
                //rb.AddForce(transform.up * UpSpeed);

                //float speedXTemp = Mathf.Clamp(rb.velocity.x, -limitSpeed, limitSpeed); //X方向の速度を制限
                //float speedYTemp = Mathf.Clamp(rb.velocity.y, -limitSpeed, limitSpeed);  //Y方向の速度を制限
                //rb.velocity = new Vector3(speedXTemp, speedYTemp);
            }
            else
            {
                Vector3 Homing = Player.transform.position - bulletTrans.position;
                rb.AddForce(Homing.normalized * bulletSpeed);

                float speedXTemp = Mathf.Clamp(rb.velocity.x, -limitSpeed, limitSpeed); //X方向の速度を制限
                float speedYTemp = Mathf.Clamp(rb.velocity.y, -limitSpeed, limitSpeed);  //Y方向の速度を制限
                rb.velocity = new Vector3(speedXTemp, speedYTemp);
            }
        }
        else
        {
            if (this.transform.position.x > Player.transform.position.x)
            {
                Vector3 Homing = Enemy.transform.position - bulletTrans.position;
                if (BoolRefection == false)
                {
                    //rb.AddForce(Homing.normalized * RefectionSpeed * 2, ForceMode2D.Impulse);
                    rb.AddForce(Vector3.right * 20.0f, ForceMode2D.Impulse);
                    BoolRefection = true;
                }
            }
            else
            {
                //Vector3 Homing = Enemy.transform.position - bulletTrans.position;
                if (BoolRefection == false)
                {
                    rb.AddForce(Vector3.right * -20.0f, ForceMode2D.Impulse);
                    BoolRefection = true;
                }
            }
        }

    }

    private void HBulletChang()
    {
        HomingChang = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerAttackPoint"))
        {
            BulletRefection = true;
            this.gameObject.tag = "PlayerBullet";
            audioSource.PlayOneShot(sound1);
            Instantiate(Parry, this.transform.position, Quaternion.identity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
        if (collision.gameObject.CompareTag("Enemy") && this.gameObject.CompareTag("PlayerBullet"))
        {
            Boss.EnemyDamage(Damage);
            Destroy(this.gameObject);
        }
    }
}