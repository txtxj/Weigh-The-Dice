using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class LoadMapFromFile : MonoBehaviour
{
    public GameObject[] gameAsserts;
    [HideInInspector]
    public int assetsNumber;

    private void CreateBlock(int x, int y, int index, int forward = 0)
    {
        Instantiate(gameAsserts[index], new Vector3(x, 0f, y) * 2, Quaternion.identity, transform);
    }

    private void BuildMap(byte[] s)
    {
        Vector2Int size = new Vector2Int(s[0], s[1]);
        MapInfo.mapSize = size;
        MapInfo.mapInfo = new int[size.y, size.x];
        for (int i = 0; i < size.y; i++)
        {
            for (int j = 0; j < size.x; j++)
            {
                int u = s[i * size.x + j + 2];
                if (u < assetsNumber)
                {
                    MapInfo.mapInfo[i, j] = u;
                    CreateBlock(i, j, u);
                }
                else
                {
                    MapInfo.mapInfo[i, j] = 15;
                }
            }
        }
    }

    private void Awake()
    {
        assetsNumber = gameAsserts.Length;
        GameObject levelTransfer = GameObject.Find("LevelTransfer");
        int level = 0;
        if (levelTransfer != null)
        {
            level = levelTransfer.GetComponent<LevelData>().id;
        }
        TextAsset levelText = Resources.Load<TextAsset>("Levels/level" + level);
        BuildMap(levelText.bytes);
    }
}
