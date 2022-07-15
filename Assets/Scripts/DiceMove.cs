using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DiceMove : MonoBehaviour
{
    public Vector2Int pos;
    public int headNumber;

    private bool moveFlag;

    private void Walk(int dir)
    {
        if (moveFlag) return;
        if (MapInfo.Movable(pos.x, pos.y, dir))
        {
            moveFlag = true;
            switch (dir)
            {
                case 0:
                    pos += Vector2Int.left;
                    break;
                case 1:
                    pos += Vector2Int.right;
                    break;
                case 2:
                    pos += Vector2Int.down;
                    break;
                case 3:
                    pos += Vector2Int.up;
                    break;
            }
            transform.position = MapInfo.GetPosition(pos.x, pos.y);
            transform.rotation = MapInfo.GetRotation(pos.x, pos.y);
        }
    }

    private void Start()
    {
        moveFlag = false;
        transform.position = MapInfo.GetPosition(pos.x, pos.y);
    }

    private void Update()
    {
        float hr = Input.GetAxisRaw("Horizontal");
        float vr = Input.GetAxisRaw("Vertical");

        if (hr < 0f)
        {
            Walk(0);
        }
        else if (hr > 0f)
        {
            Walk(1);
        }
        else if (vr < 0f)
        {
            Walk(2);
        }
        else if (vr > 0f)
        {
            Walk(3);
        }
        else
        {
            moveFlag = false;
        }
    }
}
