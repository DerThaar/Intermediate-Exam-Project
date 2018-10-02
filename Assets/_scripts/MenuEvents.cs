using UnityEngine;
using UnityEngine.Events;

public class MenuEvents : MonoBehaviour
{
	public UnityEvent ChangeScene = new UnityEvent();
	public UnityEvent ExitGame = new UnityEvent();

	private static MenuEvents instance;
	public static MenuEvents Instance
	{
		get
		{
			if (instance == null)
			{
				instance = GameObject.FindObjectOfType<MenuEvents>();
			}

			return instance;
		}
		private set { instance = value; }
	}


	void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
		}
	}
	
	public void ChangeSceneButton()
	{
		ChangeScene.Invoke();
	}	

	public void ExitGameButton()
	{
		ExitGame.Invoke();
	}
}

