using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpideSpawn : MonoBehaviour
{
    //�ʏ�e�̃v���n�u�����p
    [SerializeField] private GameObject KidSpider;
    //�G��HP���Ǘ����Ă���UI������p
    [SerializeField] private EnemyHpBar enemyHp;
    //�e�e�̔��˂Ɏg���J�E���g
    private float Bulletcnt;
    //�e�e�����o��̂ɂ����鎞��
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
