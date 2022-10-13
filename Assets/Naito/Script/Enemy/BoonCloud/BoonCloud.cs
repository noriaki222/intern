using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoonCloud : MonoBehaviour
{
    //�e�̃v���n�u�����p
    [SerializeField] private GameObject bullet;
    //�e�̏o���p�x������p
    private float cnt;
    [SerializeField] private float bulletcount=1.0f;
    //�e���o���ꏊ�������_����
    private float RndPos;
    //�e���o���Ƃ���p
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
