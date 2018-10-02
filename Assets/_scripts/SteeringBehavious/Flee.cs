using UnityEngine;

public class Flee : SteeringBehaviour
{
	Transform target;
	StateMachine stateMachine;


	public override void Awake()
	{
		base.Awake();
		target = GameObject.Find("Player").transform;
		stateMachine = GetComponent<StateMachine>();
	}

	public override void Steer()
	{
		if (stateMachine.isFleeing)
		{			
			Vector3 desired = transform.position - target.position;
			desired = desired.normalized * maxSpeed;
			Vector3 steeringForce = desired - vehicle.Velocity;
			steeringForce = Vector3.ClampMagnitude(steeringForce, maxForce);
			vehicle.ApplyForce(steeringForce);
		}
	}
}
