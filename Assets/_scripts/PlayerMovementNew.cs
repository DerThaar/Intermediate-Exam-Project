using UnityEngine;

public class PlayerMovementNew : MonoBehaviour
{
	[HideInInspector] public Transform Target;
	[HideInInspector] public bool Orbit;
	[HideInInspector] public bool Walking;

	[SerializeField] Transform PivotTransform;
	[SerializeField] float WalkSpeed;
	[SerializeField] float RunSpeed;
	[SerializeField] float SneakSpeed;

	CharacterController controller;
	Vector3 movement;
	Quaternion newRotation;

	float fallSpeed;
	float gravity = 9.8f;
	float speed;
	float horizontal;
	float vertical;	


	void Awake()
	{
		controller = GetComponent<CharacterController>();
	}

	void Update()
	{
		horizontal = Input.GetAxis("Horizontal");
		vertical = Input.GetAxis("Vertical");

		Walking = horizontal != 0f || vertical != 0f;
		Orbit = horizontal != 0f;   // is used in the CameraMovement Script	

		CheckIfRunningOrSneaking();		
		CalcMovement(horizontal, vertical);
		HandleRotation();
		movement.y = VerticalSpeed();
		controller.Move(movement * Time.deltaTime);
	}	

	void CheckIfRunningOrSneaking()
	{
		if (Input.GetAxis("Run") > 0.1f)
		{
			speed = RunSpeed;
			GetComponent<HungerSystem>().HungerGet = 3;

		}
		else if (Input.GetAxis("Sneak") > 0.1f)
		{
			speed = SneakSpeed;
			GetComponent<HungerSystem>().HungerGet = 1;
		}
		else if(Input.GetAxis("Run") > 0.1f && Input.GetAxis("Sneak") > 0.1f)
		{
			speed = WalkSpeed;
			GetComponent<HungerSystem>().HungerGet = 1;
		}
		else
		{
			speed = WalkSpeed;
			GetComponent<HungerSystem>().HungerGet = 1;
		}
	}

	void CalcMovement(float h, float v)
	{
		movement = new Vector3();

		if (h != 0 || v != 0)
		{
			movement = v * PivotForward() + h * PivotRight();
		}

		movement = movement.normalized * speed;
	}

	float VerticalSpeed()
	{
		if (!controller.isGrounded)
		{
			fallSpeed -= gravity * Time.deltaTime;
		}
		else
		{
			fallSpeed = 0f;
		}

		return fallSpeed;
	}

	void HandleRotation()
	{
		if (Walking)
		{
			newRotation = Quaternion.LookRotation(movement, Vector3.up);
		}

		transform.rotation = newRotation;
	}

	Vector3 PivotForward()
	{
		Vector3 forwardVector = PivotTransform.transform.forward;
		forwardVector.y = 0;
		return forwardVector;
	}

	Vector3 PivotRight()
	{
		Vector3 rightVector = PivotTransform.transform.right;
		rightVector.y = 0;
		return rightVector;
	}
}
