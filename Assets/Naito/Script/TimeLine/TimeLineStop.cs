using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables; //Timeline‚Ì§Œä‚É•K—v

public class TimeLineStop : MonoBehaviour
{
    [SerializeField] private PlayableDirector playableDirector;
    int Renda = 0;

    // Start is called before the first frame update
    void Start()
    {
        playableDirector.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 4") || Input.GetKeyDown("joystick button 5"))
        {
            Renda++;
        }
        if(Renda >= 10)
        {
            playableDirector.Resume();
        }
    }
}
