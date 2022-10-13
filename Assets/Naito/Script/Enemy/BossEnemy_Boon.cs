using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy_Boon : MonoBehaviour
{
    //�_���e�̃v���n�u�����p
    [SerializeField] private GameObject SnipingBullet;
    //���_�̃v���n�u�����p
    [SerializeField] private GameObject BoonCloud;
    //�挊�̃v���n�u�����p
    [SerializeField] private GameObject Grave;

    //�_���e�̏o���ꏊ���i�[����p
    [SerializeField] private Transform SnipingPoint;
    //���_�̏o���ꏊ���i�[����p
    [SerializeField] private Transform CloudPoint;
    //�挊�̏o���ꏊ���i�[����p
    [SerializeField] private Transform GravePoint;

    //�e�e�̔��˂Ɏg���J�E���g
    private float SnipingBulletcnt;
    private float BoonCloudcnt;
    //�e�e�����o��̂ɂ����鎞��
    [SerializeField] private float SnipingBulletTiming = 3.0f;
    [SerializeField] private float BoonCloudTiming = 3.0f;
    //�e�e���o�邩�o�Ȃ����̊m��
    private int BulletRnd;
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
    [SerializeField] private float NowHP;
    //�K�E�Z
    //[SerializeField] private SpiderSpecialAttack SPattack;
    //�K�E�Z�t���O�i1�񂵂��ł��Ȃ��悤�ɂ��邽�߁j
    private bool SPFlag1 = false;
    private bool SPFlag2 = false;
    //private bool SPFlag3 = false;

    // Update is called once per frame
    void Update()
    {
        Debug.Log("DamageFlag" + DamageFlag);
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
        if(NowHP > 70.0f)
        {
            SnipingBulletcnt += Time.deltaTime;
            //�_���e
            if (SnipingBulletcnt > SnipingBulletTiming)
            {
                float Snipingrad = GetAim();
                Instantiate(SnipingBullet, SnipingPoint.position, Quaternion.AngleAxis(Snipingrad, Vector3.forward));
                
                SnipingBulletcnt = 0;
            }
        }
        if(NowHP <= 70 && NowHP > 50)
        {
            SnipingBulletcnt += Time.deltaTime;
            BoonCloudcnt += Time.deltaTime;
            //�_���e
            if (SnipingBulletcnt > SnipingBulletTiming)
            {
                float Snipingrad = GetAim();
                Instantiate(SnipingBullet, SnipingPoint.position, Quaternion.AngleAxis(Snipingrad, Vector3.forward));

                SnipingBulletcnt = 0;
            }
            //���_
            if(BoonCloudcnt > BoonCloudTiming)
            {
                BulletRnd = Random.Range(1, 4);
                if(BulletRnd == 3)
                {
                    Instantiate(BoonCloud, CloudPoint.position, Quaternion.identity);
                }
                BoonCloudcnt = 0;
            }
            if(NowHP == 70)
            {
                if(SPFlag1==false)
                {
                    Instantiate(Grave, GravePoint.position, Quaternion.identity);
                    SPFlag1 = true;
                }
            }
        }
        if(NowHP <= 50 && NowHP > 40)
        {
            SnipingBulletcnt += Time.deltaTime;
            BoonCloudcnt += Time.deltaTime;
            //�_���e
            if (SnipingBulletcnt > SnipingBulletTiming)
            {
                BulletRnd = Random.Range(1, 3);
                if (BulletRnd == 1)
                {
                    float Snipingrad = GetAim();
                    Instantiate(SnipingBullet, SnipingPoint.position, Quaternion.AngleAxis(Snipingrad, Vector3.forward));
                }
                else
                {
                    float Snipingrad = GetAim();
                    Instantiate(SnipingBullet, SnipingPoint.position, Quaternion.AngleAxis(Snipingrad, Vector3.forward));
                    Invoke("LastBullet", 0.5f);
                }
                SnipingBulletcnt = 0;
            }
            //���_
            if (BoonCloudcnt > BoonCloudTiming)
            {
                BulletRnd = Random.Range(1, 4);
                if (BulletRnd == 3)
                {
                    Instantiate(BoonCloud, CloudPoint.position, Quaternion.identity);
                }
                BoonCloudcnt = 0;
            }
        }
        if (NowHP <= 40 && NowHP > 0)
        {
            SnipingBulletcnt += Time.deltaTime;
            BoonCloudcnt += Time.deltaTime;
            //�_���e
            if (SnipingBulletcnt > SnipingBulletTiming)
            {
                BulletRnd = Random.Range(1, 3);
                if (BulletRnd == 1)
                {
                    float Snipingrad = GetAim();
                    Instantiate(SnipingBullet, SnipingPoint.position, Quaternion.AngleAxis(Snipingrad, Vector3.forward));
                }
                else
                {
                    float Snipingrad = GetAim();
                    Instantiate(SnipingBullet, SnipingPoint.position, Quaternion.AngleAxis(Snipingrad, Vector3.forward));
                    Invoke("LastBullet", 0.5f);
                }
                SnipingBulletcnt = 0;
            }
            //���_
            if (BoonCloudcnt > BoonCloudTiming)
            {
                BulletRnd = Random.Range(1, 3);
                if (BulletRnd == 2)
                {
                    Instantiate(BoonCloud, CloudPoint.position, Quaternion.identity);
                }
                BoonCloudcnt = 0;
            }
            if (NowHP == 30)
            {
                if (SPFlag2 == false)
                {
                    Instantiate(Grave, GravePoint.position, Quaternion.identity);
                    SPFlag2 = true;
                }
            }
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
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            //�_���[�W�t���O��false��������_���[�W
            if (DamageFlag == false)
            {
                EnemyDamage();
            }
            collision.gameObject.SetActive(false);
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

    private void LastBullet()
    {
        float Snipingrad = GetAim();
        Instantiate(SnipingBullet, SnipingPoint.position, Quaternion.AngleAxis(Snipingrad, Vector3.forward));
    }
}
