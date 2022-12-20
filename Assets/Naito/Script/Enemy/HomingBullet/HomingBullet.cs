using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    //�e�̑��x
    [SerializeField] private float bulletSpeed;
    //�e�̏㏸���x
    [SerializeField] private float UpSpeed;
    //�e�̑��x����
    [SerializeField] private float limitSpeed;
    //�e�̑��x����
    [SerializeField] private float RefectionSpeed;
    //�e��Rigidbody2D
    private Rigidbody2D rb;
    //�e��Transform
    private Transform bulletTrans;
    //�ǂ�������Ώ�
    [SerializeField] private GameObject Player;
    //�{�X�̃_���[�W����𕪂���p
    [SerializeField] private BossController_Spider Boss;
    //�_���[�W��
    [SerializeField] private float Damage = 20.0f;
    //���ː�
    private GameObject Enemy;
    //�z�[�~���O�g������
    private bool HomingChang = false;
    //���ˎg������
    private bool BulletRefection = false;
    //���˂���񂾂���
    private bool BoolRefection = false;
    //SE�o���p
    AudioSource audioSource;
    [SerializeField] private AudioClip sound1;
    //�U���G�t�F�N�g�p
    [SerializeField] private GameObject Parry;

    // �|�[�Y�p
    [SerializeField] private PauseManager pause;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bulletTrans = GetComponent<Transform>();
        Enemy = GameObject.Find("BossEnemy_Spider");
        Invoke("HBulletChang", 3.0f);
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // �|�[�Y��
        if (pause != null && pause.GetPause())
        {
            return;
        }

        if (BulletRefection == false)
        {
            if (HomingChang == false)
            {
                //rb.AddForce(transform.right * -bulletSpeed);
                //rb.AddForce(transform.up * UpSpeed);

                //float speedXTemp = Mathf.Clamp(rb.velocity.x, -limitSpeed, limitSpeed); //X�����̑��x�𐧌�
                //float speedYTemp = Mathf.Clamp(rb.velocity.y, -limitSpeed, limitSpeed);  //Y�����̑��x�𐧌�
                //rb.velocity = new Vector3(speedXTemp, speedYTemp);
            }
            else
            {
                Vector3 Homing = Player.transform.position - bulletTrans.position;
                rb.AddForce(Homing.normalized * bulletSpeed);

                float speedXTemp = Mathf.Clamp(rb.velocity.x, -limitSpeed, limitSpeed); //X�����̑��x�𐧌�
                float speedYTemp = Mathf.Clamp(rb.velocity.y, -limitSpeed, limitSpeed);  //Y�����̑��x�𐧌�
                rb.velocity = new Vector3(speedXTemp, speedYTemp);
            }
        }
        else
        {
            if (this.transform.position.x > Player.transform.position.x)
            {
                Vector3 Homing = Enemy.transform.position - bulletTrans.position;
                if (BoolRefection == false)
                {
                    //rb.AddForce(Homing.normalized * RefectionSpeed * 2, ForceMode2D.Impulse);
                    rb.AddForce(Vector3.right * 20.0f, ForceMode2D.Impulse);
                    BoolRefection = true;
                }
            }
            else
            {
                //Vector3 Homing = Enemy.transform.position - bulletTrans.position;
                if (BoolRefection == false)
                {
                    rb.AddForce(Vector3.right * -20.0f, ForceMode2D.Impulse);
                    BoolRefection = true;
                }
            }
        }

    }

    private void HBulletChang()
    {
        HomingChang = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerAttackPoint"))
        {
            BulletRefection = true;
            this.gameObject.tag = "PlayerBullet";
            audioSource.PlayOneShot(sound1);
            Instantiate(Parry, this.transform.position, Quaternion.identity);
        }
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
}