using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	public float health;
	[SerializeField] float AmountOfPrey;
	[SerializeField] float maxHealth = 3;
	[SerializeField] GameObject prey;	

	AI ai;
	HuntingSystem huntingSystem;
	AudioChanger audioChanger;


	void Awake()
	{
		huntingSystem = GameObject.Find("HuntingTrigger").GetComponent<HuntingSystem>();
		audioChanger = huntingSystem.gameObject.GetComponent<AudioChanger>();
		ai = GameObject.Find("EventSystem").GetComponent<AI>();
		health = maxHealth;
	}	

	void Update()
	{
		if (health <= 0)
		{
			for (int i = 0; i < AmountOfPrey; i++)
			{
				Instantiate(prey, transform.position, Quaternion.identity);
			}

			huntingSystem.animal.RemoveAt(huntingSystem.animal.Count - 1);

			if (huntingSystem.animal.Count == 0)
			{
				huntingSystem.animalInReach = false;
			}

			audioChanger.nearAnimals.Remove(transform.parent.gameObject);

			if (audioChanger.nearAnimals.Count == 0)
			{
				audioChanger.audioManager.musicState = 1;
				audioChanger.audioManager.TriggerMusicChange();
			}

			ai.HarePool.ReturnToPool(transform.parent.gameObject);
		}
	}
}
