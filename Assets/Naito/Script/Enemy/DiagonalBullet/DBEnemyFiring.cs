using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBEnemyFiring : MonoBehaviour
{
    //�e�̃v���n�u�����p
    [SerializeField] private GameObject bullet;
    //�o���b�g�|�C���g�i�[�p
    [SerializeField] private Transform attackPoint;
    //�e�̏o���p�x
    float bulletcount;
    //�G��HP
    [SerializeField] private int EnemyHP = 5;
    //�����蔻��t���O
    private bool CollisionFlag = false;
    //�_�ŗp�ϐ�
    [SerializeField] private SpriteRenderer sp;
    [SerializeField] private EnemyHpBar enemyHp;
    [SerializeField] private float decreaseHp = 20.0f;
    [SerializeField] private GameObject PlayerPos;

    void Update()
    {
        bulletcount += Time.deltaTime;
        //�_���[�W������󂯂Ă���Ƃ��A�_�ł���
        if (CollisionFlag)
        {
            //Mathf.Abs�͐�Βl��Ԃ��AMathf.Sin�́{�Ȃ�P�C�|�Ȃ�0��Ԃ�
            float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            //��̎���0��1�����݂ɗ���̂ŁA����𓧖��x�ɓ���Ĕ��]�����Ă���
            sp.color = new Color(1.0f, 0.0f, 0.0f, level);
        }
        if (bulletcount>3.0f)
        {
            float Aimrad = GetAim();
            Instantiate(bullet, attackPoint.position, Quaternion.AngleAxis(Aimrad,Vector3.forward));
            bulletcount = 0.0f;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)

    {
        if(other.gameObject.CompareTag("PlayerAttackPoint"))
        {
            //�_���[�W�t���O��false��������_���[�W
            if (CollisionFlag == false)
            {
                EnemyDamage();
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            //�_���[�W�t���O��false��������_���[�W
            if (CollisionFlag == false)
            {
                EnemyDamage();
            }
        }
    }

    void EnemyDamage()
    {
        EnemyHP--;
        CollisionFlag = true;
        // �G�̗͌���
        enemyHp.DecHp(decreaseHp);
        //�_���[�W���肪�I�������A3�b��ɖ��G����������
        Invoke("InvincibleEnd", 3.0f);
    }

    void InvincibleEnd()
    {
        CollisionFlag = false;
        sp.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    private float GetAim()
    {
        Vector2 p1 = attackPoint.transform.position;
        Vector2 p2 = PlayerPos.transform.position;
        float dx = p2.x - p1.x;
        float dy = p2.y - p1.y;
        float rad = Mathf.Atan2(dy, dx);

        return rad * Mathf.Rad2Deg;
    }
}
