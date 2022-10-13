using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kidsBoon : MonoBehaviour
{
    //�v���C���[�����p
    private GameObject Player;
    //�v���C���[�Ɍ������ĕ����Ă��鑬�x
    [SerializeField] private float KidsWalk = 3.0f;
    //�q�[���������o���Ă������ǂ���
    private bool BoonFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("BoonStart", 3.0f);
        Player = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        if (BoonFlag == true)
        {
            if (transform.position.x < Player.transform.position.x)
            {
                transform.Translate(KidsWalk * Time.deltaTime, 0, 0);
            }
            else
            {
                transform.Translate(-KidsWalk * Time.deltaTime, 0, 0);
            }
        }
    }

    private void BoonStart()
    {
        BoonFlag = true;
        this.gameObject.layer = 13;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAttackPoint"))
        {
            Destroy(this.gameObject);
        }
    }
}
