using UnityEngine;

public class Arrive : SteeringBehaviour
{
	Vector3 velocity;
	Transform target;
	StateMachine stateMachine;	

	float MaxSlowDistance = 1f;
	float PlayerOffset = 1f;
	float distanceToTarget;


	public override void Awake()
	{
		base.Awake();
		velocity = Vector3.zero;
		target = GameObject.Find("Player").transform;
		stateMachine = GetComponent<StateMachine>();		
	}

	public override void Steer()
	{
		if (stateMachine.isSeeking)
		{
			Vector3 desired = target.position - transform.position;
			distanceToTarget = desired.magnitude - PlayerOffset;
			if (distanceToTarget < MaxSlowDistance)
			{
				float mappedSpeed = Mathe.Map(distanceToTarget, 0, MaxSlowDistance, 0, maxSpeed);
				desired = desired.normalized * mappedSpeed;
			}
			else
			{
				desired = desired.normalized * maxSpeed;
			}
			Vector3 steering = desired - velocity;
			steering = Vector3.ClampMagnitude(steering, maxForce);
			vehicle.ApplyForce(steering);
		}
	}
}
