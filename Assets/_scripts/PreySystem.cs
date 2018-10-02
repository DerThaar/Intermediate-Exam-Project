using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PreySystem : MonoBehaviour
{
	[SerializeField] HungerSystem hungerSystem;
	[SerializeField] GameObject caughtPrey;
	[SerializeField] GameObject prey;
	[SerializeField] PupHunger pupHunger;
	[SerializeField] TextMeshProUGUI actionText;

	List<GameObject> currentPrey = new List<GameObject>();

	public bool nearbyPrey;
	public float time;
	int timer = 2;


	void Update()
	{
		CatchPrey();
		EatPrey();
		DropPrey();

		if (actionText.text != "")
		{
			time += Time.deltaTime;
			if (time > timer)
			{
				actionText.text = "";
				time = 0;
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Prey")
		{
			currentPrey.Add(other.gameObject);
			nearbyPrey = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Prey")
		{
			currentPrey.Remove(other.gameObject);

			if (currentPrey.Count == 0)
			{
				nearbyPrey = false;
			}
		}
	}

	void CatchPrey()
	{
		if (!nearbyPrey && Input.GetButtonDown("PickUp"))
		{
			time = 0;
			actionText.text = "There is nothing to pick up!";
		}
		else if (nearbyPrey && Input.GetButtonDown("PickUp"))
		{
			if (caughtPrey.activeSelf)
			{
				time = 0;
				actionText.text = "You can only carry one piece of food!";
			}
			else
			{
				int pickedUpPrey = currentPrey.Count - 1;
				GameObject gameObjectToDestroy = currentPrey[pickedUpPrey];
				currentPrey.RemoveAt(pickedUpPrey);
				Destroy(gameObjectToDestroy);
				caughtPrey.SetActive(true);
				nearbyPrey = false;
				pupHunger.gotPrey = false;
			}
		}
	}

	void EatPrey()
	{
		if (!caughtPrey.activeSelf && Input.GetButtonDown("Eat"))
		{
			time = 0;
			actionText.text = "There is nothing to eat!";
		}
		else if (caughtPrey.activeSelf && Input.GetButtonDown("Eat"))
		{
			if (hungerSystem.Health >= hungerSystem.MaxHealth && hungerSystem.Hunger <= 0)
			{
				time = 0;
				actionText.text = "You are not hungry!";
			}
			else
			{
				if (hungerSystem.Hunger - 20 <= 0)
				{
					hungerSystem.Hunger = 0;
				}
				else
				{
					hungerSystem.Hunger -= 20;
				}
				if (hungerSystem.Health + 1 >= hungerSystem.MaxHealth)
				{
					hungerSystem.Health = hungerSystem.MaxHealth;
				}
				else
				{
					hungerSystem.Health += 1;
				}

				caughtPrey.SetActive(false);				
			}
		}
	}

	void DropPrey()
	{
		if (!caughtPrey.activeSelf && Input.GetButtonDown("Drop"))
		{
			time = 0;
			actionText.text = "There is nothing to drop!";
		}
		else if (caughtPrey.activeSelf && Input.GetButtonDown("Drop"))
		{
			caughtPrey.SetActive(false);
			Instantiate(prey, caughtPrey.transform.position, Quaternion.identity);
		}
	}
}
