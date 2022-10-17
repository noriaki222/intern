using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPBullet : MonoBehaviour
{
    private bool BulletRefection = false;
    //弾のRigidbody2D
    private Rigidbody2D rb;
    //プレイヤーの位置情報入れる人
    [SerializeField] private GameObject Player;
    //SE出す用
    AudioSource audioSource;
    [SerializeField] private AudioClip sound1;
    //敵へのダメージを取るよう
    [SerializeField] private BossEnemy_Spider Boss;
    //敵に与えるダメージ量
    [SerializeField] private float Damage = 30.0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

    }
    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerAttackPoint"))
        {
            if (this.transform.position.x > Player.transform.position.x)
            {
                rb.AddForce(transform.right * 30.0f, ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(transform.right * -30.0f, ForceMode2D.Impulse);
            }
            this.gameObject.tag = "PlayerBullet";
            this.gameObject.layer =6;
            audioSource.PlayOneShot(sound1);
            //hit.AddHit();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player")||collision.gameObject.CompareTag("Wall"))
        {
            this.gameObject.SetActive(false);
            //this.gameObject.tag = "EnemyBullet";
            this.gameObject.layer = 8;
            rb.velocity = Vector2.zero;
        }
        if (collision.gameObject.CompareTag("Enemy") && this.gameObject.CompareTag("PlayerBullet"))
        {
            Boss.EnemyDamage(Damage);
            this.gameObject.SetActive(false);
            //this.gameObject.tag = "EnemyBullet";
            this.gameObject.layer = 8;
            rb.velocity = Vector2.zero;
        }
    }

}
