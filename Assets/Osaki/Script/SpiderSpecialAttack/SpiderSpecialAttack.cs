using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private Vector3 startPos;
    private Vector3 endPos;

    private Vector3 line;

    private bool isStart = false;
    // Start is called before the first frame update
    void Start()
    {
        startPos = endPos = line = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartAttack()
    {
        isStart = true;
    }

    // ���̎n�_�ƏI�_�����肵�A�x�N�g�����쐬���A
    // �x�N�g���ɉ����悤�ɉ�]�A���̒[���n�_�Ɉړ�
    private void CreateLine()
    {

    }

    // �x�N�g���ɂ����Ď����n�_����I�_�֌������Ĉړ�������
    private void MoveSpiderSilk()
    {

    }

    // �x�N�g���ɂ����Ďq�w偂��ړ�������
    private void AttackSmallSpider()
    {

    }
}
