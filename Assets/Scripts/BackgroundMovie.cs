using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class BackgroundMovie : MonoBehaviour
{
    private void Start()
    {
        GetComponent<VideoPlayer>().url = System.IO.Path.Combine(Application.streamingAssetsPath, "TitleBackground.mp4");
        GetComponent<VideoPlayer>().Play();
    }
}
