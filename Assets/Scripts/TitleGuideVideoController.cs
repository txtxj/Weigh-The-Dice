using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Video;

public class TitleGuideVideoController : MonoBehaviour
{
    [HideInInspector]
    public string[] guideClips;
    public GameObject videoObject;

    private void Start()
    {
        guideClips = new string[3]
        {
            System.IO.Path.Combine(Application.streamingAssetsPath, "teach1.mp4"),
            System.IO.Path.Combine(Application.streamingAssetsPath, "teach2.mp4"),
            System.IO.Path.Combine(Application.streamingAssetsPath, "teach3.mp4")
        };
    }

    public void PlayClip(int index)
    {
        videoObject.GetComponent<VideoPlayer>().url = guideClips[index];
        videoObject.SetActive(true);
        videoObject.GetComponent<VideoPlayer>().Play();
    }
}
