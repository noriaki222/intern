using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpideSpawn : MonoBehaviour
{
    //通常弾のプレハブ入れる用
    [SerializeField] private GameObject KidSpider;
    //敵のHPを管理しているUIを入れる用
    [SerializeField] private EnemyHpBar enemyHp;
    //各弾の発射に使うカウント
    private float Bulletcnt;
    //各弾が一回出るのにかかる時間
    [SerializeField] private float BulletTiming = 3.0f;
    //
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Bulletcnt += Time.deltaTime;
        if (Bulletcnt > BulletTiming)
        {
            Instantiate(KidSpider, this.transform.position, Quaternion.identity);
            Bulletcnt = 0;
        }
    }
    public void SpawnExistenceOn()
    {
        if (enemyHp.GetNowHp() <= 50)
        {
            gameObject.SetActive(true);
            Bulletcnt = 0;
            Invoke("ExistenceOff", 10.0f);
        }
    }

    private void ExistenceOff()
    {
        gameObject.SetActive(false);
    }
}
