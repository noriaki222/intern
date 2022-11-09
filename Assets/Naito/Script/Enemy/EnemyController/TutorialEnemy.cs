using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemy : MonoBehaviour
{
    //�ʏ�e�̃v���n�u�����p
    [SerializeField] private GameObject Bullet;

    //�ʏ�e�̏o���ꏊ���i�[����p
    [SerializeField] private Transform BulletPoint;

    //�e�e�̔��˂Ɏg���J�E���g
    private float Bulletcnt;

    //�e�e�����o��̂ɂ����鎞��
    [SerializeField] private float BulletTiming = 3.0f;

    //�_���[�W��H����Ă��邩�ǂ����̃t���O
    private bool DamageFlag = false;
    //�_�ŗp�ɓ_�ł��������I�u�W�F�N�g������
    [SerializeField] private SpriteRenderer sp;
    //�G��HP���Ǘ����Ă���UI������p
    [SerializeField] private EnemyHpBar enemyHp;
    //�G�̌��݂�HP���i�[����p
    [SerializeField] private float NowHP;

    //SE�o���p
    AudioSource audioSource;
    [SerializeField] private AudioClip sound1;
    [SerializeField] private AudioClip sound2;
    //�F�X�h�炷�p
    [SerializeField] private Shake shake;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
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
        Bulletcnt += Time.deltaTime;
        //�ʏ�e
        if (Bulletcnt > BulletTiming)
        {
            Instantiate(Bullet, BulletPoint.position, Quaternion.identity);
            audioSource.PlayOneShot(sound1);
            Bulletcnt = 0;
        }
    }

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

}
