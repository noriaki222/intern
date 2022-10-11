using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 対象のオブジェクトを揺らす
 * Parameter：揺らす対象のオブジェクト配列
 *          　振幅
 *            揺れる時間
 *            
 *            元の座標
 */

public class Shake : MonoBehaviour
{
    public struct ShaleInfo
    {
        float duration; // 時間
        float strenght; // 強さ
        float vibrate;  // 量
        Vector2 off;    // オフセット
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
