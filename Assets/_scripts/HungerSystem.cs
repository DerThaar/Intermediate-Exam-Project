using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HungerSystem : MonoBehaviour
{	
	[SerializeField] Image HungerBar;
	[SerializeField] TextMeshProUGUI HungerBarText;
	[SerializeField] Image HealthBar;
	[SerializeField] TextMeshProUGUI HealthBarText;
	[SerializeField] GameObject StarvingText;
	[SerializeField] PauseGameManager pauseGameManager;
	[SerializeField] float MaxHunger = 100f;

	public float MaxHealth = 10f;
	public float Health = 10f;
	public float Hunger = 0;
	public int HungerGet = 1;

	bool isWalking;
	bool isHungry;
	float time;
	float timer = 2f;
	

	void Update()
	{
		isWalking = GetComponent<PlayerMovementNew>().Walking;
		HungerBarText.text = "Hunger " + Hunger + "/" + MaxHunger;
		HealthBarText.text = "Health " + Health + "/" + MaxHealth;

		if (Hunger == MaxHunger)
		{
			HungerBar.GetComponent<Image>().color = Color.red;
			StarvingText.SetActive(true);		
			isHungry = true;
		}
		else if (Hunger > MaxHunger * 0.75f)
		{
			HungerBar.GetComponent<Image>().color = Color.red;
			StarvingText.SetActive(false);			
			isHungry = false;
		}
		else if (Hunger > MaxHunger * 0.25f)
		{
			HungerBar.GetComponent<Image>().color = Color.yellow;
			StarvingText.SetActive(false);			
			isHungry = false;
		}
		else
		{
			HungerBar.GetComponent<Image>().color = Color.green;
			StarvingText.SetActive(false);			
			isHungry = false;
		}

		UpdateHunger();		
	}

	void UpdateHunger()
	{
		if (isWalking)
		{
			time += Time.deltaTime;

			if (time >= timer)
			{
				if (isHungry)
				{
					Health -= 1;
				}			

				Hunger += HungerGet;
				time = 0;
			}
		}

		if (Hunger <= 0)
		{
			Hunger = 0;
		}
		if (Hunger >= MaxHunger)
		{
			Hunger = MaxHunger;
		}
		if (Health <= 0)
		{
			Health = 0;
			pauseGameManager.GameOver();
		}
		if (Health >= MaxHealth)
		{
			Health = MaxHealth;
		}

		HungerBar.fillAmount = Hunger / MaxHunger;
		HealthBar.fillAmount = Health / MaxHealth;
	}
}
