using UnityEngine;

public class ObstacleAvoidance : SteeringBehaviour
{	
	AvoidingTrigger trigger;

	public override void Awake()
	{		
		base.Awake();
		trigger = transform.GetChild(1).GetComponent<AvoidingTrigger>();
	}
	
	public override void Steer()
	{		
		if (trigger.avoid)
		{			
			Vector3 desired = transform.position - trigger.obstacle.position;
			desired = desired.normalized * maxSpeed;
			Vector3 steeringForce = desired - vehicle.Velocity;
			steeringForce = Vector3.ClampMagnitude(steeringForce, maxForce);
			vehicle.ApplyForce(steeringForce);			
		}
	}	
}
