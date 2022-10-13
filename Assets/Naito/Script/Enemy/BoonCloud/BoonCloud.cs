using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoonCloud : MonoBehaviour
{
    //弾のプレハブ入れる用
    [SerializeField] private GameObject bullet;
    //弾の出現頻度数える用
    private float cnt;
    [SerializeField] private float bulletcount=1.0f;
    //弾を出す場所をランダムに
    private float RndPos;
    //弾を出すところ用
    private Vector3 BoonPos;

    private void Start()
    {
        Invoke("DestroyObj",6.0f);
    }
    void Update()
    {
        cnt += Time.deltaTime;
        if(cnt>bulletcount)
        {
            RndPos = Random.Range(-5.0f, 5.0f);
            BoonPos =new  Vector3(this.transform.position.x + RndPos, this.transform.position.y, this.transform.position.z);
            Instantiate(bullet,BoonPos, Quaternion.identity);
            cnt = 0;
        }
    }

    private void DestroyObj()
    {
        Destroy(this.gameObject);
    }
}
