using UnityEngine;

public class CamLerpOffset : MonoBehaviour
{		
	Vector3 heightOffset;	
	[SerializeField] Transform Dummy;	


	void Awake()
	{
		heightOffset = new Vector3(0, 0.3f);
	}

	void Update()
	{
		transform.position = Dummy.position + heightOffset;
		transform.rotation = Dummy.rotation;
	}
}
