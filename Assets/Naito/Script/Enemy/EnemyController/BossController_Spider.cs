using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController_Spider : MonoBehaviour
{
    //�ʏ�e�̃v���n�u�����p
    [SerializeField] private GameObject Bullet;
    //�ǔ��e�̃v���n�u�����p
    [SerializeField] private GameObject HomingBullet;
    //�_���e�̃v���n�u�����p
    [SerializeField] private GameObject SnipingBullet;
    //�g�łe�̔��˃I�u�W�F�N�g�̃v���n�u�����p
    [SerializeField] private WaveController WaveObj;
    //�q�w偂��o��������I�u�W�F�N�g�̃v���n�u�����p
    [SerializeField] private SpideSpawn kidSpawn;
    //�ΎR�̃v���n�u�����p
    [SerializeField] private GameObject Volcano;

    //�ʏ�e�̏o���ꏊ���i�[����p
    [SerializeField] private Transform BulletPoint;
    //�ǔ��e�̏o���ꏊ���i�[����p
    [SerializeField] private Transform HomingPoint1;
    [SerializeField] private Transform HomingPoint2;
    [SerializeField] private Transform HomingPoint3;
    [SerializeField] private Transform HomingPoint4;
    [SerializeField] private Transform HomingPoint5;
    //�_���e�̏o���ꏊ���i�[����悤
    [SerializeField] private Transform SnipingPoint1;
    [SerializeField] private Transform SnipingPoint2;
    [SerializeField] private Transform SnipingPoint3;
    [SerializeField] private Transform SnipingPoint4;

    //�e�e�̔��˂Ɏg���J�E���g
    private float Bulletcnt;
    //�e�e������o�邩�̃J�E���g�p
    private int HomingCnt;
    private int SnipingCnt;
    //�e�e���o��܂ł̊Ԋu
    [SerializeField] private float HomingTime = 0.8f;
    [SerializeField] private float SnipingTime = 0.8f;
    //�_���ΏہiPlayer�j�̍��W������p
    [SerializeField] private GameObject PlayerPos;

    //�e�e�����o��̂ɂ����鎞��
    [SerializeField] private float BulletTiming = 3.0f;

    //�_���[�W��H����Ă��邩�ǂ����̃t���O
    private bool DamageFlag = false;
    //�_�ŗp�ɓ_�ł��������I�u�W�F�N�g������
    [SerializeField] private SpriteRenderer sp;
    //�G�̌��݂�HP���i�[����p
    [SerializeField] private float NowHP;
    //�G��HP���Ǘ����Ă���UI������p
    [SerializeField] private EnemyHpBar enemyHp;

    //�U���p�^�[���ύX�p
    private int AttackChanger = 0;
    //�f�o�b�N���[�h�̐؂�ւ��p
    private bool DebugFlag = false;
    //�G���U�����Ă��邩�ǂ����̔��f������p
    private bool AttackFlag = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //����HP���i�[
        NowHP = enemyHp.GetNowHp();
        if(AttackFlag==false)
        {
            AttackChanger = Random.Range(1, 4);
        }
        //�_���[�W������󂯂Ă���Ƃ��A�_�ł���
        if (DamageFlag)
        {
            //Mathf.Abs�͐�Βl��Ԃ��AMathf.Sin�́{�Ȃ�P�C�|�Ȃ�0��Ԃ�
            float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            //��̎���0��1�����݂ɗ���̂ŁA����𓧖��x�ɓ���Ĕ��]�����Ă���
            sp.color = new Color(1.0f, 0.0f, 0.0f, level);
        }

        //if (Bulletcnt > BulletTiming)
        //{
        //    Instantiate(Bullet, BulletPoint.position, Quaternion.identity);
        //    Bulletcnt = 0;
        //}

        //�G�̍U���p�^�[���Ǘ�
        switch (AttackChanger)
        {
            case 1:
                HomingBulletTable1();
                AttackFlagChanger();
                AttackChanger = 0;
                break;
            case 2:
                WaveBulletTable();
                AttackFlagChanger();
                AttackChanger = 0;
                break;
            case 3:
                SnipingBulletTable1();
                AttackFlagChanger();
                AttackChanger = 0;
                break;
        }
        //�f�o�b�N�p�ɐ����L�[�ōU����ω���������
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AttackChanger = 1;
            HomingCnt = 0;
        }
        //�f�o�b�N�p�ɐ����L�[�ōU����ω���������
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AttackChanger = 2;
        }
        //�f�o�b�N�p�ɐ����L�[�ōU����ω���������
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            AttackChanger = 3;
        }
    }

    public void EnemyDamage(float damage)
    {
        if (DamageFlag == false)
        {
            DamageFlag = true;
            // �G�̗͌���
            //enemyHp.DecHp(damage);
            //audioSource.PlayOneShot(sound2);
            //�_���[�W���肪�I�������A3�b��ɖ��G����������
            Invoke("InvincibleEnd", 3.0f);
            //shake.PlayShake(2, 0);
        }
    }

    void InvincibleEnd()
    {
        DamageFlag = false;
        sp.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    private void HomingBulletTable1()
    {
        HomingBulletCreate(HomingPoint1.position);
        Invoke("HomingBulletTable2", HomingTime);
        HomingCnt += 1;
    }

    private void HomingBulletTable2()
    {
        HomingBulletCreate(HomingPoint2.position);
        Invoke("HomingBulletTable3", HomingTime);
        HomingCnt += 1;
    }

    private void HomingBulletTable3()
    {
        HomingBulletCreate(HomingPoint3.position);
        Invoke("HomingBulletTable4", HomingTime + 1.0f);
        HomingCnt += 1;
    }

    private void HomingBulletTable4()
    {
        HomingBulletCreate(HomingPoint4.position);
        Invoke("HomingBulletTable5", HomingTime);
        HomingCnt += 1;
    }

    private void HomingBulletTable5()
    {
        HomingBulletCreate(HomingPoint5.position);
        if (NowHP <= 50 && NowHP > 0)
        {
            Instantiate(Volcano, new Vector3(PlayerPos.transform.position.x, PlayerPos.transform.position.y - 2.0f), Quaternion.identity);
        }
        HomingCnt += 1;
        if (HomingCnt < 15)
        {
            Invoke("HomingBulletTable1", HomingTime + 1.0f);
        }
        else
        {
            Invoke("AttackFlagChanger", 1.0f);
        }
    }



    private void WaveBulletTable()
    {
        WaveObj.WaveExistenceOn();
        kidSpawn.SpawnExistenceOn();
    }

    private void HomingBulletCreate(Vector3 position)
    {
        Instantiate(HomingBullet, position, Quaternion.identity);
    }

    private void SnipingBulletTable1()
    {
        float AimRad = GetAim(SnipingPoint1);
        Instantiate(SnipingBullet, SnipingPoint1.position, Quaternion.AngleAxis(AimRad, Vector3.forward));
        Invoke("SnipingBulletTable2", SnipingTime);
        SnipingCnt += 1;
    }

    private void SnipingBulletTable2()
    {
        float AimRad = GetAim(SnipingPoint2);
        Instantiate(SnipingBullet, SnipingPoint2.position, Quaternion.AngleAxis(AimRad, Vector3.forward));
        Invoke("SnipingBulletTable3", SnipingTime);
        SnipingCnt += 1;
    }
    private void SnipingBulletTable3()
    {
        float AimRad = GetAim(SnipingPoint3);
        Instantiate(SnipingBullet, SnipingPoint3.position, Quaternion.AngleAxis(AimRad, Vector3.forward));
        Invoke("SnipingBulletTable4", SnipingTime);
        SnipingCnt += 1;
    }

    private void SnipingBulletTable4()
    {
        float AimRad = GetAim(SnipingPoint4);
        Instantiate(SnipingBullet, SnipingPoint4.position, Quaternion.AngleAxis(AimRad, Vector3.forward));
        SnipingCnt += 1;
        if(SnipingCnt < 12)
        {
            Invoke("SnipingBulletTable1", SnipingTime);
        }
        else
        {
            Invoke("AttackFlagChanger", 1.0f);
        }
    }

    private float GetAim(Transform SnipingPoint)
    {
        Vector2 p1 = SnipingPoint.position;
        Vector2 p2 = PlayerPos.transform.position;
        float dx = p2.x - p1.x;
        float dy = p2.y - p1.y;
        float rad = Mathf.Atan2(dy, dx);

        return rad * Mathf.Rad2Deg;
    }

    public void AttackFlagChanger()
    {
        if(AttackFlag)
        {
            AttackFlag = false;
        }
        else
        {
            AttackFlag = true;
        }
    }
}
