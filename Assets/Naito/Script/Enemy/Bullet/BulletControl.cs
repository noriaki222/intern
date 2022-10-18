using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    [SerializeField] private float speed = 5; //�e�e�̃X�s�[�h

    [SerializeField] private float Reflectionspeed = 10; //���ˏe�e�̃X�s�[�h

    [SerializeField] private BossEnemy_Spider Boss;
    [SerializeField] private float Damage = 10.0f;

    //�v���C���[�̈ʒu�������l
    [SerializeField] private GameObject Player;
    //SE�o���p
    AudioSource audioSource;
    [SerializeField] private AudioClip sound1;

    //�U���G�t�F�N�g�p
    [SerializeField] private GameObject Parry;

    //�R���{���Z�p
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
            Vector3 lazerPos = transform.position; //Vector3�^��playerPos�Ɍ��݂̈ʒu�����i�[
            lazerPos.x -= speed * Time.deltaTime; //x���W��speed�����Z
            transform.position = lazerPos; //���݂̈ʒu���ɔ��f������
        }
        else
        {
            if (this.transform.position.x > Player.transform.position.x)
            {
                Vector3 lazerPos = transform.position; //Vector3�^��playerPos�Ɍ��݂̈ʒu�����i�[
                lazerPos.x += Reflectionspeed * Time.deltaTime; //x���W��speed�����Z
                transform.position = lazerPos; //���݂̈ʒu���ɔ��f������
                transform.localScale = new Vector3(-0.5f, 0.5f, 1);
            }
            else
            {
                Vector3 lazerPos = transform.position; //Vector3�^��playerPos�Ɍ��݂̈ʒu�����i�[
                lazerPos.x -= Reflectionspeed * Time.deltaTime; //x���W��speed�����Z
                transform.position = lazerPos; //���݂̈ʒu���ɔ��f������
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
            Instantiate(Parry, this.transform.position, Quaternion.identity);
            //hit.AddHit();
        }
    }
}