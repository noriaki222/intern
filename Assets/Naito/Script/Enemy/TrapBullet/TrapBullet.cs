using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBullet : MonoBehaviour
{

    [SerializeField] private float speed = 5; //銃弾のスピード
    //罠設置用
    [SerializeField] private GameObject Trap;


    // Start is called before the first frame update
    void Start()
    {
        Invoke("TrapCreate", 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //弾移動
        transform.Translate(speed * Time.deltaTime, 0, 0);
    }

    void TrapCreate()
    {
        Instantiate(Trap, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerAttackPoint"))
        {
            TrapCreate();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Floor")||collision.gameObject.CompareTag("Player"))
        {
            TrapCreate();
        }
    }
}
