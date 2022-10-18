using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoonSPAttack : MonoBehaviour
{
    //子骸骨のプレハブ入れる用
    [SerializeField] private GameObject KidsBoon;
    //子骸骨を出す間隔と、それを数えるカウント
    private float BoonCnt;
    [SerializeField] private float BoonTiming = 1.0f;
    //ランダムに子骸骨を出すためにこの変数を座標に足し引きする
    private float RndPos;
    //子骸骨を出現させる位置
    private Vector3 BoonPos;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyObj", 6.0f);
    }

    // Update is called once per frame
    void Update()
    {
        BoonCnt += Time.deltaTime;
        if (BoonCnt > BoonTiming)
        {
            RndPos = Random.Range(-5.0f, 5.0f);
            BoonPos = new Vector3(this.transform.position.x + RndPos, this.transform.position.y, this.transform.position.z);
            Instantiate(KidsBoon, BoonPos, Quaternion.identity);
            BoonCnt = 0;
        }
    }

    private void DestroyObj()
    {
        Destroy(this.gameObject);
    }
}
