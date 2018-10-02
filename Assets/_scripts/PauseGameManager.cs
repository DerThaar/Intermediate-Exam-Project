using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class PauseGameManager : MonoBehaviour
{
	[SerializeField] GameObject Player;
	[SerializeField] GameObject Cam;
	[SerializeField] GameObject PauseScreen;
	[SerializeField] GameObject GameOverScreen;
	[SerializeField] GameObject WinScreen;
	[SerializeField] TextMeshProUGUI actionText;

	AudioManager audioManager;
	bool isFreezed;


	void Awake()
	{
		audioManager = GameObject.Find("Audio").GetComponent<AudioManager>();
	}

	void Update()
	{
		if (Input.GetButtonDown("Pause"))
		{
			PauseGame();
		}
	}

	public void GameOver()
	{
		audioManager.musicState = 5;
		audioManager.TriggerMusicChange();
		
		if (!GameOverScreen.activeSelf)
		{			
			GameOverScreen.SetActive(true);
			EventSystem.current.SetSelectedGameObject(null);
			GameOverScreen.transform.GetChild(0).GetComponent<Button>().Select();
			FreezeGame();
		}
	}

	public void Win()
	{
		audioManager.musicState = 4;
		audioManager.TriggerMusicChange();

		if (!WinScreen.activeSelf)
		{			
			WinScreen.SetActive(true);
			EventSystem.current.SetSelectedGameObject(null);
			WinScreen.transform.GetChild(0).GetComponent<Button>().Select();
			FreezeGame();
		}
	}

	public void PauseGame()
	{
		if (!PauseScreen.activeSelf)
		{
			PauseScreen.SetActive(true);
			EventSystem.current.SetSelectedGameObject(null);
			PauseScreen.transform.GetChild(0).GetComponent<Button>().Select();
			FreezeGame();
		}
		else if (PauseScreen.activeSelf)
		{
			PauseScreen.SetActive(false);
			FreezeGame();
		}
	}

	public void BackToMainMenu()
	{		
		audioManager.musicState = 1;
		audioManager.TriggerMusicChange();
		FreezeGame();
		SceneManager.LoadScene("Menu");
	}

	public void PlayAgain()
	{
		audioManager.musicState = 1;
		audioManager.TriggerMusicChange();
		FreezeGame();
		SceneManager.LoadScene("Game");
	}

	void FreezeGame()
	{
		if (!isFreezed)
		{
			actionText.text = "";
			Player.GetComponent<PlayerMovementNew>().enabled = false;
			Player.GetComponent<HungerSystem>().enabled = false;
			Player.GetComponentInChildren<HuntingSystem>().enabled = false;
			Player.GetComponentInChildren<PreySystem>().enabled = false;
			Cam.GetComponent<CameraMovementNew>().enabled = false;
			GetComponent<ButtonManager>().enabled = false;
			GetComponent<AI>().enabled = false;

			Time.timeScale = 0;
			isFreezed = true;
		}
		else if (isFreezed)
		{
			Player.GetComponent<PlayerMovementNew>().enabled = true;
			Player.GetComponent<HungerSystem>().enabled = true;
			Player.GetComponentInChildren<HuntingSystem>().enabled = true;
			Player.GetComponentInChildren<PreySystem>().enabled = true;
			Cam.GetComponent<CameraMovementNew>().enabled = true;
			GetComponent<ButtonManager>().enabled = true;
			GetComponent<AI>().enabled = true;

			Time.timeScale = 1;
			isFreezed = false;
		}
	}
}
