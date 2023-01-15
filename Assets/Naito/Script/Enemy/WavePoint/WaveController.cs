using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    //通常弾のプレハブ入れる用
    [SerializeField] private GameObject Bullet;
    //敵のプレハブ入れる用
    [SerializeField] private BossController_Spider Boss;

    //上下するスピード
    [SerializeField] private float PointSpeed;

    //折り返すのにかかる時間
    [SerializeField] private float DirectonSpeed;

    //各弾の発射に使うカウント
    private float Bulletcnt;
    //各弾が一回出るのにかかる時間
    [SerializeField] private float BulletTiming = 3.0f;

    //発射地点の初期位置代入用
    private Vector2 StartPos;

    //発射地点が折り返すためのフラグ
    private bool DirectonFlag;
    private bool ChangerFlag;



    // Start is called before the first frame update
    void Start()
    {
        StartPos = this.transform.position;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Bulletcnt += Time.deltaTime;
        if(Bulletcnt>BulletTiming)
        {
            Instantiate(Bullet, this.transform.position, Quaternion.identity);
            Bulletcnt = 0;
        }
    }

    void FixedUpdate()
    {
        if (DirectonFlag == false)
        {
            transform.Translate(0.0f, PointSpeed, 0.0f);
        }
        else
        {
            transform.Translate(0.0f, -PointSpeed, 0.0f);
        }
        if(ChangerFlag == false)
        {
            Invoke("DirectonChanger", DirectonSpeed);
            ChangerFlag = true;
        }

    }

    private void DirectonChanger()
    {
        if(DirectonFlag)
        {
            DirectonFlag = false;
        }
        else
        {
            DirectonFlag = true;
        }
        ChangerFlag = false;
    }

    public void WaveExistenceOn()
    {
        gameObject.SetActive(true);
        transform.position = StartPos;
        DirectonFlag = false;
        Bulletcnt = 0;
        Invoke("ExistenceOff", 10.0f);
    }

    private void ExistenceOff()
    {
        gameObject.SetActive(false);
        Invoke("EnemyAttackchanger", 2.0f);
    }

    private void EnemyAttackchanger()
    {
        Boss.AttackFlagChanger();
    }
}