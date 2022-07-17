using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip[] levelBGM;
    public AudioClip[] swimming;

    private int levelBGMIndex = 0;
    private AudioSource swimmingSource;

    private void Start()
    {
        swimmingSource = GameObject.Find("dice").GetComponent<AudioSource>();
        swimmingSource.loop = false;
        GameObject obj = GameObject.Find("LevelObject");
        levelBGMIndex = obj.GetComponent<LevelData>().id % levelBGM.Length;
        if (obj.GetComponent<AudioSource>().clip == levelBGM[levelBGMIndex] && obj.GetComponent<AudioSource>().isPlaying) return;
        obj.GetComponent<AudioSource>().clip = levelBGM[levelBGMIndex];
        obj.GetComponent<AudioSource>().Play();
    }

    public void PlaySwim()
    {
        if (swimmingSource.isPlaying) return;
        swimmingSource.clip = swimming[Random.Range(0, swimming.Length)];
        swimmingSource.Play();
    }
}
