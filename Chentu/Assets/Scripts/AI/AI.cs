using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

	public string name;
	public string job;
	public float prestige;
	public Quest quest;
	public List<string> questLines;
	public bool questTook;

	public AIClass aiClass;
	public AIFight aiFight;
	public AIMovement aiMovement;
	public AIInteract aiInteract;

	//Portrait
	public Sprite body;
	public Sprite cloth;
	public Sprite face;
	public Sprite beard;
	public Sprite hair;


	// Use this for initialization
	void Start () {
		aiClass = GetComponent<AIClass>();
		aiFight = GetComponent<AIFight>();
		aiMovement = GetComponent<AIMovement>();
		aiInteract = GetComponent<AIInteract>();
		CheckJob();
		GameController.gameController.questControl.GenerateQuest(job,this);

	}

	void CheckJob(){
		if(job == "Merchant" | job == "Farmer"){
			gameObject.tag = "Civilian";
			gameObject.layer = LayerMask.NameToLayer("Civilian");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
