using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	public const string level01Name = "Game";
	public const string menuName = "Menu";
		

	private void OnEnable()
	{
		MenuEvents.Instance.ChangeScene.AddListener(ChangeScene);
		MenuEvents.Instance.ExitGame.AddListener(ExitGame);		
	}

	private void OnDisable()
	{
		MenuEvents.Instance?.ChangeScene.RemoveListener(ChangeScene);
		MenuEvents.Instance?.ChangeScene.RemoveListener(ExitGame);	
	}

	public void ChangeScene()
	{
		if (SceneManager.GetActiveScene().name == "Menu")
		{
			SceneManager.LoadScene(level01Name);
		}
		else if(SceneManager.GetActiveScene().name == "Game")
		{
			SceneManager.LoadScene(menuName);
		}
	}	

	public void ExitGame()
	{
		Application.Quit();		
	}
}