using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private HitUI hit;
    [SerializeField] private Shake shake;
    private bool HitFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        //�Q�[���I�u�W�F�N�g���A�N�e�B�u��Ԃ�
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
        //�Q�[���I�u�W�F�N�g���A�N�e�B�u��Ԃ�
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("PlayerBullet"))
        {
            hit.AddHit();
            // �h���
            shake.PlayShake();
            HitFlag = true;
            Invoke("StopRelieve", 0.5f);
        }
    }

    private void StopRelieve()
    {
        HitFlag = false;
    }

    public bool GetHitStop()
    {
        return HitFlag;
    }
}
