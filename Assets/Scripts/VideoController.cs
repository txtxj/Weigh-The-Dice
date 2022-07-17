using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    [HideInInspector]
    public string[] teachVideo;
    public int[] playLevel;

    public GameObject videoObject;

    private void PlayVideo(string vc)
    {
        videoObject.SetActive(true);
        videoObject.GetComponent<VideoPlayer>().url = vc;
        videoObject.GetComponent<VideoPlayer>().Play();
    }

    private void Start()
    {
        teachVideo = new string[3]
        {
            System.IO.Path.Combine(Application.streamingAssetsPath, "teach1.mp4"),
            System.IO.Path.Combine(Application.streamingAssetsPath, "teach2.mp4"),
            System.IO.Path.Combine(Application.streamingAssetsPath, "teach3.mp4"),
        };
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
