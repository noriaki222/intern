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
    private GameObject Player;
    //���ː�
    private GameObject Enemy;
    //�z�[�~���O�g������
    private bool HomingChang = false;
    //���ˎg������
    private bool BulletRefection = false;
    //���˂���񂾂���
    private bool BoolRefection = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bulletTrans = GetComponent<Transform>();
        Player = GameObject.Find("player");
        Enemy = GameObject.Find("BossEnemy_Spider");
        Invoke("HBulletChang", 2.0f);
    }

    private void Update()
    {
        if (BulletRefection == false)
        {
            if (HomingChang == false)
            {
                rb.AddForce(transform.right * -bulletSpeed);
                rb.AddForce(transform.up * UpSpeed);

                float speedXTemp = Mathf.Clamp(rb.velocity.x, -limitSpeed, limitSpeed); //X�����̑��x�𐧌�
                float speedYTemp = Mathf.Clamp(rb.velocity.y, -limitSpeed, limitSpeed);  //Y�����̑��x�𐧌�
                rb.velocity = new Vector3(speedXTemp, speedYTemp);
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
            Vector3 Homing = Enemy.transform.position - bulletTrans.position;
            if (BoolRefection == false)
            {
                rb.AddForce(Homing.normalized * RefectionSpeed * 5, ForceMode2D.Impulse);
                BoolRefection = true;
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
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
            Destroy(this.gameObject);
    }
}