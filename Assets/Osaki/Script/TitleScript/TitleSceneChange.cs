using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneChange : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private GetTitleAnimState fin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("B"))
        {
            anim.Play("title", 0 ,0);
        }

        if (fin.GetFin())
        {
           // SceneManager.LoadScene("BossSpider_Light_Naito");
        }
    }
}
