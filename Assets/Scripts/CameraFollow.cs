using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public GameObject player;
	public GameObject wasd;
	
	public float speed = 0.1f;
	public int playerStress = 4;

	public Vector3 dir = new Vector3(-5f, 12f, -5f);
	public Vector3 rot = new Vector3(0f, 0f, 40f);
	
	private RectTransform wasdRect;
	
	public void ResetPosition(GameObject cplayer)
	{
		if (cplayer != null)
		{
			player = cplayer;
			transform.position = player.transform.position + dir;
		}
		else
		{
			transform.position = dir;
		}
	}

	private void Start()
	{
		wasdRect = wasd.GetComponent<RectTransform>();
		ResetPosition(null);
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(1))
		{
			dir.x *= -1;
			rot.z *= -1;
		}
		Vector3 target = (player.transform.position * playerStress) / (playerStress + 1) + dir;
		transform.position = Vector3.Lerp(transform.position, target, speed);
		wasdRect.rotation = Quaternion.Lerp(wasdRect.rotation, Quaternion.Euler(rot), speed);
		transform.LookAt(player.transform);
	}
}
