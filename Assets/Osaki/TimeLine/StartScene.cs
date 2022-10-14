using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    private void OnDisable()
    {
        SceneManager.LoadScene("BossSpider_Light_Naito");
    }
}
