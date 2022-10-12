using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBulletControl : MonoBehaviour
{
    [SerializeField] private float speed = 5; //銃弾のスピード

    [SerializeField] private float Reflectionspeed = 10; //反射銃弾のスピード

    //プレイヤーの位置情報入れる人
    [SerializeField] private GameObject Player;

    //コンボ加算用
    //[SerializeField] private HitUI hit;

    bool BulletRefection = false;

    void Start()
    {

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
                transform.localScale = new Vector3(-0.5f, 0.5f, 1);
            }
            else
            {
                Vector3 lazerPos = transform.position; //Vector3型のplayerPosに現在の位置情報を格納
                lazerPos.x -= Reflectionspeed * Time.deltaTime; //x座標にspeedを加算
                transform.position = lazerPos; //現在の位置情報に反映させる
                transform.localScale = new Vector3(0.5f, 0.5f, 1);
            }
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player")|| collision.gameObject.CompareTag("Enemy")|| collision.gameObject.CompareTag("Floor"))
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerAttackPoint"))
        {
            BulletRefection = true;
            this.gameObject.tag = "PlayerBullet";
            //hit.AddHit();
        }
    }
}