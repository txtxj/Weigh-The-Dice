using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DiceMove : MonoBehaviour
{
    public Vector2Int pos;
    public int headNumber;
    public float moveTime;

    private Vector3 rotate;
    private Vector3 lastRotate;
    private static int animFlag = 0;

    public void SignalFlag()
    {
        animFlag += 1;
    }

    private void MoveAnimation(Vector3 position)
    {
        animFlag -= 1;
        Hashtable hash = new Hashtable();
        hash.Add("position", position);
        hash.Add("time", moveTime);
        hash.Add("easeType", iTween.EaseType.linear);
        hash.Add("onComplete", "SignalFlag");
        iTween.MoveTo(gameObject, hash);
    }

    private void Walk(int dir)
    {
        if (MapInfo.Movable(pos.x, pos.y, dir))
        {
            Vector3 rot = Vector3.zero;
            switch (dir)
            {
                case 0:
                    pos += Vector2Int.left;
                    rot = new Vector3(0f, 0f, 90f);
                    break;
                case 1:
                    pos += Vector2Int.right;
                    rot = new Vector3(0f, 0f, -90f);
                    break;
                case 2:
                    pos += Vector2Int.down;
                    rot = new Vector3(-90f, 0f, 0f);
                    break;
                case 3:
                    pos += Vector2Int.up;
                    rot = new Vector3(90f, 0f, 0f);
                    break;
            }
            MoveAnimation(MapInfo.GetPosition(pos.x, pos.y));
            lastRotate = transform.eulerAngles;
            rotate = MapInfo.GetRotation(pos.x, pos.y) + rot;
        }
    }

    private void Start()
    {
        transform.position = MapInfo.GetPosition(pos.x, pos.y);
        rotate = Vector3.zero;
    }

    private void FixedUpdate()
    {
        if (animFlag < 0f)
        {
            transform.Rotate(rotate * Time.deltaTime / moveTime, Space.World);
        }
    }

    private void Update()
    {
        if (animFlag < 0) return;
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
    }
}
