using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [System.NonSerialized] public bool bFadeIn = false;
    [System.NonSerialized] public bool bFadeOut = false;

    [SerializeField] Image panelImage;
    [SerializeField] float fadeSpeed = 0.02f;

    float red, green, blue, alpha;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        red = panelImage.color.r;
        green = panelImage.color.g;
        blue = panelImage.color.b;
        alpha = panelImage.color.a;
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
        alpha += fadeSpeed;
        SetAlpha();
        if(alpha>=1)
        {
            bFadeIn = false;
        }
    }
    void FadeOut()
    {
        alpha -= fadeSpeed;
        SetAlpha();
        if(alpha<=0)
        {
            bFadeOut = false;
            Destroy(gameObject);
        }
    }

    void SetAlpha()
    {
        panelImage.color = new Color(red, green, blue, alpha);
    }
}
