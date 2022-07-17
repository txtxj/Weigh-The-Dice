using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoClip[] teachVideo;
    public int[] playLevel;

    public GameObject videoObject;

    private void PlayVideo(VideoClip vc)
    {
        videoObject.SetActive(true);
        videoObject.GetComponent<VideoPlayer>().clip = vc;
        videoObject.GetComponent<VideoPlayer>().Play();
    }

    private void Start()
    {
        int id = GameObject.Find("LevelObject").GetComponent<LevelData>().id;
        for (int i = 0; i < playLevel.Length; i++)
        {
            if (playLevel[i] == id && !LevelData.played[i])
            {
                LevelData.played[i] = true;
                PlayVideo(teachVideo[i]);
                return;
            }
        }
    }
}
