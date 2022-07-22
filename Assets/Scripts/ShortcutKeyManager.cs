using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShortcutKeyManager : MonoBehaviour
{
	public GameObject hideFace;

	private void Update()
	{
		bool hide = Input.GetKeyDown(KeyCode.H);
		
		if (hide)
		{
			hideFace.SetActive(!hideFace.activeInHierarchy);
		}
	}
}
