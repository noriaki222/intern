using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBar : MonoBehaviour
{
    [SerializeField] private Image now;
    [SerializeField] private Image gain;

    [SerializeField] private float maxHp = 100.0f;
    [SerializeField] private float rate = 0.1f;
    private float nowHp;
    // Start is called before the first frame update
    void Start()
    {
        nowHp = maxHp;   
    }

    private void FixedUpdate()
    {
        //Debug.Log(gain.fillAmount);
        now.fillAmount = nowHp / maxHp;

        var work = Mathf.Lerp(gain.fillAmount, now.fillAmount, rate);
        gain.fillAmount = work;
    }

    public void DecHp(float num)
    {
        if (nowHp - num > 0.0f)
        {
            nowHp -= num;
        }
        else
        {
            nowHp = 0.0f;
        }
    }

    public void SetMaxHp(float num)
    {
        if(num > 0)
        {
            maxHp = num;
            nowHp = maxHp;
        }
    }

    public void SetRate(float num)
    {
        if(num < 1.0f && num > 0.0f)
        {
            rate = num;
        }
    }

    public float GetNowHp()
    {
        return nowHp;
    }

    public void SetNowHp(float num)
    {
        if (num > 0.0f)
            nowHp = num;
    }
}