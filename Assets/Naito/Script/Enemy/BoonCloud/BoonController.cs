using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoonController : MonoBehaviour
{

    [SerializeField] private float Reflectionspeed = 10; //”½Ëe’e‚ÌƒXƒs[ƒh

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
            Vector3 lazerPos = transform.position; //Vector3Œ^‚ÌplayerPos‚ÉŒ»İ‚ÌˆÊ’uî•ñ‚ğŠi”[
            lazerPos.x += Reflectionspeed * Time.deltaTime; //xÀ•W‚Éspeed‚ğ‰ÁZ
            transform.position = lazerPos; //Œ»İ‚ÌˆÊ’uî•ñ‚É”½‰f‚³‚¹‚é
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
