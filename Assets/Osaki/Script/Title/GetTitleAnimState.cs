using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTitleAnimState : MonoBehaviour
{
    private bool fin = false;

    public bool GetFin()
    {
        return fin;
    }

    public void AnimFin()
    {
        fin = true;
    }
}
