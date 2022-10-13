using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�v���C���[�̈ړ��Ɋւ���X�N���v�g
public class PlayerMove : MonoBehaviour
{
    //�v���C���[�̈ړ����x
    [SerializeField] private float Dashspeed = 6.0f;
    //�v���C���[�̓k���X�s�[�h
    [SerializeField] private float Walkspeed = 2.0f;
    //�v���C���[�̈ړ��͈�
    //public float moveableRange = 30.0f;
    //�v���C���[�̃W�����v��
    [SerializeField] private float power = 1.0f;
    //�v���C���[�̃��W�b�g�{�f�B
    private Rigidbody2D rbody2D;
    //�v���C���[���W�����v���Ă������̏���
    [SerializeField] private int jumpCount = 0;
    //�v���C���[��HP
    //[SerializeField] private int PlayerHP = 5;
    //�����蔻��t���O
    private bool CollisionFlag = false;
    //�_�ŗp�ϐ�
    [SerializeField] private SpriteRenderer sp;
    //���C�tUI�p
    [SerializeField] private LifeUI life;
    //�z�[�~���O�I�u�W�F�N�g
    [SerializeField] private GameObject HomingObj;
    //�U���G���A�p
    [SerializeField] private PlayerAttack AttackArea;
    //�ړ������i�[�p
    private float x_val;
    //㩂ɂ������Ă��邩�̔��f�p
    [SerializeField] private bool BoolTrap = false;
    //�v���C���[���g���b�v���甲����̂ɕK�v�ȃN���b�N��
    private int Trapcnt;


    private void Start()
    {
        rbody2D = GetComponent<Rigidbody2D>();
    }

    //Update is called once per frame
    void Update()
    {
        if (CollisionFlag)
        {
            //�_���[�W������󂯂Ă���Ƃ��A�_�ł���
            //Mathf.Abs�͐�Βl��Ԃ��AMathf.Sin�́{�Ȃ�P�C�|�Ȃ�0��Ԃ�
            float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            //��̎���0��1�����݂ɗ���̂ŁA����𓧖��x�ɓ���Ĕ��]�����Ă���
            sp.color = new Color(1.0f, 1.0f, 1.0f, level);
        }
        if (BoolTrap == false)
        {
            if (AttackArea.GetHitStop() == false)
            {
                rbody2D.isKinematic = false;
                x_val = Input.GetAxis("Horizontal");
                Debug.Log(x_val);
                if (x_val > 0)
                {
                    //�E������
                    transform.localScale = new Vector3(0.1f, 0.1f, 1);
                    //����
                    transform.Translate(Walkspeed * Time.deltaTime, 0, 0);
                    if (x_val >= 0.8f)
                    {
                        //����
                        transform.Translate(Dashspeed * Time.deltaTime, 0, 0);
                    }
                }
                else if (x_val < 0)
                {
                    //��������
                    transform.localScale = new Vector3(-0.1f, 0.1f, 1);
                    //����
                    transform.Translate(-Walkspeed * Time.deltaTime, 0, 0);
                    if (x_val <= -0.8f)
                    {
                        //����
                        transform.Translate(-Dashspeed * Time.deltaTime, 0, 0);
                    }
                }
                //���ړ�
                //transform.Translate(x_val * speed * Time.deltaTime, 0, 0);
                //�W�����v
                if ((Input.GetKeyDown(KeyCode.UpArrow) && this.jumpCount < 1) || ((Input.GetKeyDown("joystick button 0") || Input.GetKeyDown("joystick button 1") ||
                    Input.GetKeyDown("joystick button 2") || Input.GetKeyDown("joystick button 3")) && this.jumpCount < 1))
                {
                    this.rbody2D.AddForce(transform.up * power);
                    jumpCount++;
                }
                //�U��
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 4") || Input.GetKeyDown("joystick button 5"))
                {
                    AttackArea.AttackAreaCreate();
                }
                //�v���C���[�̈ړ�����
                //transform.position = new Vector2(Mathf.Clamp(
                //    transform.position.x, -moveableRange, moveableRange),
                //    transform.position.y);
            }
            else
            {

            }
        }
        else
        {
            //rbody2D.velocity = Vector3.zero;
            //rbody2D.isKinematic = true;
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 0") || Input.GetKeyDown("joystick button 1") ||
                Input.GetKeyDown("joystick button 2") || Input.GetKeyDown("joystick button 3") || Input.GetKeyDown("joystick button 4") || Input.GetKeyDown("joystick button 5"))
            {
                Trapcnt++;
            }
            if(Trapcnt>10)
            {
                BoolTrap = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Floor"))
        {
            jumpCount = 0;
        }

        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            //�_���[�W�t���O��false��������_���[�W
            if(CollisionFlag==false)
            PlayerDamage();
        }
        if(other.gameObject.CompareTag("Trap"))
        {
            BoolTrap = true;
            Trapcnt = 0;
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
        sp.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

}
