using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private bool isPause = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public bool GetPause()
    {
        return isPause;
    }

    public void SetPause(bool flg)
    {
        isPause = flg;
    }
}
