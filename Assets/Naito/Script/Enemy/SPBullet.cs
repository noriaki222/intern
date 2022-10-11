using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPBullet : MonoBehaviour
{
    private bool BulletRefection = false;
    //’e‚ÌRigidbody2D
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }
    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerAttackPoint"))
        {
            rb.AddForce(transform.right * 5.0f, ForceMode2D.Impulse);
            this.gameObject.tag = "PlayerBullet";
            this.gameObject.layer =6;
            //hit.AddHit();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy")&&this.gameObject.CompareTag("PlayerBullet"))
        {
            this.gameObject.SetActive(false);
            this.gameObject.tag = "EnemyBullet";
            this.gameObject.layer = 8;
            rb.velocity = Vector2.zero;
        }
    }

}
