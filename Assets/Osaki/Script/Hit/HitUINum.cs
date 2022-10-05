using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitUINum : MonoBehaviour
{
    private bool animFlg = false;
    private int num = 0;

    private Text text;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        text.text = num.ToString();

        if(animFlg == true)
        {
            anim.Play("HitAnim", 0, 0);
            animFlg = false;
        }
    }

    public void SetNum(int no)
    {
        num = no;
    }

    public void SetAnimFlg(bool flg)
    {
        animFlg = flg;
    }
}
