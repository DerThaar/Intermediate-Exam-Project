using UnityEngine;
using System.Collections.Generic;

public class AI : MonoBehaviour
{
	[SerializeField] GameObject squirrel;
	[SerializeField] GameObject hare;
	[SerializeField] GameObject deer;
	[SerializeField] GameObject wolf;
	[SerializeField] GameObject bear;
	public AnimalPool SquirrelPool;
	public AnimalPool HarePool;
	public AnimalPool DeerPool;
	public AnimalPool WolfPool;
	public AnimalPool BearPool;
	public List<EnemyAnimal> EnemyAnimals = new List<EnemyAnimal>(32);


	void Awake()
	{
		SquirrelPool = new AnimalPool(squirrel);
		SquirrelPool.InitializePool();
		HarePool = new AnimalPool(hare);
		HarePool.InitializePool();
		DeerPool = new AnimalPool(deer);
		DeerPool.InitializePool();
		WolfPool = new AnimalPool(wolf);
		WolfPool.InitializePool();
		BearPool = new AnimalPool(bear);
		BearPool.InitializePool();
	}

	void Update()
	{
		for (int i = 0; i < EnemyAnimals.Count; i++)
		{
			EnemyAnimal eA = EnemyAnimals[i];

			for (int j = 0; j < eA.SteeringBehaviours.Length; j++)
			{
				eA.SteeringBehaviours[j].Steer();
			}
			
			eA.UpdateAnimal();
		}
	}

	public void Spawn(List<Vector3> positions, AnimalPool pool)
	{
		for (int i = 0; i < positions.Count; i++)
		{	
			EnemyAnimals.Add(pool.GetNext(positions[i]).GetComponent<EnemyAnimal>());
		}
	}
}


public class AnimalPool
{
	GameObject prefab; //this.prefab
	public Stack<GameObject> animals = new Stack<GameObject>(4);

	public AnimalPool(GameObject prefab)
	{
		this.prefab = prefab;
	}

	public void InitializePool()
	{
		for (int i = 0; i < 4; i++)
		{
			GameObject animal = GameObject.Instantiate(prefab);
			animals.Push(animal);
		}
	}

	public GameObject GetNext(Vector3 atPosition)
	{
		if (animals.Count == 0)
		{
			GameObject animal = GameObject.Instantiate(prefab);
			return animal;
		}
		else
		{
			GameObject animal = animals.Pop();
			animal.transform.position = atPosition;
			animal.SetActive(true);
			return animal;
		}
	}

	public void ReturnToPool(GameObject animal)
	{
		animal.SetActive(false);
		animal.transform.position = Vector3.zero;
		animals.Push(animal);
	}
}