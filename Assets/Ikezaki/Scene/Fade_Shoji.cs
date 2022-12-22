using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade_Shoji : MonoBehaviour
{
    [System.NonSerialized] public bool bFadeIn = false;
    [System.NonSerialized] public bool bFadeOut = false;

    public RectTransform R;
    public RectTransform L;

    [SerializeField] float panelSpeed = 1.0f;
    float LPos, RPos;

    // Start is called before the first frame update
    void Start()
    {
        LPos = L.position.x;
        RPos = R.position.x;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (bFadeIn)
        {
            FadeIn();
        }
        if(bFadeOut)
        {
            FadeOut();
        }
    }

    void FadeIn()
    {
        LPos += panelSpeed;
        RPos -= panelSpeed;
        SetPos();

        if(LPos >= 0.0f /*|| RPos <= 580.0f*/)
        {
            LPos = L.position.x;
            RPos = R.position.x;
            bFadeIn = false;
            //LPos = L.localPosition.x;
            //RPos = R.localPosition.x;
            //LPos = 0.0f;
            //RPos = 0.0f;
        }

    }
    void FadeOut()
    {
        LPos -= panelSpeed;
        RPos += panelSpeed;
        SetPos();

        if (RPos >= 1200.0f || LPos <= -600.0f)
        {
            bFadeOut = false;
            Destroy(this.gameObject);
        }
    }

    void SetPos()
    {
        L.position = new Vector3(LPos, 0.0f, 0.0f);
        R.position = new Vector3(RPos, 0.0f, 0.0f);
    }
}
