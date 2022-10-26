using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBulletControl : MonoBehaviour
{
    [SerializeField] private float speed = 5; //銃弾のスピード

    [SerializeField] private float Reflectionspeed = 10; //反射銃弾のスピード

    //プレイヤーの位置情報入れる人
    [SerializeField] private GameObject Player;
    //ダメージ量
    [SerializeField] private float Damage = 10.0f;
    //ボスのダメージ判定を分ける用
    [SerializeField] private BossEnemy_Spider Boss;

    //SE出す用
    AudioSource audioSource;
    [SerializeField] private AudioClip sound1;

    //攻撃エフェクト用
    [SerializeField] private GameObject Parry;

    //コンボ加算用
    //[SerializeField] private HitUI hit;

    bool BulletRefection = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

    }

    public void Move()
    {
        if (BulletRefection == false)
        {
            //弾移動
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        else
        {
            if (this.transform.position.x > Player.transform.position.x)
            {
                Vector3 lazerPos = transform.position; //Vector3型のplayerPosに現在の位置情報を格納
                lazerPos.x += Reflectionspeed * Time.deltaTime; //x座標にspeedを加算
                transform.position = lazerPos; //現在の位置情報に反映させる
                transform.localScale = new Vector3(-0.08f, 0.08f, 1);
            }
            else
            {
                Vector3 lazerPos = transform.position; //Vector3型のplayerPosに現在の位置情報を格納
                lazerPos.x -= Reflectionspeed * Time.deltaTime; //x座標にspeedを加算
                transform.position = lazerPos; //現在の位置情報に反映させる
                transform.localScale = new Vector3(0.08f, 0.08f, 1);
            }
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Floor"))
        {
            Destroy(this.gameObject);
        }
        if (collision.gameObject.CompareTag("Enemy") && this.gameObject.CompareTag("PlayerBullet"))
        {
            Boss.EnemyDamage(Damage);
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerAttackPoint"))
        {
            BulletRefection = true;
            this.gameObject.tag = "PlayerBullet";
            this.gameObject.layer = 6;
            audioSource.PlayOneShot(sound1);
            Instantiate(Parry, this.transform.position, Quaternion.identity);
            //hit.AddHit();
        }
    }
}