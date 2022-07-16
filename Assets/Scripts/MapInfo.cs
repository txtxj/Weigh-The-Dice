using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MapInfo : MonoBehaviour
{
    public static int[,] mapInfo;
    public static int[,] rotation;
    public static int[,] hole;
    public static Vector2Int mapSize;
    public static Vector2Int target;
    public static GameObject[,] tiles;
    public static Vector3[,] slopePositionBias = new Vector3[4, 2]
    {
        {new Vector3(-0.3f, 0f, 0f), new Vector3(-0.5f, -0.1f, 0f)},
        {new Vector3(0.3f, 0f, 0f), new Vector3(0.5f, -0.1f, 0f)},
        {new Vector3(0f, 0f, 0.3f), new Vector3(0f, -0.1f, 0.5f)},
        {new Vector3(0f, 0f, -0.3f), new Vector3(0f, -0.1f, -0.5f)}
    };
    public static Vector3[,] slopeRotationBias = new Vector3[4, 2]
    {
        {new Vector3(0f, 0f, 15f), new Vector3(0f, 0f, 30f)},
        {new Vector3(0f, 0f, -15f), new Vector3(0f, 0f, -30f)},
        {new Vector3(15f, 0f, 0f), new Vector3(30f, 0f, 0f)},
        {new Vector3(-15f, 0f, 0f), new Vector3(-30f, 0f, 0f)},
    };

    public static int GetHeight(int i, int j)
    {
        return Mathf.Max(3 - mapInfo[i, j] / 3, 0);
    }

    public static bool IsSlope(int i, int j)
    {
        return mapInfo[i, j] >= 9 && mapInfo[i, j] < 15;
    }

    public static bool IsWater(int i, int j)
    {
        return (mapInfo[i, j] + 1) % 3 == 0;
    }
    
    public static bool IsCrack(int i, int j)
    {
        return hole[i, j] == 1;
    }
    
    public static bool IsHole(int i, int j)
    {
        return hole[i, j] == 2;
    }

    public static bool InSize(int i, int j)
    {
        return i >= 0 && j >= 0 && i < mapSize.y && j < mapSize.x;
    }
    
    public static bool IsTarget(int i, int j)
    {
        return target.x == i && target.y == j;
    }

    public static int GetSlopeCode(int i, int j)
    {
        if (mapInfo[i, j] <= 10) return 3;
        if (mapInfo[i, j] <= 12) return 2;
        if (mapInfo[i, j] <= 14) return 6;
        return 0;
    }

    public static bool Movable(int i, int j, int dir)
    {
        Vector2Int d = Vector2Int.zero;
        switch (dir)
        {
            case 0:
                d = Vector2Int.left;
                break;
            case 1:
                d = Vector2Int.right;
                break;
            case 2:
                d = Vector2Int.down;
                break;
            case 3:
                d = Vector2Int.up;
                break;
        }
        if (!InSize(i + d.x, j + d.y))
        {
            return false;
        }
        if (IsHole(i + d.x, j + d.y))
        {
            return false;
        }
        if (GetHeight(i, j) == GetHeight(i + d.x, j + d.y))
        {
            return true;
        }
        if (IsSlope(i, j))
        {
            if (!InSize(i - d.x, j - d.y))
            {
                return false;
            }
            int from = GetHeight(i - d.x, j - d.y);
            int to = GetHeight(i + d.x, j + d.y);
            if (from * to == GetSlopeCode(i, j))
            {
                return true;
            }
        }
        else if (IsSlope(i + d.x, j + d.y))
        {
            int from = GetHeight(i , j);
            int to = GetHeight(i + d.x * 2, j + d.y * 2);
            if (from * to == GetSlopeCode(i + d.x, j + d.y))
            {
                return true;
            }
        }
        return false;
    }

    public static Vector3 GetPosition(int i, int j)
    {
        float h = 0f;
        Vector3 pos = Vector3.zero;
        if (IsSlope(i, j))
        {
            int code = GetSlopeCode(i, j);
            if (code == 2)
            {
                h = 1.5f;
                pos = slopePositionBias[rotation[i, j], 0];
            }
            else if (code == 3)
            {
                h = 2f;
                pos = slopePositionBias[rotation[i, j], 1];
            }
            else if (code == 6)
            {
                h = 2.5f;
                pos = slopePositionBias[rotation[i, j], 0];
            }
        }
        else
        {
            h = GetHeight(i, j);
        }
        return new Vector3(i * 2f, 1.5f + h * 0.5f, j * 2f) + pos;
    }

    public static Vector3 GetRotation(int i, int j)
    {
        Vector3 q = Vector3.zero;
        if (IsSlope(i, j))
        {
            int code = GetSlopeCode(i, j);
            if (code == 2)
            {
                q = slopeRotationBias[rotation[i, j], 0];
            }
            else if (code == 3)
            {
                q = slopeRotationBias[rotation[i, j], 1];
            }
            else if (code == 6)
            {
                q = slopeRotationBias[rotation[i, j], 0];
            }
        }
        return q;
    }

    public static void MakeCrack(int x, int y)
    {
        hole[x, y] = 1;
        tiles[x, y].GetComponent<Renderer>().materials[1].SetColor("_Color", (Color)new Color32(0xda, 0xae, 0x7e, 0xff));
    }

    public static void DestroyTile(int x, int y)
    {
        hole[x, y] = 2;
        Hashtable hash = new Hashtable();
        hash.Add("position", tiles[x, y].transform.position + Vector3.down * 80f);
        hash.Add("time", 2f);
        hash.Add("easeType", "easeInQuad");
        Destroy(tiles[x, y], 2f);
        iTween.MoveTo(tiles[x, y], hash);
    }
}
