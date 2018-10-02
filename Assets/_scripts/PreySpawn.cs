using UnityEngine;

public class PreySpawn : MonoBehaviour
{
	float thrust = 300;


	void Awake()
	{		
		Vector3 rnd = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
		gameObject.GetComponent<Rigidbody>().AddForce(transform.up * thrust);
		gameObject.GetComponent<Rigidbody>().AddForce(rnd * thrust / 6);
	}
}