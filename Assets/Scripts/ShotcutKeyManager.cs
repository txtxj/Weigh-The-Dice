using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShotcutKeyManager : MonoBehaviour
{
    private void Update()
    {
        bool retry = Input.GetKeyDown(KeyCode.R);
        bool swap = Input.GetKeyDown(KeyCode.V);

        if (retry)
        {
            SceneManager.LoadScene(1);
        }
        else if (swap)
        {
            
        }
    }
}
