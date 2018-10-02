using UnityEngine;

public abstract class SteeringBehaviour : MonoBehaviour
{
	[SerializeField] protected float maxSpeed = 4f;	
	[SerializeField] protected float maxForce = 1f;	

	protected EnemyAnimal vehicle;


	public abstract void Steer();

	public virtual void Awake()
	{
		vehicle = GetComponent<EnemyAnimal>();
	}

}