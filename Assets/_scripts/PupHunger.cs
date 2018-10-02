using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PupHunger : MonoBehaviour
{
	[SerializeField] GameObject Player;
	[SerializeField] Image HungerBar;
	[SerializeField] GameObject Pup1;
	[SerializeField] GameObject Pup2;
	[SerializeField] GameObject Pup3;
	[SerializeField] GameObject Pup1Object;
	[SerializeField] GameObject Pup2Object;
	[SerializeField] GameObject Pup3Object;
	[SerializeField] TextMeshProUGUI HungerBarText;
	[SerializeField] GameObject StarvingText;
	[SerializeField] PauseGameManager pauseGameManager;
	[SerializeField] float MaxHunger = 100;
	[SerializeField] float Hunger = 50;

	public bool gotPrey;

	GameObject prey;
	List<GameObject> currentPrey = new List<GameObject>();
	bool isWalking;
	bool won;
	int pupDeath;
	float time;
	float eatTime;
	float dieTime;
	float timer = 4f;
	int eatTimer = 2;
	int dieTimer = 10;


	void Update()
	{
		ChangeHungerbar();
		UpdateHunger();

		isWalking = Player.GetComponent<PlayerMovementNew>().Walking;
		HungerBarText.text = "Pup Hunger " + Hunger + "/" + MaxHunger;

		if (gotPrey)
		{
			eatTime += Time.deltaTime;

			if (eatTime >= eatTimer)
			{
				Hunger -= 20;
				eatTime = 0;
				int droppedPrey = currentPrey.Count - 1;
				GameObject gameObjectToDestroy = currentPrey[droppedPrey];
				currentPrey.RemoveAt(droppedPrey);
				Player.transform.GetChild(1).GetComponent<PreySystem>().nearbyPrey = false;
				Destroy(gameObjectToDestroy);

				if (currentPrey.Count == 0)
				{
					gotPrey = false;
				}
			}
		}
		else if (!gotPrey)
		{
			eatTime = 0;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Prey")
		{
			currentPrey.Add(other.gameObject);
			gotPrey = true;
		}
	}

	void ChangeHungerbar()
	{
		if (Hunger == MaxHunger)
		{
			HungerBar.GetComponent<Image>().color = Color.red;
			StarvingText.SetActive(true);

			if (isWalking)
			{
				dieTime += Time.deltaTime;
				if (dieTime > dieTimer)
				{
					if (pupDeath == 0)
					{
						Pup1.SetActive(false);
						Pup1Object.SetActive(false);
						pupDeath += 1;
						dieTime = 0;
					}
					else if (pupDeath == 1)
					{
						Pup2.SetActive(false);
						Pup2Object.SetActive(false);
						pupDeath += 1;
						dieTime = 0;
					}
					else if (pupDeath == 2)
					{
						Pup3.SetActive(false);
						Pup3Object.SetActive(false);
						pauseGameManager.GameOver();
					}
				}
			}
		}
		else if (Hunger > MaxHunger * 0.75f)
		{
			HungerBar.GetComponent<Image>().color = Color.red;
			StarvingText.SetActive(false);
		}
		else if (Hunger > MaxHunger * 0.25f)
		{
			HungerBar.GetComponent<Image>().color = Color.yellow;
			StarvingText.SetActive(false);
		}
		else
		{
			HungerBar.GetComponent<Image>().color = Color.green;
			StarvingText.SetActive(false);
		}
	}

	void UpdateHunger()
	{
		if (isWalking)
		{
			time += Time.deltaTime;

			if (time >= timer)
			{
				Hunger += 1;
				time = 0;
			}
		}

		if (Hunger <= 0)
		{
			Hunger = 0;

			if (!won)
			{
				won = true;
				pauseGameManager.Win();
			}
		}

		if (Hunger >= MaxHunger)
		{
			Hunger = MaxHunger;
		}

		HungerBar.fillAmount = Hunger / MaxHunger;
	}
}
