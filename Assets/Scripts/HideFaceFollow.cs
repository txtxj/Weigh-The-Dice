using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideFaceFollow : MonoBehaviour
{
    public GameObject dice;
    public Vector3 dir;

    private Material mat;
    private Dice d;
    private static readonly int MainTexSt = Shader.PropertyToID("_MainTex_ST");
    private static readonly float _1_6 = 1f / 6f;

    private void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
        d = dice.GetComponent<Dice>();
    }
    
    private void Update()
    {
        transform.position = dice.transform.position + dir;
        mat.SetVector(MainTexSt, new Vector4(_1_6, 1f, (d.FaceNumber - 1f) / 6f, 0f));
    }
}
