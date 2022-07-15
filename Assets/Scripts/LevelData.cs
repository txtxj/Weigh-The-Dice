using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public int id;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
