using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInfo : MonoBehaviour
{
    [HideInInspector]
    public static int[,] mapInfo;
    [HideInInspector]
    public static Vector2Int mapSize;

    public static int GetHeight(int i, int j)
    {
        return Mathf.Max(3 - mapInfo[i, j] / 3, 0);
    }

    public static bool IsSlope(int i, int j)
    {
        return mapInfo[i, j] >= 9 && mapInfo[i, j] < 15;
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
        if (GetHeight(i, j) == GetHeight(i + d.x, j + d.y))
        {
            return true;
        }
        if (IsSlope(i, j))
        {
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
        if (IsSlope(i, j))
        {
            int code = GetSlopeCode(i, j);
            if (code == 2) h = 1.5f;
            else if (code == 3) h = 2f;
            else if (code == 6) h = 2.5f;
        }
        else
        {
            h = GetHeight(i, j);
        }
        return new Vector3(i * 2f, 1.5f + h * 0.5f, j * 2f);
    }
}
