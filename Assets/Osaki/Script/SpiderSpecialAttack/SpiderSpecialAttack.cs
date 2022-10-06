using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
 �n�_�A�I�_���W�͈̔�
��F-12.0f < x < 12.0f, y = 6.0f; 
���F-12.0f < x < 12.0f, y = -6.0f;
���Fx = -12.0f, -6.0f < y < 6.0f;
�E�Fx = 12.0f, -6.0f < y < 6.0f;
 */

public class SpiderSpecialAttack : MonoBehaviour
{
    [SerializeField] private GameObject spiderSilk;
    [SerializeField] private GameObject start;
    [SerializeField] private GameObject offstart;
    [SerializeField] private GameObject offend;
    [SerializeField] private GameObject end;

    [SerializeField] private GameObject small_spider;
    [SerializeField] private float speed = 1.0f;
    private Vector3 startPos;   // �J�n���W
    private Vector3 endPos;     // �I�����W

    private Vector3 stopPos;    // �ꎞ��~���W

    // �U���X�e�[�g
    private enum STATE_SILK
    {
        CREATE,
        MOVE_SILK,
        ATTACK,
        DELETE,

        MAX_STATE,
        NONE
    };
    private STATE_SILK state = STATE_SILK.NONE;

    // Start is called before the first frame update
    void Start()
    {
        startPos = endPos = stopPos = Vector3.zero;
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case STATE_SILK.CREATE:
                CreateLine();
                state = STATE_SILK.MOVE_SILK;
                break;
            case STATE_SILK.MOVE_SILK:
                MoveSpiderSilk();
                if((spiderSilk.transform.position - stopPos).magnitude < 1.0f)
                    state = STATE_SILK.ATTACK;
                break;
            case STATE_SILK.ATTACK:
                AttackSmallSpider();
                //state = STATE_SILK.DELETE;
                break;
            case STATE_SILK.DELETE:
                DeleteSilk();
                state = STATE_SILK.NONE;
                break;
            default:
                break;
        }
    }

    public void StartAttack()
    {
        if(state == STATE_SILK.NONE)
        {
            state = STATE_SILK.CREATE;
        }
    }

    // ���̎n�_�ƏI�_�����肵�A�x�N�g�����쐬���A
    // �I�u�W�F�N�g���J�n�n�_�ֈړ�
    private void CreateLine()
    {
        UnityEngine.Random.InitState(DateTime.Now.Millisecond);
        // �n�_�ƏI�_������
        int work = UnityEngine.Random.Range(0, 4);
        startPos = GetRandPos(work);

        int work2 = UnityEngine.Random.Range(0, 4);
        while(work2 == work)
        {
            work2 = UnityEngine.Random.Range(0, 4);
        }

        endPos = GetRandPos(work2);

        // �p�x�����߂�
        float angle = GetAngle(startPos, endPos);

        // �I�u�W�F�N�g����]
        var rot = spiderSilk.transform.eulerAngles;
        rot.z = angle;
        spiderSilk.transform.eulerAngles = rot;

        // �I�u�W�F�N�g���ړ�
        float objLength = spiderSilk.transform.localScale.x;
        float off_x, off_y;
        float off_ex, off_ey;

        if(((endPos.y - startPos.y) / (endPos.x - startPos.x)) >= 0.0f)
        {
            //Debug.Log("�X���F��");
            if((endPos.x - startPos.x) >= 0.0f)
            {
                //Debug.Log("��������E��");
                off_x = startPos.x - ((objLength / 2) * Mathf.Abs(Mathf.Cos(angle * Mathf.Deg2Rad)));
                off_y = startPos.y - ((objLength / 2) * Mathf.Abs(Mathf.Sin(angle * Mathf.Deg2Rad)));
                off_ex = endPos.x + ((objLength / 2) * Mathf.Abs(Mathf.Cos(angle * Mathf.Deg2Rad)));
                off_ey = endPos.y + ((objLength / 2) * Mathf.Abs(Mathf.Sin(angle * Mathf.Deg2Rad)));
            }
            else
            {
                //Debug.Log("�E�ォ�獶��");
                off_x = startPos.x + ((objLength / 2) * Mathf.Abs(Mathf.Cos(angle * Mathf.Deg2Rad)));
                off_y = startPos.y + ((objLength / 2) * Mathf.Abs(Mathf.Sin(angle * Mathf.Deg2Rad)));
                off_ey = endPos.y - ((objLength / 2) * Mathf.Abs(Mathf.Sin(angle * Mathf.Deg2Rad)));
                off_ex = endPos.x - ((objLength / 2) * Mathf.Abs(Mathf.Cos(angle * Mathf.Deg2Rad)));
            }
        }
        else
        {
            //Debug.Log("�X���F��");
            if ((endPos.x - startPos.x) >= 0.0f)
            {
                //Debug.Log("���ォ��E��");
                off_x = startPos.x - ((objLength / 2) * Mathf.Abs(Mathf.Cos(angle * Mathf.Deg2Rad)));
                off_y = startPos.y + ((objLength / 2) * Mathf.Abs(Mathf.Sin(angle * Mathf.Deg2Rad)));
                off_ex = endPos.x + ((objLength / 2) * Mathf.Abs(Mathf.Cos(angle * Mathf.Deg2Rad)));
                off_ey = endPos.y - ((objLength / 2) * Mathf.Abs(Mathf.Sin(angle * Mathf.Deg2Rad)));
            }
            else
            {
                //Debug.Log("�E�����獶��");
                off_x = startPos.x + ((objLength / 2) * Mathf.Abs(Mathf.Cos(angle * Mathf.Deg2Rad)));
                off_y = startPos.y - ((objLength / 2) * Mathf.Abs(Mathf.Sin(angle * Mathf.Deg2Rad)));
                off_ex = endPos.x - ((objLength / 2) * Mathf.Abs(Mathf.Cos(angle * Mathf.Deg2Rad)));
                off_ey = endPos.y + ((objLength / 2) * Mathf.Abs(Mathf.Sin(angle * Mathf.Deg2Rad)));
            }
        }

        stopPos = new Vector3((endPos.x + startPos.x) / 2, (endPos.y + startPos.y) / 2, 0.0f);
        Debug.Log("��~���W�F" + stopPos);

        spiderSilk.transform.position = new Vector3(off_x, off_y, 0.0f);
        offstart.transform.position = new Vector3(off_x, off_y, 0.0f);
        offend.transform.position = new Vector3(off_ex, off_ey, 0.0f);

        start.transform.position = startPos;
        end.transform.position = endPos;

        small_spider.transform.position = offstart.transform.position;
    }

    // 2���W����p�x�����߂�
    private float GetAngle(Vector2 start, Vector2 target)
    {
        Vector2 dt = target - start;
        float rad = Mathf.Atan2(dt.y, dt.x);
        float degree = rad * Mathf.Rad2Deg;

        return degree;
    }

    // �x�N�g���ɂ����Ď����n�_����I�_�֌������Ĉړ�������
    private void MoveSpiderSilk()
    {
        spiderSilk.transform.position = Vector3.MoveTowards(spiderSilk.transform.position, stopPos, speed);
    }

    // �x�N�g���ɂ����Ďq�w偂��ړ�������
    private void AttackSmallSpider()
    {
        small_spider.transform.position = Vector3.MoveTowards(small_spider.transform.position, offend.transform.position, speed * 1.1f);
    }

    // �\�������������\����
    private void DeleteSilk()
    {

    }

    // ��ʒ[�̍��W��Ԃ�
    // �����F0�`3�ŉ�ʂ̏㉺���E���w��
    // �߂�l�F���W
    private Vector3 GetRandPos(int num)
    {
        Vector3 pos = Vector3.zero;
        switch(num)
        {
            case 0: // ��
                pos.x = UnityEngine.Random.Range(-12.0f, 12.0f);
                pos.y = 6.0f;
                break;
            case 1: // ��
                pos.x = UnityEngine.Random.Range(-12.0f, 12.0f);
                pos.y = -6.0f;
                break;
            case 2: // ��
                pos.x = -12.0f;
                pos.y = UnityEngine.Random.Range(-6.0f, 6.0f);
                break;
            case 3: // �E
                pos.x = 12.0f;
                pos.y = UnityEngine.Random.Range(-6.0f, 6.0f);
                break;
        }

        return pos;
    }
}
