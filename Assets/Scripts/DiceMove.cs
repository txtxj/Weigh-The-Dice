using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UIElements;

public class DiceMove : MonoBehaviour
{
    public Vector2Int pos;
    public float moveTime;

    private Vector3 rotate;
    private Quaternion lastRotate;
    private static int animFlag = 0;
    private bool finishRotate = true;

    public int[] faceNumber = new int[] { 2, 5, 1, 6, 3, 4 };

    public void SignalFlag()
    {
        animFlag += 1;
        finishRotate = true;
    }

    private void MoveAnimation(Vector3 position)
    {
        animFlag -= 2;
        Hashtable hash = new Hashtable();
        hash.Add("position", position);
        hash.Add("time", moveTime);
        hash.Add("easeType", iTween.EaseType.linear);
        hash.Add("onComplete", "SignalFlag");
        iTween.MoveTo(gameObject, hash);
    }

    private void ChainSwap(int a, int b, int c, int d)
    {
        int k = faceNumber[a];
        faceNumber[a] = faceNumber[d];
        faceNumber[d] = faceNumber[c];
        faceNumber[c] = faceNumber[b];
        faceNumber[b] = k;
    }

    private void Walk(int dir)
    {
        if (MapInfo.Movable(pos.x, pos.y, dir))
        {
            Vector3 rot = -MapInfo.GetRotation(pos.x, pos.y);
            switch (dir)
            {
                case 0:
                    pos += Vector2Int.left;
                    rot += new Vector3(0f, 0f, 90f);
                    ChainSwap(0, 4, 1, 5);
                    break;
                case 1:
                    pos += Vector2Int.right;
                    rot += new Vector3(0f, 0f, -90f);
                    ChainSwap(0, 5, 1, 4);
                    break;
                case 2:
                    pos += Vector2Int.down;
                    rot += new Vector3(-90f, 0f, 0f);
                    ChainSwap(0, 3, 1, 2);
                    break;
                case 3:
                    pos += Vector2Int.up;
                    rot += new Vector3(90f, 0f, 0f);
                    ChainSwap(0, 2, 1, 3);
                    break;
            }
            MoveAnimation(MapInfo.GetPosition(pos.x, pos.y));
            rotate = MapInfo.GetRotation(pos.x, pos.y) + rot;
            finishRotate = false;
            Debug.Log(faceNumber[0]);
        }
    }

    private void Start()
    {
        transform.position = MapInfo.GetPosition(pos.x, pos.y);
        rotate = Vector3.zero;
        lastRotate = transform.rotation;
    }

    private void FixedUpdate()
    {
        if (finishRotate == false)
        {
            transform.Rotate(rotate * Time.deltaTime / moveTime, Space.World);
        }
        else if (animFlag < 0)
        {
            finishRotate = true;
            transform.rotation = lastRotate * Quaternion.Inverse(lastRotate) * Quaternion.Euler(rotate) * lastRotate;
            lastRotate = transform.rotation;
            animFlag += 1;
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
