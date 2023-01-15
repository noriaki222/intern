using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    //�ʏ�e�̃v���n�u�����p
    [SerializeField] private GameObject Bullet;
    //�G�̃v���n�u�����p
    [SerializeField] private BossController_Spider Boss;

    //�㉺����X�s�[�h
    [SerializeField] private float PointSpeed;

    //�܂�Ԃ��̂ɂ����鎞��
    [SerializeField] private float DirectonSpeed;

    //�e�e�̔��˂Ɏg���J�E���g
    private float Bulletcnt;
    //�e�e�����o��̂ɂ����鎞��
    [SerializeField] private float BulletTiming = 3.0f;

    //���˒n�_�̏����ʒu����p
    private Vector2 StartPos;

    //���˒n�_���܂�Ԃ����߂̃t���O
    private bool DirectonFlag;
    private bool ChangerFlag;



    // Start is called before the first frame update
    void Start()
    {
        StartPos = this.transform.position;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Bulletcnt += Time.deltaTime;
        if(Bulletcnt>BulletTiming)
        {
            Instantiate(Bullet, this.transform.position, Quaternion.identity);
            Bulletcnt = 0;
        }
    }

    void FixedUpdate()
    {
        if (DirectonFlag == false)
        {
            transform.Translate(0.0f, PointSpeed, 0.0f);
        }
        else
        {
            transform.Translate(0.0f, -PointSpeed, 0.0f);
        }
        if(ChangerFlag == false)
        {
            Invoke("DirectonChanger", DirectonSpeed);
            ChangerFlag = true;
        }

    }

    private void DirectonChanger()
    {
        if(DirectonFlag)
        {
            DirectonFlag = false;
        }
        else
        {
            DirectonFlag = true;
        }
        ChangerFlag = false;
    }

    public void WaveExistenceOn()
    {
        gameObject.SetActive(true);
        transform.position = StartPos;
        DirectonFlag = false;
        Bulletcnt = 0;
        Invoke("ExistenceOff", 10.0f);
    }

    private void ExistenceOff()
    {
        gameObject.SetActive(false);
        Invoke("EnemyAttackchanger", 2.0f);
    }

    private void EnemyAttackchanger()
    {
        Boss.AttackFlagChanger();
    }
}