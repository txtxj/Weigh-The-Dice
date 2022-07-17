using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Video;

public class TitleGuideVideoController : MonoBehaviour
{
    public VideoClip[] guideClips;
    public GameObject videoObject;

    public void PlayClip(int index)
    {
        videoObject.GetComponent<VideoPlayer>().clip = guideClips[index];
        videoObject.SetActive(true);
        videoObject.GetComponent<VideoPlayer>().Play();
    }
}
