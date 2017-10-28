using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentControl : MonoBehaviour {

	public GameObject boarPrefab;
	GameObject boarClone;
	public float boarSpawnChance;

	// Use this for initialization
	void Start () {
		boarSpawnChance = 100;
		SpawnAnimal();
	}

	Vector3 spawnPosition;
	void SpawnAnimal(){
		spawnPosition = new Vector3(Random.Range(-8,8),Random.Range(-8,8),0);
		if(Random.Range(0,100)<boarSpawnChance){
			print("Spawn boar");
			GameObject boarClone = Instantiate(boarPrefab, spawnPosition, transform.rotation);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
