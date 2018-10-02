using UnityEngine;

public class CameraMovementNew : MonoBehaviour
{
	[SerializeField] float OffsetPlayer;
	[SerializeField] float XSpeed;
	[SerializeField] float YSpeed;
	[SerializeField] float XClampAngle;
	[SerializeField] float XClampMaxDistance;
	[SerializeField] float WallOffsetDistance;
	[SerializeField] float SmoothTime;

	[SerializeField] Transform CameraTarget;

	PlayerMovementNew playerMovement;

	private Vector3 offsetComplete;
	private Vector3 heightOffset;
	[SerializeField] LayerMask Mask;

	private float x;
	private float y;

	float pitch;

	Vector3 desiredCamPos;
	Vector3 velocity;
	Quaternion newRotation;
	Quaternion wantedRotation;


	void Awake()
	{
		playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovementNew>();
	}

	void Start()
	{
		offsetComplete = Vector3.forward * OffsetPlayer;
		heightOffset = new Vector3(0, 1);
	}

	void Update()
	{
		x = Input.GetAxis("Mouse X") * XSpeed;
		y = Input.GetAxis("Mouse Y") * YSpeed;

		if (CameraTarget)
		{
			SmoothLookAt(x, y);
		}
	}

	void SmoothLookAt(float _x, float _y)
	{
		pitch = Mathf.Clamp(pitch + _y, -XClampAngle, XClampAngle * XClampMaxDistance);

		if (playerMovement != null)
		{
			if (playerMovement.Orbit)
			{
				newRotation = Quaternion.LookRotation(CameraTarget.position - transform.position);
				wantedRotation = Quaternion.Euler(pitch, newRotation.eulerAngles.y + _x, 0);
			}
			else
			{
				wantedRotation = Quaternion.Euler(pitch, transform.eulerAngles.y + _x, 0);
			}
		}

		transform.rotation = wantedRotation;
		desiredCamPos = CameraTarget.transform.position - (transform.rotation * offsetComplete) + heightOffset;

		CompensateForWalls(CameraTarget.transform.position + heightOffset, ref desiredCamPos);

		transform.position = desiredCamPos;
	}

	private void CompensateForWalls(Vector3 fromObject, ref Vector3 toCam)
	{
		RaycastHit wallHit = new RaycastHit();

		if(Physics.Linecast(fromObject, toCam, out wallHit, Mask))
		{
			Vector3 targetUnsmooth = new Vector3(wallHit.point.x, wallHit.point.y, wallHit.point.z);
			Vector3 targetSmooth = targetUnsmooth + transform.forward * WallOffsetDistance;
			toCam = Vector3.SmoothDamp(targetUnsmooth, targetSmooth, ref velocity, SmoothTime * Time.deltaTime);
		}
	}
}