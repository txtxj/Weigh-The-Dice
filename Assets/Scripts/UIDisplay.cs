using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    public GameObject levelTex;

    private void Start()
    {
        levelTex.GetComponent<Text>().text = GameObject.Find("LevelObject").GetComponent<LevelData>().id.ToString();
    }
    
}
