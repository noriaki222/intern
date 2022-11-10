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

    public void PauseAnim()
    {
        anim.speed = 0.0f;
    }

    public void PlayAnim()
    {
        anim.speed = 1.0f;
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

    public void PlayAddAnim()
    {
        isLossFin = false;
    }
}