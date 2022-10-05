using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�v���C���[�̈ړ��Ɋւ���X�N���v�g
public class PlayerMove : MonoBehaviour
{
    //�v���C���[�̈ړ����x
    [SerializeField] private float speed = 8.0f;
    //�v���C���[�̈ړ��͈�
    //public float moveableRange = 30.0f;
    //�v���C���[�̃W�����v��
    [SerializeField] private float power = 1.0f;
    //�v���C���[�̃��W�b�g�{�f�B
    private Rigidbody2D rbody2D;
    //�v���C���[���W�����v���Ă������̏���
    private int jumpCount = 0;
    //�v���C���[��HP
    [SerializeField] private int PlayerHP = 5;
    //�����蔻��t���O
    private bool CollisionFlag = false;
    //�_�ŗp�ϐ�
    [SerializeField] private SpriteRenderer sp;
    //�U���̓����蔻��p
    [SerializeField] private GameObject AttackArea;
    //�A�^�b�N�|�C���g�i�[�p
    [SerializeField] private Transform attackPoint;

    [SerializeField] private LifeUI life;


    private void Start()
    {
        rbody2D = GetComponent<Rigidbody2D>();
    }

    //Update is called once per frame
    void Update()
    {
        //�_���[�W������󂯂Ă���Ƃ��A�_�ł���
        if(CollisionFlag)
        {
            //Mathf.Abs�͐�Βl��Ԃ��AMathf.Sin�́{�Ȃ�P�C�|�Ȃ�0��Ԃ�
            float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            //��̎���0��1�����݂ɗ���̂ŁA����𓧖��x�ɓ���Ĕ��]�����Ă���
            sp.color = new Color(0.0f, 1.0f, 1.0f, level);
        }
        //���ړ�
        transform.Translate(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, 0, 0);
        //�W�����v
        if (Input.GetKeyDown(KeyCode.UpArrow)&&this.jumpCount<1)
        {
            this.rbody2D.AddForce(transform.up * power);
            jumpCount++;
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(AttackArea, attackPoint.position, Quaternion.identity);
        }
        //�v���C���[�̈ړ�����
        //transform.position = new Vector2(Mathf.Clamp(
        //    transform.position.x, -moveableRange, moveableRange),
        //    transform.position.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Floor"))
        {
            jumpCount = 0;
        }

        if (other.gameObject.CompareTag("Bullet"))
        {
            //�_���[�W�t���O��false��������_���[�W
            if(CollisionFlag==false)
            PlayerDamage();
        }
    }

    void PlayerDamage()
    {
        // �̗͌���
        life.LossLife();
        CollisionFlag = true;
        //�_���[�W���肪�I�������A3�b��ɖ��G����������
        Invoke("InvincibleEnd", 3.0f);
    }

    void InvincibleEnd()
    {
        CollisionFlag = false;
        sp.color = new Color(0.0f, 1.0f, 1.0f, 1.0f);
    }

}
