using UnityEngine;

public class DestroyPrey : MonoBehaviour
{
	int timer = 30;
	float time;
	PreySystem preySystem;


	private void Awake()
	{
		timer += Random.Range(-5, 5);		
		preySystem = GameObject.Find("HuntingTrigger").GetComponent<PreySystem>();
	}

	void Update()
	{
		time += Time.deltaTime;	
		if (time >= timer)
		{
			preySystem.nearbyPrey = false;
			Destroy(gameObject);
		}
	}
}
