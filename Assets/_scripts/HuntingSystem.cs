using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HuntingSystem : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI actionText;	
	[HideInInspector] public bool animalInReach;	
	public List<GameObject> animal = new List<GameObject>();
	float attackStength = 1;	
	int timer = 2;
	

	void Update()
	{
		Attack();

		if (actionText.text != "")
		{
			GetComponent<PreySystem>().time += Time.deltaTime;
			if (GetComponent<PreySystem>().time > timer)
			{				
				actionText.text = "";
				GetComponent<PreySystem>().time = 0;
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Animal")
		{
			animal.Add(other.gameObject);
			animalInReach = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Animal")
		{
			animal.Remove(other.gameObject);

			if (animal.Count == 0)
			{
				animalInReach = false;			
			}

		}
	}

	void Attack()
	{
		if (animalInReach && Input.GetButtonDown("Attack"))
		{
			int preyToRemove = animal.Count - 1;
			animal[preyToRemove].GetComponent<EnemyHealth>().health -= attackStength;

		}
		else if (!animalInReach && Input.GetButtonDown("Attack"))
		{
			GetComponent<PreySystem>().time = 0;
			actionText.text = "There is nothing to attack!";
		}
	}

}
