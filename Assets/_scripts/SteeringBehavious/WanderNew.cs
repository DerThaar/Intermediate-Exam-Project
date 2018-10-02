using UnityEngine;

public class WanderNew : SteeringBehaviour
{
	StateMachine stateMachine;	
	Vector3 centerOfWandering;
	Vector3 currentWayPoint;
	float arrived = 0.16f;
	[SerializeField] LayerMask layerMask;
	[SerializeField] float maxRange;
	[SerializeField] float minRange;


	public override void Awake()
	{
		base.Awake();
		stateMachine = GetComponent<StateMachine>();		
		centerOfWandering = new Vector3(207.7f, 10.5f, 284.3f);
		SetWayPoint();
	}

	void Update()
	{
		if ((currentWayPoint - transform.position).sqrMagnitude < arrived)
		{
			SetWayPoint();
		}	
	}

	public override void Steer()
	{
		if (stateMachine.isWandering)
		{			
			Vector3 desired = currentWayPoint - transform.position;
			desired = desired.normalized * maxSpeed;
			Vector3 steeringForce = desired - vehicle.Velocity;
			steeringForce = Vector3.ClampMagnitude(steeringForce, maxForce);			
			vehicle.ApplyForce(steeringForce);			
		}
	}

	public void SetWayPoint()
	{
		currentWayPoint.x = centerOfWandering.x + Random.Range(minRange, maxRange);
		currentWayPoint.z = centerOfWandering.z + Random.Range(minRange, maxRange);

		float yPos = 0;
		RaycastHit hit;
		Ray ray = new Ray(new Vector3(currentWayPoint.x, 0, currentWayPoint.z) + Vector3.up * 50, Vector3.down);
		if (Physics.Raycast(ray, out hit, 75, layerMask))
		{
			yPos = hit.point.y;
		}
		currentWayPoint.y = yPos;
	}
}
