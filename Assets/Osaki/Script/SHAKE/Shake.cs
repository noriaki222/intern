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
    [System.Serializable]
    public struct ShakeInfo
    {
       public float duration; // 時間
       public float strenght; // 強さ
       public float vibrate;  // 大きさ
       [HideInInspector]
       public Vector2 off;    // オフセット
    }

    [SerializeField] private ShakeInfo[] shakeInfo;
    [SerializeField] private GameObject[] targets;
    private List<Vector3> initPos = new List<Vector3>();

    private bool[] isShake;   // 振動しているか
    private float[] totalTime;        // 経過時間
    private int[] shakeType;    // shakeInfoのタイプ

    // Start is called before the first frame update
    void Start()
    {
        totalTime = new float[targets.Length];
        isShake = new bool[targets.Length];
        shakeType = new int[targets.Length];
        for(int i = 0; i < targets.Length; ++i)
        {
            initPos.Add(targets[i].transform.position);
            totalTime[i] = 0.0f;
            shakeType[i] = 0;
            isShake[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < targets.Length; ++i)
        {
            if (!isShake[i])
                continue;

            // 揺れる
            targets[i].transform.position = GetUpdateShakePos(shakeInfo[shakeType[i]], totalTime[i], initPos[i]);

            // 時間経過後初期位置へ
            totalTime[i] += Time.deltaTime;
            if(totalTime[i] > shakeInfo[shakeType[i]].duration)
            {
                isShake[i] = false;
                totalTime[i] = 0.0f;

                // 初期位置へ
                targets[i].transform.position = initPos[i];
            }
        }
    }

    public void PlayShake(int element = -1, int type = 0)
    {
        if(element < 0 || element >= isShake.Length)
        {
            for(int i = 0; i < isShake.Length; ++i)
            {
                isShake[i] = true;
                if(type < shakeInfo.Length && type >= 0)
                    shakeType[i] = type;
            }
        }
        else
        {
            isShake[element] = true;
            if (type < shakeInfo.Length && type >= 0)
                shakeType[element] = type;
        }

        for(int i = 0; i < shakeInfo.Length; ++i)
        {
            shakeInfo[i].off = new Vector2(Random.Range(0.0f, 100.0f), Random.Range(0.0f, 100.0f)); 
        }
    }

    private Vector3 GetUpdateShakePos(ShakeInfo info, float totalTime, Vector3 initPos)
    {
        // パーリンノイズ値取得
        float randomX = GetPerlinNoiseValue(info.off.x, info.strenght, totalTime);
        float randomY = GetPerlinNoiseValue(info.off.y, info.strenght, totalTime);
        randomX *= info.strenght;
        randomY *= info.strenght;

        float rate = 1.0f - totalTime / info.duration;
        randomX = Mathf.Clamp(randomX, -info.vibrate * rate, info.vibrate * rate);
        randomY = Mathf.Clamp(randomY, -info.vibrate * rate, info.vibrate * rate);

        var pos = initPos;
        pos.x += randomX;
        pos.y += randomY;

        return pos;
    }

    private float GetPerlinNoiseValue(float off, float speed, float time)
    {
        // Mathf.PerlinNoise(x, y)  2D平面上に生成されたfloat値の擬似ランダムパターン
        // 戻り値：0.0f～1.0fの範囲内の値
        // x, y:座標　同じ座標を指定すれば同じ値が帰ってくる。平面は基本的に無限。
        var perlinNoise = Mathf.PerlinNoise(off + speed * time, 0.0f);
        // 0.0f ～ 1.0f を -1.0f ～ 1.0fに変換
        return (perlinNoise - 0.5f) * 2.0f;
    }
}
