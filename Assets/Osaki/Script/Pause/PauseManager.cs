using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private bool isPause = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
           isPause = isPause ? false : true;
        }
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
