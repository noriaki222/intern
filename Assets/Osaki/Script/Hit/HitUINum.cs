using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitUINum : MonoBehaviour
{
    [SerializeField] private RawImage _10;
    [SerializeField] private RawImage _1;

    private bool animFlg = false;
    private int num = 0;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        int ten, one;
        ten = num / 10;
        one = num % 10;
        if (ten == 0)
            _10.gameObject.SetActive(false);
        else
            _10.gameObject.SetActive(true);
        if (one == 0 && ten == 0)
            _1.gameObject.SetActive(false);
        else
            _1.gameObject.SetActive(true);

        var work = _10.uvRect;
        work.x = ten / 10.0f;
        _10.uvRect = work;
        work = _10.uvRect;
        work.x = one / 10.0f;
        _1.uvRect = work;

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
