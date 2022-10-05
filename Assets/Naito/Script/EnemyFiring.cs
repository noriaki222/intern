using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFiring : MonoBehaviour
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
            Instantiate(bullet, attackPoint.position, Quaternion.identity);
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

    void EnemyDamage()
    {
        EnemyHP--;
        CollisionFlag = true;
        //�_���[�W���肪�I�������A3�b��ɖ��G����������
        Invoke("InvincibleEnd", 3.0f);
    }

    void InvincibleEnd()
    {
        CollisionFlag = false;
        sp.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
    }
}
