using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KSBulletControl : MonoBehaviour
{
    [SerializeField] private float speed = 5; //銃弾のスピード

    //SE出す用
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
                Vector3 lazerPos = transform.position; //Vector3型のplayerPosに現在の位置情報を格納
                lazerPos.x -= speed * Time.deltaTime; //x座標にspeedを加算
                transform.position = lazerPos; //現在の位置情報に反映させる
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