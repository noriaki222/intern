using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables; //Timeline‚Ì§Œä‚É•K—v

public class TimelinePlayer : MonoBehaviour
{
    private PlayableDirector playableDirector;
    // Start is called before the first frame update
    void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
    }

    //Ä¶‚·‚é
    public void PlayTimeline()
    {
        playableDirector.Play();
    }

    //ˆê’â~‚·‚é
    public void PauseTimeline()
    {
        playableDirector.Pause();
    }

    //ˆê’â~‚ğÄŠJ‚·‚é
    public void ResumeTimeline()
    {
        playableDirector.Resume();
    }

    //’â~‚·‚é
    public void StopTimeline()
    {
        playableDirector.Stop();
    }
}
