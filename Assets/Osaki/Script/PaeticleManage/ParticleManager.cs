using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] private PauseManager pause;
    [SerializeField] private ParticleSystem[] objects;
    private bool oldPause;
    // Start is called before the first frame update
    void Start()
    {
        oldPause = pause.GetPause();
    }

    // Update is called once per frame
    void Update()
    {
        if(pause != null && pause.GetPause())
        {
            for(int i = 0; i < objects.Length; ++i)
            {
                objects[i].Pause();
            }
            oldPause = pause.GetPause();
            return;
        }
        else
        {
            if(oldPause )
            {
                for (int i = 0; i < objects.Length; ++i)
                {
                    objects[i].Play();
                }
            }
        }

        oldPause = pause.GetPause();
    }
}
