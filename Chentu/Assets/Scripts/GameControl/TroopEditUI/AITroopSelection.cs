using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AITroopSelection : MonoBehaviour {


	public Dropdown troopModelList;
	public Dropdown ally1List;
	public Dropdown ally2List;
	public Dropdown enemy1List;
	public Dropdown enemy2List;
	public Dropdown enemy3List;

	public List<string> troopModels;
	TroopInfo troopInfo;


	// Use this for initialization
	void Start () {
		//Add Troop Model
		troopInfo = GameObject.FindWithTag("TroopInfo").GetComponent<TroopInfo>();
		troopModels = new List<string>(){
			 "Ramdom", "Bandits (Few)", "Deserters (Few)", 
			 "Mounted Bandits (Few)", "Deserters (Gang)", 
			 "Garrison (Gang)","Nomad (Few)", "Nomad (Gang)",
			 "Deserters (Army)",
			 
			 } ;
		ally1List.AddOptions(troopModels);
		ally2List.AddOptions(troopModels);
		enemy1List.AddOptions(troopModels);
		enemy2List.AddOptions(troopModels);
		enemy3List.AddOptions(troopModels);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeEnemy1(int model){
		troopInfo.enemy1TroopModel = model;
	}
	public void ChangeEnemy2(int model){
		troopInfo.enemy2TroopModel = model;
	}
	public void ChangeEnemy3(int model){
		troopInfo.enemy3TroopModel = model;
	}
	public void ChangeAlly1(int model){
		troopInfo.ally1TroopModel = model;
	}
	public void ChangeAlly2(int model){
		troopInfo.ally2TroopModel = model;
	}

}
