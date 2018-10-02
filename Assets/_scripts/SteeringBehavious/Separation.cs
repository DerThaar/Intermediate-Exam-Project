using System.Collections.Generic;
using UnityEngine;

public class Separation : SteeringBehaviour
{	
	[SerializeField] float separationDistance = 2f;
	List<EnemyAnimal> nearAnimals = new List<EnemyAnimal>(16);
	AI ai;


	public override void Awake()
	{
		base.Awake();
		ai = FindObjectOfType<AI>();
	}

	public override void Steer()
	{
		nearAnimals.Clear();
		Vector3 averageDir = Vector3.zero;

		for (int i = 0; i < ai.EnemyAnimals.Count; i++)
		{
			Vector3 direction = transform.position - ai.EnemyAnimals[i].transform.position;
			if (direction.sqrMagnitude < (separationDistance * separationDistance) && direction.sqrMagnitude > 0.00001f)
			{
				nearAnimals.Add(ai.EnemyAnimals[i]);
				averageDir += direction.normalized;				
			}
		}

		if (nearAnimals.Count > 0)
		{
			averageDir = (averageDir / nearAnimals.Count).normalized;			
		}
		else
		{
			return;
		}

		Vector3 desired = averageDir * maxSpeed;
		Vector3 steeringForce = desired - vehicle.Velocity;
		steeringForce = Vector3.ClampMagnitude(steeringForce, maxForce);		
		vehicle.ApplyForce(steeringForce * 1.3f);		
	}
}

