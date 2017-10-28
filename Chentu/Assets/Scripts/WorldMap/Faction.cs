using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faction : MonoBehaviour {

	public string name;
	public List<Force> forces;
	public List<Faction> enemies;
	public List<Faction> allies;

	// Use this for initialization
	void Start () {
		FindForces();
	}

	void FindForces(){
		forces = new List<Force>();
		for (int i = 0; i < Collection.collection.forces.Count; i++){
			if(Collection.collection.forces[i].faction == name){
				forces.Add(Collection.collection.forces[i]);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
