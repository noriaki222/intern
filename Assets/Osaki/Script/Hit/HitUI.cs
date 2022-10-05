using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitUI : MonoBehaviour
{
    [SerializeField] private GameObject no;
    [SerializeField] private GameObject text;
    private HitUINum num;
    [SerializeField] private float ResetTime;
    private float timer = 0.0f;
    private int hit = 0;

    // Start is called before the first frame update
    void Start()
    {
        num = no.GetComponent<HitUINum>();
    }

    private void FixedUpdate()
    {
        if(hit > 0)
        {
            ++timer;
            no.SetActive(true);
            text.SetActive(true);
        }
        else
        {
            no.SetActive(false);
            text.SetActive(false);
        }

        if(timer > ResetTime)
        {
            timer = 0.0f;
            hit = 0;
        }
    }


    public void AddHit()
    {
        ++hit;
        timer = 0.0f;
        num.SetNum(hit);
        num.SetAnimFlg(true);
    }

    public int GetHitNum()
    {
        return hit;
    }
}
