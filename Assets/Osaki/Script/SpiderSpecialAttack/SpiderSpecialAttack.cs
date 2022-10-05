using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 始点、終点座標の範囲
上：-12.0f < x < 12.0f, y = 6.0f; 
下：-12.0f < x < 12.0f, y = -6.0f;
左：x = -12.0f, -6.0f < y < 6.0f;
右：x = 12.0f, -6.0f < y < 6.0f;
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

    // 糸の始点と終点を決定し、ベクトルを作成し、
    // ベクトルに沿うように回転、糸の端を始点に移動
    private void CreateLine()
    {

    }

    // ベクトルにそって糸を始点から終点へ向かって移動させる
    private void MoveSpiderSilk()
    {

    }

    // ベクトルにそって子蜘蛛を移動させる
    private void AttackSmallSpider()
    {

    }
}
