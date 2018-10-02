using UnityEngine;

public class CameraOffset : MonoBehaviour
{
	Vector3 endPos;	
	Vector3 velocity;
	Vector3 MoveDir;
	[SerializeField] float Smooth;
	[SerializeField] Transform Target;
	
	
	void Update()
	{
		endPos = Target.position;		
		Vector3 smoothedPos = Vector3.SmoothDamp(transform.position, endPos, ref velocity, Smooth * Time.deltaTime);
		MoveDir = smoothedPos - transform.position;
		transform.position += MoveDir;
	}
}
