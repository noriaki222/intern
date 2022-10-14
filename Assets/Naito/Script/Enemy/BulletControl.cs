using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    [SerializeField] private float speed = 5; //e’e‚ÌƒXƒs[ƒh

    [SerializeField] private float Reflectionspeed = 10; //”½Ëe’e‚ÌƒXƒs[ƒh

    [SerializeField] private BossEnemy_Spider Boss;
    [SerializeField] private float Damage = 10.0f;

    //ƒvƒŒƒCƒ„[‚ÌˆÊ’uî•ñ“ü‚ê‚él
    [SerializeField] private GameObject Player;
    //SEo‚·—p
    AudioSource audioSource;
    [SerializeField] private AudioClip sound1;

    //ƒRƒ“ƒ{‰ÁZ—p
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
            Vector3 lazerPos = transform.position; //Vector3Œ^‚ÌplayerPos‚ÉŒ»İ‚ÌˆÊ’uî•ñ‚ğŠi”[
            lazerPos.x -= speed * Time.deltaTime; //xÀ•W‚Éspeed‚ğ‰ÁZ
            transform.position = lazerPos; //Œ»İ‚ÌˆÊ’uî•ñ‚É”½‰f‚³‚¹‚é
        }
        else
        {
            if (this.transform.position.x > Player.transform.position.x)
            {
                Vector3 lazerPos = transform.position; //Vector3Œ^‚ÌplayerPos‚ÉŒ»İ‚ÌˆÊ’uî•ñ‚ğŠi”[
                lazerPos.x += Reflectionspeed * Time.deltaTime; //xÀ•W‚Éspeed‚ğ‰ÁZ
                transform.position = lazerPos; //Œ»İ‚ÌˆÊ’uî•ñ‚É”½‰f‚³‚¹‚é
                transform.localScale = new Vector3(-0.5f, 0.5f, 1);
            }
            else
            {
                Vector3 lazerPos = transform.position; //Vector3Œ^‚ÌplayerPos‚ÉŒ»İ‚ÌˆÊ’uî•ñ‚ğŠi”[
                lazerPos.x -= Reflectionspeed * Time.deltaTime; //xÀ•W‚Éspeed‚ğ‰ÁZ
                transform.position = lazerPos; //Œ»İ‚ÌˆÊ’uî•ñ‚É”½‰f‚³‚¹‚é
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
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
        if(collision.gameObject.CompareTag("Enemy")&&this.gameObject.CompareTag("PlayerBullet"))
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
            audioSource.PlayOneShot(sound1);
            //hit.AddHit();
        }
    }
}