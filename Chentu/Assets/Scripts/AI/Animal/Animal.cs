using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour {

	public Animator animator;
	public Vector3 destination;

	public float hp;
	public float speed;

	public bool dead;
	public GameObject player;
	public GameObject[] allAllies;
	public GameObject[] allEnemies;

	public bool isHuntTarget;
	public Quest quest;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CountHunt(){
		if(quest)
		quest.questAmount --;
	}
}
