using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderSpecialParam : MonoBehaviour
{
    public GameObject spiderSilk;     // 糸
    public GameObject small_spider;   // 子蜘蛛

    public Vector3 startPos = Vector3.zero;   // 開始座標
    public Vector3 endPos = Vector3.zero;     // 終了座標
    public Vector3 off_start = Vector3.zero;
    public Vector3 off_end = Vector3.zero;
    public Vector3 stopPos = Vector3.zero;    // 一時停止座標
}
