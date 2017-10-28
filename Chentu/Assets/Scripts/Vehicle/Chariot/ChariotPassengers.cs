using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChariotPassengers : MonoBehaviour {

	public Vehicle chariotMovement;
	
	// Use this for initialization
	public Transform dropOffSpotTransform;



	public List<Transform> spots;
	public Transform driver;
	public Transform archer;
	public Transform passenger1;
	public Transform passenger2;

	void Start () {
		FindSpot();
	}
	
	// Update is called once per frame
	void Update () {
		if(!chariotMovement.noHorse){
			CheckEmpty();
			CheckSpot();
			SetToNeutral();
		} else {
			chariotMovement.moveSpeed = 0;
		}
	}


	void FindSpot(){

		driver = null;
		archer = null;
		passenger1 = null;
		passenger2 = null;

		for (int i = 0; i < transform.childCount; i++){
			if(transform.GetChild(i).name == "Driver"){
				driver = transform.GetChild(i);
			} else if (transform.GetChild(i).name == "Archer"){
				archer = transform.GetChild(i);
			} else if (transform.GetChild(i).name == "Passenger1"){
				passenger1 = transform.GetChild(i);
			} else if (transform.GetChild(i).name == "Passenger2"){
				passenger2 = transform.GetChild(i);
			}
		}

		spots = new List<Transform>();
		spots.Add(driver);
		spots.Add(archer);
		spots.Add(passenger1);
		spots.Add(passenger2);
	}


	public bool empty;
	public bool hasSpot;

	//public AIInteract paasengerOntheWay;

	void CheckEmpty(){


		for (int i = spots.Count -1 ; i > -1 ; i--) {
			if(spots[i]!=null){

				if(spots[i].childCount == 0 ){
					empty = true;

				} else {
					empty = false;
					SetFaction(spots[i].GetChild(0));
				}
			}
		}

		if(empty){
			chariotMovement.aiControlling = false;
			chariotMovement.playerControlling = false;
		}

	}

	void CheckSpot(){
		hasSpot = false;
		for (int i = 0; i < spots.Count; i++){
			if(spots[i]!=null){

				if(spots[i].childCount == 0 ){
					hasSpot = true;
				} 
			}
		}
	}


	void SetHorseFaction(string faction){

		for (int i = 0; i < transform.childCount ; i++){
			if(transform.GetChild(i).name == "Bar"){
				for (int n = 0; n < transform.GetChild(i).childCount; n++){
					if(transform.GetChild(i).GetChild(n).CompareTag("Horse")){
						transform.GetChild(i).GetChild(n).gameObject.layer = LayerMask.NameToLayer(faction);
					}
				}
			}
		}
	}

	void SetWheelFaction(string faction){

		for (int i = 0; i < transform.childCount ; i++){
			if(transform.GetChild(i).CompareTag("Wheel")){
						transform.GetChild(i).gameObject.layer = LayerMask.NameToLayer(faction);
			}
		}



	}


	void SetToNeutral(){
		if(empty & !chariotMovement.noHorse ){
			
			chariotMovement.aiControlling = false;
			
			chariotMovement.playerControlling = false;
			gameObject.tag = ("Chariot");
			gameObject.layer = LayerMask.NameToLayer( "Chariot" );
			for (int i = 0; i < spots.Count; i++){
				if(spots[i]){
					SetHorseFaction("Chariot");
					SetWheelFaction("Chariot");
					spots[i].gameObject.layer = LayerMask.NameToLayer( "Chariot" );
				}
			}
		}
	}

	void SetFaction(Transform passenger){
		if(passenger.GetChild(0).CompareTag("Player") | passenger.CompareTag("Ally")){
			gameObject.layer = LayerMask.NameToLayer( "FriendlyChariot" );
			for (int i = 0; i < spots.Count; i++){
				if(spots[i]){

					SetHorseFaction("FriendlyChariot");
					SetWheelFaction("FriendlyChariot");
					spots[i].gameObject.layer = LayerMask.NameToLayer( "FriendlyChariot" );
					spots[i].transform.parent.tag = "FriendlyChariot";
				}
			}
		} else if(passenger.CompareTag("Enemy") | passenger.CompareTag("Ally")){
			gameObject.layer = LayerMask.NameToLayer( "EnemyChariot" );
			for (int i = 0; i < spots.Count; i++){
				if(spots[i]){

					SetHorseFaction("EnemyChariot");
					SetWheelFaction("EnemyChariot");
					spots[i].gameObject.layer = LayerMask.NameToLayer( "EnemyChariot" );
					spots[i].transform.parent.tag = "EnemyChariot";
				}
			}
		} 
	}



}
