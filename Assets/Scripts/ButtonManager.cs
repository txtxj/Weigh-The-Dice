using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
	public GameObject levelMenu;
	public GameObject videoMenu;
	public GameObject helpMenu;
    
	public void GameQuit()
	{
		Application.Quit();
	}

	public void ShowLevelMenu()
	{
		levelMenu.SetActive(true);
	}
	
	public void ShowHelpMenu()
	{
		helpMenu.SetActive(true);
	}

	public void HideLevelMenu()
	{
		levelMenu.SetActive(false);
	}
	public void HideVideoMenu()
	{
		videoMenu.SetActive(false);
	}

	public void HideHelpMenu()
	{
		helpMenu.SetActive(false);
	}

	public void LoadLevel(int id)
	{
		GameObject.Find("LevelObject").GetComponent<LevelData>().id = id;
		SceneManager.LoadScene(1);
	}

	public void ExitGame()
	{
		SceneManager.LoadScene(0);
	}
}
