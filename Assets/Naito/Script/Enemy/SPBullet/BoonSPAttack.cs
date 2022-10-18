using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoonSPAttack : MonoBehaviour
{
    //�q�[���̃v���n�u�����p
    [SerializeField] private GameObject KidsBoon;
    //�q�[�����o���Ԋu�ƁA����𐔂���J�E���g
    private float BoonCnt;
    [SerializeField] private float BoonTiming = 1.0f;
    //�����_���Ɏq�[�����o�����߂ɂ��̕ϐ������W�ɑ�����������
    private float RndPos;
    //�q�[�����o��������ʒu
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
