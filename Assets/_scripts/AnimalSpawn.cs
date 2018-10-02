using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawn : MonoBehaviour
{
	AI ai;
	[SerializeField] GameObject animalPositionPrefab;	
	[SerializeField] PoolType poolType;


	void Awake()
	{
		ai = GetComponent<AI>();
	}

	void Start()
	{
		switch (poolType)
		{
			case PoolType.Squirrel:
				SpawnAnimals(animalPositionPrefab, ai.SquirrelPool);
					break;
			case PoolType.Hare:
				SpawnAnimals(animalPositionPrefab, ai.HarePool);
				break;
			case PoolType.Deer:
				SpawnAnimals(animalPositionPrefab, ai.DeerPool);
				break;
			case PoolType.Wolf:
				SpawnAnimals(animalPositionPrefab, ai.WolfPool);
				break;
			case PoolType.Bear:
				SpawnAnimals(animalPositionPrefab, ai.BearPool);
				break;
		}	

		
	}

	public void SpawnAnimals(GameObject animal, AnimalPool pool)
	{
		int animalCount = animal.transform.childCount;
		List<Vector3> animalPositions = new List<Vector3>(animalCount);

		foreach (Transform animalPos in animal.transform)
		{
			animalPositions.Add(animalPos.position);
		}

		ai.Spawn(animalPositions, pool);
	}
}

enum PoolType
{
	Squirrel, Hare, Deer, Wolf, Bear
}
