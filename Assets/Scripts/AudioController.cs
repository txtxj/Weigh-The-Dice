using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip[] levelBGM;
    public AudioClip[] swimming;

    private int levelBGMIndex = 0;
    private static AudioSource swimmingSource;
    private static AudioClip[] swimmingStatic;

    private void Start()
    {
        GameObject obj = GameObject.Find("LevelObject");
        levelBGMIndex = obj.GetComponent<LevelData>().id % levelBGM.Length;
        if (obj.GetComponent<AudioSource>().clip == levelBGM[levelBGMIndex] && obj.GetComponent<AudioSource>().isPlaying) return;
        obj.GetComponent<AudioSource>().clip = levelBGM[levelBGMIndex];
        obj.GetComponent<AudioSource>().Play();
        swimmingSource = GameObject.Find("dice").GetComponent<AudioSource>();
        swimmingSource.loop = false;
        swimmingStatic = swimming;
    }

    public static void PlaySwim()
    {
        if (swimmingSource == null)
        {
            swimmingSource = GameObject.Find("dice").GetComponent<AudioSource>();
        }
        if (swimmingSource.isPlaying) return;
        swimmingSource.clip = swimmingStatic[Random.Range(0, swimmingStatic.Length)];
        swimmingSource.Play();
    }
}
