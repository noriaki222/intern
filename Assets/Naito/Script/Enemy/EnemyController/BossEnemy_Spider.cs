using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy_Spider : MonoBehaviour
{
    //�ʏ�e�̃v���n�u�����p
    [SerializeField] private GameObject Bullet;
    //�ǔ��e�̃v���n�u�����p
    [SerializeField] private GameObject HomingBullet;
    //�_���e�̃v���n�u�����p
    [SerializeField] private GameObject SnipingBullet;
    //㩒e�̃v���n�u�����p
    [SerializeField] private GameObject TrapBullet;
    //�ΎR�̃v���n�u�����p
    [SerializeField] private GameObject Volcano;

    //�ʏ�e�̏o���ꏊ���i�[����p
    [SerializeField] private Transform BulletPoint;
    //�ǔ��e�̏o���ꏊ���i�[����p
    [SerializeField] private Transform HomingPoint;
    //�_���e�̏o���ꏊ���i�[����p
    [SerializeField] private Transform SnipingPoint;
    //㩒e�̏o���ꏊ���i�[����p
    [SerializeField] private Transform TrapPoint;

    //�e�e�̔��˂Ɏg���J�E���g
    private float Bulletcnt;
    private float HomingBulletcnt;
    private float SnipingBulletcnt;
    private float TrapBulletcnt;
    private float Volcanocnt;
    //�e�e�����o��̂ɂ����鎞��
    [SerializeField] private float BulletTiming = 3.0f;
    [SerializeField] private float HomingBulletTiming = 3.0f;
    [SerializeField] private float SnipingBulletTiming = 3.0f;
    [SerializeField] private float TrapBulletTiming = 3.0f;
    [SerializeField] private float VolcanoTiming = 3.0f;
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
    [SerializeField] private SpiderSpecialAttack SPattack;
    //�K�E�Z�t���O�i1�񂵂��ł��Ȃ��悤�ɂ��邽�߁j
    private bool SPFlag1 = false;
    private bool SPFlag2 = false;
    private bool SPFlag3 = false;

    //SE�o���p
    AudioSource audioSource;
    [SerializeField] private AudioClip sound1;
    [SerializeField] private AudioClip sound2;
    //�F�X�h�炷�p
    [SerializeField] private Shake shake;
    //�N���A���o�pTimeLine
    [SerializeField] private TimelinePlayer ClearLine;
    //�N���A���o�͈�x����
    private bool ClearFlag = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

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
            Bulletcnt += Time.deltaTime;
            HomingBulletcnt += Time.deltaTime;
            //�ʏ�e
            if(Bulletcnt>BulletTiming)
            {
                //������͎����ŉΎR�o������B�g�����������炱������
                //Instantiate(Volcano, new Vector3(PlayerPos.transform.position.x, PlayerPos.transform.position.y - 2.0f), Quaternion.identity);
                Instantiate(Bullet, BulletPoint.position, Quaternion.identity);
                audioSource.PlayOneShot(sound1);
                Bulletcnt = 0;
            }
            //�ǔ��e
            if(HomingBulletcnt > HomingBulletTiming)
            {
                BulletRnd = Random.Range(1, 4);
                if(BulletRnd == 3)
                {
                    Instantiate(HomingBullet, HomingPoint.position, Quaternion.identity);
                    audioSource.PlayOneShot(sound1);
                }
                HomingBulletcnt = 0;
            }
        }
        if(NowHP <= 70 && NowHP > 50)
        {
            Bulletcnt += Time.deltaTime;
            HomingBulletcnt += Time.deltaTime;
            SnipingBulletcnt += Time.deltaTime;
            TrapBulletcnt += Time.deltaTime;
            //�ʏ�e
            if (Bulletcnt > BulletTiming)
            {
                Instantiate(Bullet, BulletPoint.position, Quaternion.identity);
                audioSource.PlayOneShot(sound1);
                Bulletcnt = 0;
            }
            //�ǔ��e
            if (HomingBulletcnt > HomingBulletTiming)
            {
                BulletRnd = Random.Range(1, 3);
                if (BulletRnd == 2)
                {
                    Instantiate(HomingBullet, HomingPoint.position, Quaternion.identity);
                    audioSource.PlayOneShot(sound1);
                }
                HomingBulletcnt = 0;
            }
            //�_���e
            if(SnipingBulletcnt > SnipingBulletTiming)
            {
                BulletRnd = Random.Range(1, 5);
                if (BulletRnd == 4)
                {
                    float Snipingrad = GetAim();
                    Instantiate(SnipingBullet, SnipingPoint.position, Quaternion.AngleAxis(Snipingrad, Vector3.forward));
                }
                SnipingBulletcnt = 0;
            }
            //㩒e
            if(TrapBulletcnt>TrapBulletTiming)
            {
                BulletRnd = Random.Range(1, 5);
                if(BulletRnd == 4)
                {
                    float Aimrad = Random.Range(180.0f, 270.0f);
                    Instantiate(TrapBullet, TrapPoint.position, Quaternion.AngleAxis(Aimrad, Vector3.forward));
                }
                TrapBulletcnt = 0;
            }
        }
        if(NowHP <= 50 && NowHP > 0)
        {
            Bulletcnt += Time.deltaTime;
            HomingBulletcnt += Time.deltaTime;
            SnipingBulletcnt += Time.deltaTime;
            TrapBulletcnt += Time.deltaTime;
            //�ʏ�e
            if (Bulletcnt > BulletTiming)
            {
                BulletRnd = Random.Range(1, 3);
                if (BulletRnd == 1)
                {
                    Instantiate(Bullet, BulletPoint.position, Quaternion.identity);
                    audioSource.PlayOneShot(sound1);
                }
                else if (BulletRnd == 2)
                {
                    Instantiate(Bullet, BulletPoint.position, Quaternion.identity);
                    audioSource.PlayOneShot(sound1);
                    Invoke("FirstBullet", 0.7f);
                }
                Bulletcnt = 0;
            }
            //�ǔ��e
            if (HomingBulletcnt > HomingBulletTiming)
            {
                BulletRnd = Random.Range(1, 3);
                if (BulletRnd == 2)
                {
                    Instantiate(HomingBullet, HomingPoint.position, Quaternion.identity);
                    audioSource.PlayOneShot(sound1);
                }
                HomingBulletcnt = 0;
            }
            //�_���e
            if (SnipingBulletcnt > SnipingBulletTiming)
            {
                BulletRnd = Random.Range(1, 3);
                if (BulletRnd == 2)
                {
                    float Snipingrad = GetAim();
                    Instantiate(SnipingBullet, SnipingPoint.position, Quaternion.AngleAxis(Snipingrad, Vector3.forward));
                }
                SnipingBulletcnt = 0;
            }
            //㩒e
            if (TrapBulletcnt > TrapBulletTiming)
            {
                BulletRnd = Random.Range(1, 4);
                if (BulletRnd == 3)
                {
                    float Aimrad = Random.Range(180.0f, 270.0f);
                    Instantiate(TrapBullet, TrapPoint.position, Quaternion.AngleAxis(Aimrad, Vector3.forward));
                }
                TrapBulletcnt = 0;
            }
            if (NowHP==50.0f)
            {
                if(SPFlag1==false)
                {
                    SPattack.StartAttack();
                    SPFlag1 = true;
                }
            }
            if (NowHP == 30.0f)
            {
                if (SPFlag2 == false)
                {
                    SPattack.StartAttack();
                    SPFlag2 = true;
                }
            }
            if (NowHP == 10.0f)
            {
                if (SPFlag3 == false)
                {
                    SPattack.StartAttack();
                    SPFlag3 = true;
                }
            }
        }
        if(NowHP <= 0)
        {
            if (ClearFlag == false)
            {
                ClearLine.PlayTimeline();
                ClearFlag = true;
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
                EnemyDamage(10);
            }

        }
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("PlayerBullet"))
    //    {
    //        Debug.Log("��������");
    //        //�_���[�W�t���O��false��������_���[�W
    //        if (DamageFlag == false)
    //        {
    //            //EnemyDamage();
    //        }
    //        collision.gameObject.SetActive(false);
    //    }
    //}
    public void EnemyDamage(float damage)
    {
        if (DamageFlag == false)
        {
            DamageFlag = true;
            // �G�̗͌���
            enemyHp.DecHp(damage);
            audioSource.PlayOneShot(sound2);
            //�_���[�W���肪�I�������A3�b��ɖ��G����������
            Invoke("InvincibleEnd", 3.0f);
            shake.PlayShake(2, 0);
        }
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

    private void FirstBullet()
    {
        Instantiate(Bullet, BulletPoint.position, Quaternion.identity);
        audioSource.PlayOneShot(sound1);
        Invoke("LastBullet", 0.7f);
    }

    private void LastBullet()
    {
        Instantiate(Bullet, BulletPoint.position, Quaternion.identity);
        audioSource.PlayOneShot(sound1);
    }
}
