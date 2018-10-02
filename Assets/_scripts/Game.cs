using UnityEngine;

public class Game : MonoBehaviour
{
	private LevelManager levelManager;
	public LevelManager LevelManager
	{
		get { return levelManager; }
	}	

	private static Game instance;
	public static Game Instance
	{
		get
		{
			if (instance == null)
			{
				instance = GameObject.FindObjectOfType<Game>();
				instance.levelManager = instance.GetComponentInChildren<LevelManager>();				
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

		levelManager = GetComponentInChildren<LevelManager>();		
	}
}