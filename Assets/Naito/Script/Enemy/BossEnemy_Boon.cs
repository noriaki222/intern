using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy_Boon : MonoBehaviour
{
    //�_���e�̃v���n�u�����p
    [SerializeField] private GameObject SnipingBullet;
    //���_�̃v���n�u�����p
    [SerializeField] private GameObject BoonCloud;

    //�_���e�̏o���ꏊ���i�[����p
    [SerializeField] private Transform SnipingPoint;
    //���_�̏o���ꏊ���i�[����p
    [SerializeField] private Transform TrapPoint;

    //�e�̔��˂Ɏg���J�E���g
    private float Bulletcnt;
    //�e�����o��̂ɂ����鎞��
    [SerializeField] private float BulletTiming = 3.0f;
    //�_���ΏہiPlayer�j�̍��W������p
    [SerializeField] private GameObject PlayerPos;

    //�_���[�W��H����Ă��邩�ǂ����̃t���O
    private bool DamageFlag = false;
    //�_�ŗp�ɓ_�ł��������I�u�W�F�N�g������
    [SerializeField] private SpriteRenderer sp;
    //�G��HP���Ǘ����Ă���UI������p
    [SerializeField] private EnemyHpBar enemyHp;
    //�_���[�W��H������Ƃ��A�ǂꂭ�炢HP�����邩�����
    [SerializeField] private float decreaseHp = 5.0f;
    //�G�̌��݂�HP���i�[����p
    private float NowHP;

    // Update is called once per frame
    void Update()
    {
        //�}�C�t���[���J�E���g�𑝉�
        Bulletcnt += Time.deltaTime;
        //����HP���i�[
        NowHP = enemyHp.GetNowHp();
        //�_���[�W������󂯂Ă���Ƃ��A�_�ł���
        if (DamageFlag)
        {
            //Mathf.Abs�͐�Βl��Ԃ��AMathf.Sin�́{�Ȃ�P�C�|�Ȃ�0��Ԃ�
            float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            //��̎���0��1�����݂ɗ���̂ŁA����𓧖��x�ɓ���Ĕ��]�����Ă���
            sp.color = new Color(1.0f, 0.0f, 0.0f, level);
        }
        if (Bulletcnt > BulletTiming)
        {
            //�ʏ�e
            Instantiate(Bullet, BulletPoint.position, Quaternion.identity);
            if (NowHP <= 85.0f)
            {
                //�ǔ��e
                Instantiate(HomingBullet, HomingPoint.position, Quaternion.identity);
            }
            if(NowHP <= 50.0f)
            {
                //�_���e
                Instantiate(SnipingBullet, SnipingPoint.position, Quaternion.AngleAxis(GetAim(), Vector3.forward));
            }
            if(NowHP <= 30)
            {
                float Aimrad = Random.Range(180.0f, 270.0f);
                //�_���e
                Instantiate(TrapBullet, TrapPoint.position, Quaternion.AngleAxis(Aimrad, Vector3.forward));
            }
            Bulletcnt = 0.0f;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerAttackPoint"))
        {
            //�_���[�W�t���O��false��������_���[�W
            if (DamageFlag == false)
            {
                EnemyDamage();
            }

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            //�_���[�W�t���O��false��������_���[�W
            if (DamageFlag == false)
            {
                EnemyDamage();
            }
        }
    }
    void EnemyDamage()
    {
        DamageFlag = true;
        // �G�̗͌���
        enemyHp.DecHp(decreaseHp);
        //�_���[�W���肪�I�������A3�b��ɖ��G����������
        Invoke("InvincibleEnd", 3.0f);
    }
    void InvincibleEnd()
    {
        DamageFlag = false;
        sp.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }
    private float GetAim()
    {
        Vector2 p1 = SnipingPoint.transform.position;
        Vector2 p2 = PlayerPos.transform.position;
        float dx = p2.x - p1.x;
        float dy = p2.y - p1.y;
        float rad = Mathf.Atan2(dy, dx);

        return rad * Mathf.Rad2Deg;
    }
}
