using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public GameObject player;

	public float speed = 0.1f;
	public int playerStress = 4;

	private Vector3 dir = new Vector3(-5f, 12f, -5f);
	
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
		ResetPosition(null);
	}

	private void Update()
	{
		if (Camera.main != null && player != null)
		{
			if (Input.GetMouseButtonDown(1))
			{
				dir.x *= -1;
			}
			Vector3 target = (player.transform.position * playerStress) / (playerStress + 1) + dir;
			transform.position = Vector3.Lerp(transform.position, target, speed);
			transform.LookAt(player.transform);
		}
	}
}
