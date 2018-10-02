using UnityEngine;

public class EnemyAnimal : MonoBehaviour
{
	public Vector3 Velocity { get; set; }
	public SteeringBehaviour[] SteeringBehaviours;
	StateMachine stateMachine;	
	float friction = 0.5f;
	[SerializeField] LayerMask layerMask;
	[SerializeField] float slerpFactor = 1f;
	

	void Awake()
	{
		SteeringBehaviours = GetComponents<SteeringBehaviour>();
		stateMachine = GetComponent<StateMachine>();	
	}

	public void ApplyForce(Vector3 sForce)
	{
		Velocity += sForce;
	}

	public void UpdateAnimal()
	{
		float yPos = 0;
		RaycastHit hit;
		Ray ray = new Ray(transform.position + Vector3.up * 2, Vector3.down);
		if (Physics.Raycast(ray, out hit, 3, layerMask))
		{
			yPos = hit.point.y;
		}

		transform.position += Velocity * Time.deltaTime;
		transform.position = new Vector3(transform.position.x, yPos, transform.position.z);		

		if (Velocity.sqrMagnitude > 0.1f)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Velocity), Time.deltaTime * slerpFactor);			
		}			

		if (!stateMachine.isWandering)
		{
			Velocity -= Velocity * friction;
		}
	}
}