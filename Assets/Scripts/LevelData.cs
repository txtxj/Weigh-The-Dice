using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public int id;

    private static bool exist = false;

    public static bool[] played = new bool[3]{false, false, false};

    private void Start()
    {
        if (!exist)
        {
            exist = true;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
