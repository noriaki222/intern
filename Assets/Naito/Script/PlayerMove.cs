using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�v���C���[�̈ړ��Ɋւ���X�N���v�g
public class PlayerMove : MonoBehaviour
{
    //�v���C���[�̈ړ����x
    public float speed = 8.0f;
    //�v���C���[�̈ړ��͈�
    //public float moveableRange = 30.0f;
    //�v���C���[�̃W�����v��
    public float power = 1.0f;
    //�v���C���[�̃��W�b�g�{�f�B
    private Rigidbody2D rbody2D;
    //�v���C���[���W�����v���Ă������̏���
    private int jumpCount = 0;

    private void Start()
    {
        rbody2D = GetComponent<Rigidbody2D>();
    }

    //Update is called once per frame
    void Update()
    {
        //���ړ�
        transform.Translate(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, 0, 0);
        //�W�����v
        if(Input.GetKeyDown(KeyCode.Space)&&this.jumpCount<1)
        {
            this.rbody2D.AddForce(transform.up * power);
            jumpCount++;
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
    }
}
