using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kidsBoon : MonoBehaviour
{
    //プレイヤー入れる用
    private GameObject Player;
    //プレイヤーに向かって歩いてくる速度
    [SerializeField] private float KidsWalk = 3.0f;
    //子骸骨が動き出していいかどうか
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
