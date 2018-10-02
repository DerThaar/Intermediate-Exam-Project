using UnityEngine;
using System.Collections.Generic;

public class StateMachine : MonoBehaviour
{
	Transform player;	

	public bool isWandering;
	public bool isFleeing;
	public bool isSeeking;
	public bool isAttacking;
	public bool isAvoiding;	

	public float attackDistance = 1f;

	enum Steering
	{
		Wander, Flee, Seek, Attack
	}

	Steering steeringBehaviour = Steering.Wander;

	void Awake()
	{
		player = GameObject.Find("Player").transform;		
		ChangeSteering(steeringBehaviour);
	}

	void Update()
	{
		CheckPlayerSpeed();
		CheckAttackDistance();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			if (gameObject.tag == "Passive")
			{
				SteeringFlee();
			}
			else if (gameObject.tag == "Aggressive")
			{
				SteeringSeek();
			}		
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{			
			SteeringWander();
		}
	}

	void CheckPlayerSpeed()
	{
		if (Input.GetAxis("Run") > 0.1f && !isSeeking)
		{
			GetComponent<SphereCollider>().radius = 5;
		}
		else if (Input.GetAxis("Sneak") > 0.1f && !isSeeking)
		{
			GetComponent<SphereCollider>().radius = 1.75f;
		}
		else if (Input.GetAxis("Run") > 0.1f && Input.GetAxis("Sneak") > 0.1f && !isSeeking)
		{
			GetComponent<SphereCollider>().radius = 3;
		}
		else if (GetComponent<SphereCollider>().radius != 3)
		{
			GetComponent<SphereCollider>().radius = 3;
		}
	}

	void CheckAttackDistance()
	{
		if ((player.position - transform.position).magnitude <= attackDistance && gameObject.tag == "Aggressive")
		{
			SteeringAttack();
		}
		else if ((player.position - transform.position).magnitude > attackDistance && isSeeking && gameObject.tag == "Aggressive")
		{
			SteeringSeek();
		}
	}

	void SteeringFlee()
	{
		ChangeSteering(Steering.Flee);
	}

	void SteeringSeek()
	{
		ChangeSteering(Steering.Seek);
	}

	void SteeringAttack()
	{
		ChangeSteering(Steering.Attack);
	}

	void SteeringWander()
	{
		GetComponent<WanderNew>().SetWayPoint();
		ChangeSteering(Steering.Wander);
	}

	void ChangeSteering(Steering steeringBehaviour)
	{
		switch (steeringBehaviour)
		{
			case Steering.Wander:
				isWandering = true;
				isFleeing = false;
				isAttacking = false;
				isSeeking = false;				
				break;
			case Steering.Flee:
				isWandering = false;
				isFleeing = true;
				isAttacking = false;
				isSeeking = false;				
				break;
			case Steering.Seek:
				isWandering = false;
				isFleeing = false;
				isAttacking = false;
				isSeeking = true;				
				break;
			case Steering.Attack:
				isWandering = false;
				isFleeing = false;
				isAttacking = true;
				isSeeking = true;
				break;
		}
	}
}
