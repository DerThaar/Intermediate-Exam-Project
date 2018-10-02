using UnityEngine;

public class AvoidingTrigger : MonoBehaviour
{
	[HideInInspector] public bool avoid;
	[HideInInspector] public Transform obstacle;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Obstacle")
		{
			avoid = true;
			obstacle = other.gameObject.transform;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Obstacle")
		{
			avoid = false;
			GetComponentInParent<WanderNew>().SetWayPoint();
		}
	}
}
