using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volcano : MonoBehaviour
{
    //プレイヤー入れる用
    private GameObject Player;
    //噴火を止めるかどうかのフラグ
    private bool StartFlag = false;
    //火柱オブジェクト
    [SerializeField] private GameObject PillarFire;
    //噴火開始するためのカウント用
    private float VolcanoCnt;
    //噴火タイミング
    [SerializeField] private float VolcanoTiming = 1.0f;
    //噴火していいかのフラグ
    private bool PillarFireFlag = false;
    //火柱出現間隔
    [SerializeField] private float PillarFireTiming = 0.1f;
    //出現カウント用
    private float PillarFirecnt;
    //火柱エフェクト用
    [SerializeField] private GameObject Effact;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("VolcanoStart", 2.0f);
        Invoke("VolcanoEnd", 8.0f);
        Player = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        if (PillarFireFlag == false)
        {
            if (StartFlag == false)
            {
                if (this.transform.position.x != Player.transform.position.x)
                {
                    Vector3 VolcanoTrans = transform.position;
                    VolcanoTrans.x = Player.transform.position.x;
                    transform.position = VolcanoTrans;
                }
            }
            else
            {
                VolcanoCnt += Time.deltaTime;
                if(VolcanoCnt>VolcanoTiming)
                {
                    PillarFireFlag = true;
                    Instantiate(Effact, transform.position, Quaternion.identity);
                }
            }
        }
        else
        { 
            PillarFirecnt += Time.deltaTime;
            if (PillarFirecnt > PillarFireTiming)
            {
                Instantiate(PillarFire, transform.position, Quaternion.identity);
                PillarFirecnt = 0.0f;
            }
        }
    }

    void VolcanoStart()
    {
        StartFlag = true;
    }

    void VolcanoEnd()
    {
        this.gameObject.SetActive(false);
    }
}
