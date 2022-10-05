using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayLife : MonoBehaviour
{
    private Animator anim;
    private bool isLossFin = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayLossAnim()
    {
        isLossFin = false;
        anim.Play("LifeLossAnim");
    }

    public void FinLossAnim()
    {
        isLossFin = true;
    }

    public bool GetLossFin()
    {
        return isLossFin;
    }
}
