using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    [SerializeField] private float speed = 5; //�e�e�̃X�s�[�h

    [SerializeField] private float Reflectionspeed = 10; //���ˏe�e�̃X�s�[�h

    //�R���{���Z�p
    //[SerializeField] private HitUI hit;

    bool BulletRefection = false;

    void Start()
    {

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
            Vector3 lazerPos = transform.position; //Vector3�^��playerPos�Ɍ��݂̈ʒu�����i�[
            lazerPos.x += Reflectionspeed * Time.deltaTime; //x���W��speed�����Z
            transform.position = lazerPos; //���݂̈ʒu���ɔ��f������
            transform.localScale = new Vector3(-0.5f, 0.5f, 1);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player")|| collision.gameObject.CompareTag("Enemy"))
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerAttackPoint"))
        {
            BulletRefection = true;
            //hit.AddHit();
        }
    }
}