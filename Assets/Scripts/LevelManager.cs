using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
	public GameObject levelButtonPrefab;

	public int levelCount = 9;

	private ButtonManager btManager; 

	private void Start()
	{
		int row = 0;
		int column = 0;
		btManager = GameObject.Find("EventSystem").GetComponent<ButtonManager>();
		for (int id = 1; id <= levelCount; id++)
		{
			if (Resources.Load<TextAsset>("Levels/" + id + "/level") == null)
			{
				return;
			}
			GameObject bt = Instantiate(levelButtonPrefab, transform);
			bt.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-120f + 120f * column, 120f - 120f * row, 0f);
			bt.GetComponentInChildren<Text>().text = id.ToString();
			bt.name = id.ToString();
			bt.GetComponent<Button>().onClick.AddListener(delegate
			{
				btManager.LoadLevel(Int32.Parse(bt.name));
			});
			column += 1;
			row += column / 3;
			column %= 3;
		}
	}
}
