using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KSBulletControl : MonoBehaviour
{
    [SerializeField] private float speed = 5; //e’e‚ÌƒXƒs[ƒh

    //SEo‚·—p
    //AudioSource audioSource;
    //[SerializeField] private AudioClip sound1;

    //private ParticleSystem ps;

    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
                Vector3 lazerPos = transform.position; //Vector3Œ^‚ÌplayerPos‚ÉŒ»İ‚ÌˆÊ’uî•ñ‚ğŠi”[
                lazerPos.x -= speed * Time.deltaTime; //xÀ•W‚Éspeed‚ğ‰ÁZ
                transform.position = lazerPos; //Œ»İ‚ÌˆÊ’uî•ñ‚É”½‰f‚³‚¹‚é
    }

    //private void OnBecameInvisible()
    //{
    //    Destroy(this.gameObject);
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }

}