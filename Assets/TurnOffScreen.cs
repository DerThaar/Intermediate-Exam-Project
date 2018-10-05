using UnityEngine;

public class TurnOffScreen : MonoBehaviour
{
	private void Update()
	{
		if (Input.GetButtonDown("PickUp"))
			TurnOff();
	}

	public void TurnOff()
	{
		Destroy(gameObject);
	}
}
