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
        levelBGMIndex = GameObject.Find("LevelObject").GetComponent<LevelData>().id % levelBGM.Length;
        GetComponent<AudioSource>().clip = levelBGM[levelBGMIndex];
        GetComponent<AudioSource>().Play();
        swimmingSource = GameObject.Find("dice").GetComponent<AudioSource>();
        swimmingSource.loop = false;
        swimmingStatic = swimming;
    }

    public static void PlaySwim()
    {
        if (swimmingSource.isPlaying) return;
        swimmingSource.clip = swimmingStatic[Random.Range(0, swimmingStatic.Length)];
        swimmingSource.Play();
    }
}
