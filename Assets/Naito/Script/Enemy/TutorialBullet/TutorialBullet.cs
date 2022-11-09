using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBullet : MonoBehaviour
{
    [SerializeField] private float speed = 5; //�e�e�̃X�s�[�h

    [SerializeField] private float Reflectionspeed = 10; //���ˏe�e�̃X�s�[�h

    [SerializeField] private TutorialEnemy Boss;
    [SerializeField] private float Damage = 10.0f;

    //�v���C���[�̈ʒu�������l
    [SerializeField] private GameObject Player;
    //SE�o���p
    AudioSource audioSource;
    [SerializeField] private AudioClip sound1;

    //�U���G�t�F�N�g�p
    [SerializeField] private GameObject Parry;

    private bool ChargeFlag = true;

    //�R���{���Z�p
    //[SerializeField] private HitUI hit;

    bool BulletRefection = false;

    //private ParticleSystem ps;


    void Start()
    {
        //ps = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
        Invoke("ChargeEnd", 0.5f);
        //var fo = ps.forceOverLifetime;
        //fo.x = 0;
        //fo.xMultiplier = 0;
        //fo.y = 3;
        //fo.yMultiplier = 5;
        Invoke("RotForce", 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        Move();

    }

    public void Move()
    {
        if (ChargeFlag)
        {

        }
        else
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
            audioSource.PlayOneShot(sound1);
            Instantiate(Parry, this.transform.position, Quaternion.identity);
            //hit.AddHit();
        }
    }

    private void ChargeEnd()
    {
        ChargeFlag = false;
    }

    //private void RotForce()
    //{
    //    var fo = ps.forceOverLifetime;
    //    fo.x = 3;
    //    fo.xMultiplier = 5;
    //    fo.y = 0;
    //    fo.yMultiplier = 0;
    //}
}