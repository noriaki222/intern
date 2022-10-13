using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private HitUI hit;
    [SerializeField] private Shake shake;
    [SerializeField] private AudioClip sound1;
    private Animator Sound;
    private bool HitFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        //ゲームオブジェクトを非アクティブ状態に
        gameObject.SetActive(false);
        Sound = GetComponent<Animator>();
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
        if(collision.gameObject.CompareTag("PlayerBullet"))
        {
            hit.AddHit();
            // 揺れる
            shake.PlayShake();
            Time.timeScale = 0.1f;
            Invoke("StopRelieve", 0.03f);
            
        }
    }

    private void StopRelieve()
    {
        Time.timeScale = 1.0f;
    }
}
