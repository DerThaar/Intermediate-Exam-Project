using UnityEngine;

public class Attack : MonoBehaviour
{
	GameObject player;
	StateMachine stateMachine;
	[SerializeField] int AttackStength;
	float time;
	int timer = 1;


	public void Awake()
	{
		player = GameObject.Find("Player");
	    stateMachine = GetComponent<StateMachine>();
	}

	void Update()
	{
		if (stateMachine.isAttacking)
		{
			time += Time.deltaTime;
			if (time > timer)
			{
				if (player.GetComponent<HungerSystem>().Health - AttackStength < 0)
				{
					AttackStength = (int)player.GetComponent<HungerSystem>().Health;
				}
				player.GetComponent<HungerSystem>().Health -= AttackStength;
				time = 0;
			}
		}
		else if (!stateMachine.isAttacking && time > 0)
		{
			time = 0;
		}
	}
}
