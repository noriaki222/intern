using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables; //Timelineの制御に必要

public class TimelinePlayer : MonoBehaviour
{
    private PlayableDirector playableDirector;
    // Start is called before the first frame update
    void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
    }

    //再生する
    public void PlayTimeline()
    {
        playableDirector.Play();
    }

    //一時停止する
    public void PauseTimeline()
    {
        playableDirector.Pause();
    }

    //一時停止を再開する
    public void ResumeTimeline()
    {
        playableDirector.Resume();
    }

    //停止する
    public void StopTimeline()
    {
        playableDirector.Stop();
    }
}
