using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class LoadMapFromFile : MonoBehaviour
{
    public GameObject[] gameAsserts;
    [HideInInspector]
    public int assetsNumber;

    private void CreateBlock(int x, int y, int index, int forward)
    {
        Quaternion q = Quaternion.identity;
        switch (forward)
        {
            case 0:
                q = Quaternion.Euler(0f, 0f, 0f);
                break;
            case 1:
                q = Quaternion.Euler(0f, 180f, 0f);
                break;
            case 2:
                q = Quaternion.Euler(0f, 90f, 0f);
                break;
            case 3:
                q = Quaternion.Euler(0f, 270f, 0f);
                break;
        }
        Instantiate(gameAsserts[index], new Vector3(x, 0f, y) * 2, q, transform);
    }

    private void BuildMap(byte[] s, byte[] r)
    {
        Vector2Int size = new Vector2Int(s[0], s[1]);
        MapInfo.mapSize = size;
        MapInfo.mapInfo = new int[size.y, size.x];
        MapInfo.rotation = new int[size.y, size.x];
        for (int i = 0; i < size.y; i++)
        {
            for (int j = 0; j < size.x; j++)
            {
                int u = s[i * size.x + j + 2];
                int v = r[i * size.x + j];
                if (u < assetsNumber)
                {
                    MapInfo.mapInfo[i, j] = u;
                    MapInfo.rotation[i, j] = v;
                    CreateBlock(i, j, u, v);
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
        TextAsset rotateTex = Resources.Load<TextAsset>("Levels/rotate" + level);
        BuildMap(levelText.bytes, rotateTex.bytes);
    }
}