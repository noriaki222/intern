using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoonController : MonoBehaviour
{

    [SerializeField] private float Reflectionspeed = 10; //反射銃弾のスピード

    private Rigidbody2D rd;

    bool BulletRefection = false;
    private void Start()
    {
        rd = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (BulletRefection)
        {
            Vector3 lazerPos = transform.position; //Vector3型のplayerPosに現在の位置情報を格納
            lazerPos.x += Reflectionspeed * Time.deltaTime; //x座標にspeedを加算
            transform.position = lazerPos; //現在の位置情報に反映させる
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerAttackPoint"))
        {
            BulletRefection = true;
            rd.velocity = Vector3.zero;
            this.gameObject.tag = "PlayerBullet";
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor")|| other.gameObject.CompareTag("Enemy")|| other.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
