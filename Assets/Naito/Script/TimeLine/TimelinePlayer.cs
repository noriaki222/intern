using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables; //Timeline�̐���ɕK�v

public class TimelinePlayer : MonoBehaviour
{
    private PlayableDirector playableDirector;
    // Start is called before the first frame update
    void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
    }

    //�Đ�����
    public void PlayTimeline()
    {
        playableDirector.Play();
    }

    //�ꎞ��~����
    public void PauseTimeline()
    {
        playableDirector.Pause();
    }

    //�ꎞ��~���ĊJ����
    public void ResumeTimeline()
    {
        playableDirector.Resume();
    }

    //��~����
    public void StopTimeline()
    {
        playableDirector.Stop();
    }
}
