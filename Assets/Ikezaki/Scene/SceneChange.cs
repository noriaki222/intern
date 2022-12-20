using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField] GameObject FadeCanvasPrefab;
    [SerializeField] float FadeIdleTime = 1.0f;

    GameObject FadeCanvasClone;
    //Fade fadeCanvas;
    Fade_Test1 fadeCanvas;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadSceneAsync("TitleScene_Ikezaki");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadSceneAsync("TutorialScene_Ikezaki");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneManager.LoadSceneAsync("GameScene_Ikezaki");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SceneManager.LoadSceneAsync("ClearScene_Ikezaki");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SceneManager.LoadSceneAsync("GameoverScene_Ikezaki");
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            StartCoroutine(WaitForLoadScene());
        }
    }
    
    IEnumerator WaitForLoadScene()
    {
        FadeCanvasClone = Instantiate(FadeCanvasPrefab);
        //fadeCanvas = FadeCanvasClone.GetComponent<Fade>();
        fadeCanvas = FadeCanvasClone.GetComponent<Fade_Test1>();
        fadeCanvas.bFadeIn = true;
        yield return new WaitForSeconds(FadeIdleTime);
        yield return SceneManager.LoadSceneAsync("IkezakiScene");
        fadeCanvas.bFadeOut = true;
    }
}
