using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitUI : MonoBehaviour
{
    [SerializeField] private GameObject no;
    [SerializeField] private GameObject text;
    [SerializeField] private GameObject back;

    [SerializeField] private Transform target;
    [SerializeField] private Camera cam;
    private HitUINum num;
    [SerializeField] private float ResetTime;
    private float timer = 0.0f;
    private int hit = 0;
    private bool finFade = false;
    private bool startFade = false;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        num = no.GetComponent<HitUINum>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        UpdateScreenPos();

        if(hit > 0)
        {
            ++timer;
            no.SetActive(true);
            text.SetActive(true);
            // back.SetActive(true);
        }
        else
        {
            no.SetActive(false);
            text.SetActive(false);
            back.SetActive(false);
        }

        if(timer > ResetTime && !startFade && !finFade)
        {
            startFade = true;
            anim.Play("HitFade", 0, 0);
        }

        if(finFade)
        {
            timer = 0.0f;
            hit = 0;
            finFade = false;
            finFade = false;
        }
    }


    public void AddHit()
    {
        ++hit;
        timer = 0.0f;
        num.SetNum(hit);
        num.SetAnimFlg(true);
        startFade = false;
        anim.Play("DefaultHit", 0, 0);
    }

    public int GetHitNum()
    {
        return hit;
    }

    public void ResetHit()
    {
        timer = 0.0f;
        hit = 0;
    }

    public void FinFade()
    {
        finFade = true;
    }

    private void UpdateScreenPos()
    {
        var targetWorldPos = target.position;
        var targetScreenPos = cam.WorldToScreenPoint(targetWorldPos);
        if (targetScreenPos.x < 510 && targetScreenPos.y > 900.0f)
        {
            transform.position = new Vector3(targetScreenPos.x, 900.0f, transform.position.z);
        }
        else
        {
            transform.position = targetScreenPos;
        }
    }
}
