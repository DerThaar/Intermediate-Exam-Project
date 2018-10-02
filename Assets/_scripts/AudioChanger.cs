using System.Collections.Generic;
using UnityEngine;

public class AudioChanger : MonoBehaviour
{
	[HideInInspector] public AudioManager audioManager;
	[HideInInspector] public List<GameObject> nearAnimals = new List<GameObject>();


	void Awake()
	{
		audioManager = GameObject.Find("Audio").GetComponent<AudioManager>();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Aggressive" || other.gameObject.tag == "Passive")
		{
			nearAnimals.Add(other.gameObject);			

			if (other.gameObject.name == "Bear(Clone)")
			{
				audioManager.musicState = 3;
				audioManager.TriggerMusicChange();
			}
			else if (audioManager.musicState == 3)
			{
				return;
			}
			else if (audioManager.musicState == 2)
			{
				return;
			}
			else
			{
				audioManager.musicState = 2;
				audioManager.TriggerMusicChange();
			}
		}		
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Aggressive" || other.gameObject.tag == "Passive")
		{
			if (other.gameObject.name == "Bear(Clone)" && nearAnimals.Count > 1)
			{
				audioManager.musicState = 2;
				audioManager.TriggerMusicChange();
			}

			nearAnimals.Remove(other.gameObject);

			if (nearAnimals.Count == 0)
			{
				audioManager.musicState = 1;
				audioManager.TriggerMusicChange();
			}
		}		

		
	}
}
