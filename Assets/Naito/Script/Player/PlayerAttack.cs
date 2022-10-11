using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private HitUI hit;
    // Start is called before the first frame update
    void Start()
    {
        //ゲームオブジェクトを非アクティブ状態に
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AttackAreaCreate()
    {
        gameObject.SetActive(true);
        Invoke("AttackAreaDereta", 0.1f);
    }

    void AttackAreaDereta()
    {
        //ゲームオブジェクトを非アクティブ状態に
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("EnemyBullet"))
        {
            hit.AddHit();
        }
    }
}
