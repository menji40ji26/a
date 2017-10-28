using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleControl : MonoBehaviour {

	static BattleControl battleControl;
	public bool battleOver;
	public float time;

	void Awake(){
		battleOver = false;
	}

	// Use this for initialization
	void Start () {
		
	}
	

	// Update is called once per frame
	void LateUpdate () {
		time += Time.deltaTime;
		if(time > 5 ){
			FindSurvivors();
			CheckSurvivors();
		}
	}
	

    public GameObject[] allAllies;
    public GameObject[] allEnemies;
	public GameObject player;

	void FindSurvivors(){
		allAllies =  GameObject.FindGameObjectsWithTag("Ally");
		allEnemies =  GameObject.FindGameObjectsWithTag("Enemy");
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void CheckWounded(){

	}

	void CheckSurvivors(){
		if(!battleOver){
			if(allAllies.Length== 0 & !player){
				battleOver = true;
				print("Battle Over");
			} else if(allEnemies.Length == 0 ){
				battleOver = true;
				print("Battle Over");
				CheckLeader();
			}
		}
	}

	public List<GameObject> leftPlayerUnits;

	void CheckLeader(){
		for (int i = 0; i < allAllies.Length; i++){
			if(!allAllies[i].GetComponent<AIFight>().dead & allAllies[i].GetComponent<AIClass>().leader == "Player"){
				leftPlayerUnits.Add(allAllies[i].GetComponent<AIClass>().unitCard);
			}
		}
	}
}
