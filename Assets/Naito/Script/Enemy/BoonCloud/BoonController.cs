using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoonController : MonoBehaviour
{

    [SerializeField] private float Reflectionspeed = 10; //���ˏe�e�̃X�s�[�h

    private Rigidbody2D rd;

    bool BulletRefection = false;
    private void Start()
    {
        rd = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (BulletRefection)
        {
            Vector3 lazerPos = transform.position; //Vector3�^��playerPos�Ɍ��݂̈ʒu�����i�[
            lazerPos.x += Reflectionspeed * Time.deltaTime; //x���W��speed�����Z
            transform.position = lazerPos; //���݂̈ʒu���ɔ��f������
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerAttackPoint"))
        {
            BulletRefection = true;
            rd.velocity = Vector3.zero;
            this.gameObject.tag = "PlayerBullet";
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor")|| other.gameObject.CompareTag("Enemy")|| other.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
