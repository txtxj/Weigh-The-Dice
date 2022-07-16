using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMapFromFile : MonoBehaviour
{
    public GameObject[] gameAsserts;
    [HideInInspector]
    public int assetsNumber;

    private TextAsset levelText;
    private TextAsset rotateTex;
    private Vector2Int size;

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
        MapInfo.tiles[x, y] = Instantiate(gameAsserts[index], new Vector3(x, 0f, y) * 2, q, transform);
    }

    private void BuildMap(byte[] s, byte[] r)
    {
        size = new Vector2Int(s[1], s[0]);
        MapInfo.target = new Vector2Int(r[0], r[1]);
        MapInfo.mapSize = size;
        MapInfo.mapInfo = new int[size.y, size.x];
        MapInfo.rotation = new int[size.y, size.x];
        MapInfo.hole = new int[size.y, size.x];
        MapInfo.tiles = new GameObject[size.y, size.x];
        for (int i = 0; i < size.y; i++)
        {
            for (int j = 0; j < size.x; j++)
            {
                int u = s[i * size.x + j + 2];
                int v = r[i * size.x + j + 2];
                if (u < assetsNumber)
                {
                    MapInfo.mapInfo[i, j] = u;
                    MapInfo.rotation[i, j] = v;
                    if (u == 0 || u == 3 || u == 6 || u == 9 || u == 11 || u == 13)
                    {
                        MapInfo.hole[i, j] = 1;
                    }
                    else
                    {
                        MapInfo.hole[i, j] = 0;
                    }
                    CreateBlock(i, j, u, v);
                }
                else
                {
                    MapInfo.mapInfo[i, j] = 15;
                }
            }
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(1);
    }

    private void Awake()
    {
        assetsNumber = gameAsserts.Length;
        GameObject levelTransfer = GameObject.Find("LevelObject");
        int level = 0;
        if (levelTransfer != null)
        {
            level = levelTransfer.GetComponent<LevelData>().id;
        }
        levelText = Resources.Load<TextAsset>("Levels/" + level + "/level");
        rotateTex = Resources.Load<TextAsset>("Levels/" + level + "/rotate");
        BuildMap(levelText.bytes, rotateTex.bytes);
    }
}
