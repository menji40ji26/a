using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Force : MonoBehaviour {//, IPointerEnterHandler, IPointerExitHandler{

	//Info
	public bool isPlayer;
	public string name;
	public string faction;
	Color factionColor;
	public string location;
	public Settlement localSettlement;

	//Attributes
	public float speed;
	public float firstAid;                                                                                                                                                                                                                                                                     
	public int troopModel;


	//Movement
	public string finalGoal;
	public string goal;
	public float moveTime;

	//GameObject
	public GameObject info;
	public Text nameText;
	public Text classText;
	public GameObject moveIcon;
	public Text moveTimeText;
	public Text goalText;
	public Transform units;

	//Portrait
	public Transform portrait;

	// Use this for initialization
	void Start () {
		AddToCollection();
		CheckPlayer();
		CheckFaction();
		SetInfo();

		timeOnRoad = 0;
	}

	void AddToCollection(){
		Collection.collection.forces.Add(this);
	}

	void SetInfo(){
		nameText.text = name;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Locate();
		CheckMoving();
		Move();
	}

	void LateUpdate(){
		ShowDetail();
	}

	// public void OnPointerEnter(PointerEventData eventData) {
	// 	if(!isPlayer)
	// 	info.SetActive(true);
	// }
	
    // public void OnPointerExit(PointerEventData eventData){
	// 	if(!isPlayer)
	// 	info.SetActive(false);
	// 	print("Exit");
	// }

	void CheckPlayer(){
		if(isPlayer){
			Collection.collection.player = this.transform;
		}
	}

	void CheckFaction(){
		factionColor = Collection.collection.neutralColor;
		if(faction == "QU"){
			factionColor = Collection.collection.quColor;
		} else if(faction == "PU"){
			factionColor =  Collection.collection.quColor;
		} else if(faction == "XING"){
			factionColor =  Collection.collection.quColor;
		} else if(faction == "JU"){
			factionColor =  Collection.collection.quColor;
		} else if(faction == "SUI"){
			factionColor =  Collection.collection.quColor;
		} else if(faction == "GONG"){
			factionColor =  Collection.collection.quColor;
		} else if(faction == "RONG"){
			factionColor = Collection.collection.rongColor;
		}

		classText.color = factionColor;
		moveIcon.GetComponent<Image>().color = factionColor;
	}



	void Locate(){

		localSettlement = transform.parent.parent.GetComponent<Settlement>();

		for (int i = 0; i <  Collection.collection.locations.Length; i++){
			if(location == Collection.collection.locations[i].GetComponent<Settlement>().name ){
				transform.SetParent(Collection.collection.locations[i].GetComponent<Settlement>().forces);
			}
		}
		CheckArrive();
	}


	void CheckArrive(){
		if(localSettlement.name == finalGoal & finalGoal != "Null"){
			finalGoal = "Null";
			goal = "Null";
			CheckForces();
		}
	}

	public float timeOnRoad;

	void Move(){
		if(finalGoal != "Null"){
			SetMoveTime();
			CountDistance();
			if( moveTime > 0 ){
				if(goal == "Null"){
					finalGoal = "Null";
					return;
				}
				ShowGoal();
				if( !Collection.collection.timeControl.pause ){
					timeOnRoad += Time.deltaTime;
				}

				if(timeOnRoad > moveTime){
					location = goal;
					timeOnRoad = 0;
				}
			}
		} else {
		}
	}

	void SetMoveTime(){
		moveTime =   ( 15 * 0.1f * ( 10 - localSettlement.roadLevel ) ) / speed * 10;
	}


	public int distance;

	void CountDistance(){
		distance = 0;
		for (int i = 0; i < localSettlement.nearbyPlaces.Count; i++){
			if(localSettlement.nearbyPlaces[i].GetComponent<Settlement>().name == finalGoal){
				goal = finalGoal;
				distance = 1;
			} 
		}



		if( goal != finalGoal){


			for (int i = 0; i < localSettlement.nearbyPlaces.Count; i++){
				for (int n = 0; n < localSettlement.nearbyPlaces[i].GetComponent<Settlement>().nearbyPlaces.Count; n++){
					if(localSettlement.nearbyPlaces[i].GetComponent<Settlement>().nearbyPlaces[n].GetComponent<Settlement>().name == finalGoal){
						goal = localSettlement.nearbyPlaces[i].GetComponent<Settlement>().name;
						distance = 2;
					} else if(distance != 2){
						for (int a = 0; a < localSettlement.nearbyPlaces[i].GetComponent<Settlement>().nearbyPlaces[n].GetComponent<Settlement>().nearbyPlaces.Count; a++){
							if(localSettlement.nearbyPlaces[i].GetComponent<Settlement>().nearbyPlaces[n].GetComponent<Settlement>().nearbyPlaces[a].GetComponent<Settlement>().name == finalGoal){
								goal = localSettlement.nearbyPlaces[i].GetComponent<Settlement>().name;
								distance = 3;
							} else if(distance != 3){
								for (int b = 0; b < localSettlement.nearbyPlaces[i].GetComponent<Settlement>().nearbyPlaces[n].GetComponent<Settlement>().nearbyPlaces[a].GetComponent<Settlement>().nearbyPlaces.Count; b++){
									if(localSettlement.nearbyPlaces[i].GetComponent<Settlement>().nearbyPlaces[n].GetComponent<Settlement>().nearbyPlaces[a].GetComponent<Settlement>().nearbyPlaces[b].GetComponent<Settlement>().name == finalGoal){
										goal = localSettlement.nearbyPlaces[i].GetComponent<Settlement>().name;
										distance = 4;
									} else if(distance != 4){
										for (int c = 0; c < localSettlement.nearbyPlaces[i].GetComponent<Settlement>().nearbyPlaces[n].GetComponent<Settlement>().nearbyPlaces[a].GetComponent<Settlement>().nearbyPlaces[b].GetComponent<Settlement>().nearbyPlaces.Count; c++){
											if(localSettlement.nearbyPlaces[i].GetComponent<Settlement>().nearbyPlaces[n].GetComponent<Settlement>().nearbyPlaces[a].GetComponent<Settlement>().nearbyPlaces[b].GetComponent<Settlement>().nearbyPlaces[c].GetComponent<Settlement>().name == finalGoal){
												goal = localSettlement.nearbyPlaces[i].GetComponent<Settlement>().name;
												distance = 5;
											} else if(distance != 5){
												for (int d = 0; d < localSettlement.nearbyPlaces[i].GetComponent<Settlement>().nearbyPlaces[n].GetComponent<Settlement>().nearbyPlaces[a].GetComponent<Settlement>().nearbyPlaces[b].GetComponent<Settlement>().nearbyPlaces[c].GetComponent<Settlement>().nearbyPlaces.Count; d++){
													if(localSettlement.nearbyPlaces[i].GetComponent<Settlement>().nearbyPlaces[n].GetComponent<Settlement>().nearbyPlaces[a].GetComponent<Settlement>().nearbyPlaces[b].GetComponent<Settlement>().nearbyPlaces[c].GetComponent<Settlement>().nearbyPlaces[d].GetComponent<Settlement>().name == finalGoal){
														goal = localSettlement.nearbyPlaces[i].GetComponent<Settlement>().name;
														distance = 6;
													} else if(distance != 6){
														for (int e = 0; e < localSettlement.nearbyPlaces[i].GetComponent<Settlement>().nearbyPlaces[n].GetComponent<Settlement>().nearbyPlaces[a].GetComponent<Settlement>().nearbyPlaces[b].GetComponent<Settlement>().nearbyPlaces[c].GetComponent<Settlement>().nearbyPlaces[d].GetComponent<Settlement>().nearbyPlaces.Count; e++){
															if(localSettlement.nearbyPlaces[i].GetComponent<Settlement>().nearbyPlaces[n].GetComponent<Settlement>().nearbyPlaces[a].GetComponent<Settlement>().nearbyPlaces[b].GetComponent<Settlement>().nearbyPlaces[c].GetComponent<Settlement>().nearbyPlaces[d].GetComponent<Settlement>().nearbyPlaces[e].GetComponent<Settlement>().name == finalGoal){
																goal = localSettlement.nearbyPlaces[i].GetComponent<Settlement>().name;
																distance = 7;
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}

	public void MouseEnter(){
		pointing = true;
	}

	public void MouseExit(){
		pointing = false;
	}

	bool pointing;
	void ShowDetail(){
		if(pointing){
			if(!isPlayer){
				CheckIfAtWar();
				SetTarget();

			}
		}
	}
	
	void SetTarget(){
		if(Input.GetButton("Fire1")){
			 Collection.collection.mouseControl.targetForce = this;
		}
	}

	void CheckIfAtWar(){
		for (int i = 0; i < Collection.collection.factions.Length; i++){
			if(Collection.collection.player.GetComponent<Force>().faction == Collection.collection.factions[i].GetComponent<Faction>().name){
				for (int n = 0; n < Collection.collection.factions[i].GetComponent<Faction>().enemies.Count; n++){
					if(faction == Collection.collection.factions[i].GetComponent<Faction>().enemies[n].name){
						Collection.collection.mouseControl.SetAttack();

					}
				}
			}
		}
	}

	public List<Force> allyForces;

	public List<Force> enemyForces;

	void CheckForces(){

		//Check Enemy
		enemyForces = new List<Force>();
		for (int i = 0; i < localSettlement.forces.childCount; i++){
			for (int n = 0; n < Collection.collection.factions.Length; n++){
				if(faction == Collection.collection.factions[n].GetComponent<Faction>().name){
					for (int a = 0; a < Collection.collection.factions[n].GetComponent<Faction>().enemies.Count; a++){
						if(localSettlement.forces.GetChild(i).GetComponent<Force>().faction == Collection.collection.factions[n].GetComponent<Faction>().enemies[a].name){
							enemyForces.Add(localSettlement.forces.GetChild(i).GetComponent<Force>());
						}
					}
				}
			}
		}

		//Check Ally
		allyForces = new List<Force>();
		for (int i = 0; i < localSettlement.forces.childCount; i++){

			if(localSettlement.forces.GetChild(i).GetComponent<Force>().faction == faction & localSettlement.forces.GetChild(i).GetComponent<Force>() != this){
				allyForces.Add(localSettlement.forces.GetChild(i).GetComponent<Force>());
			}

			for (int n = 0; n < Collection.collection.factions.Length; n++){
				if(faction == Collection.collection.factions[n].GetComponent<Faction>().name){

					for (int a = 0; a < Collection.collection.factions[n].GetComponent<Faction>().allies.Count; a++){
						if(localSettlement.forces.GetChild(i).GetComponent<Force>().faction == Collection.collection.factions[n].GetComponent<Faction>().allies[a].name){
							allyForces.Add(localSettlement.forces.GetChild(i).GetComponent<Force>());
						}
					}
				}
			}
		}

		SetBattleTroop();
		Battle();
	}

	void SetBattleTroop(){
		if(isPlayer){
			if(enemyForces.Count==1){
				TroopInfo.troopInfo.enemy1TroopModel = enemyForces[0].troopModel;
				TroopInfo.troopInfo.enemy2TroopModel = -1;
				TroopInfo.troopInfo.enemy3TroopModel = -1;
			} else if(enemyForces.Count==2){
				TroopInfo.troopInfo.enemy1TroopModel = enemyForces[0].troopModel;
				TroopInfo.troopInfo.enemy2TroopModel = enemyForces[1].troopModel;
				TroopInfo.troopInfo.enemy3TroopModel = -1;
			} else if(enemyForces.Count>=3){
				TroopInfo.troopInfo.enemy1TroopModel = enemyForces[0].troopModel;
				TroopInfo.troopInfo.enemy2TroopModel = enemyForces[1].troopModel;
				TroopInfo.troopInfo.enemy3TroopModel = enemyForces[2].troopModel;
			}

			if(allyForces.Count==0){
				TroopInfo.troopInfo.ally1TroopModel = -1;
				TroopInfo.troopInfo.ally2TroopModel = -1;
			} else if(allyForces.Count==1){
				TroopInfo.troopInfo.ally1TroopModel = allyForces[0].troopModel;
				TroopInfo.troopInfo.ally2TroopModel = -1;
			} else if(allyForces.Count>=2){
				TroopInfo.troopInfo.ally1TroopModel = allyForces[0].troopModel;
				TroopInfo.troopInfo.ally2TroopModel = allyForces[1].troopModel;
			}

		}
	}

	void NamesToTroopInfo(){
		if(enemyForces.Count >= 1){
			TroopInfo.troopInfo.enemy1Name = enemyForces[0].name;
		}
		if(enemyForces.Count >= 2){
			TroopInfo.troopInfo.enemy2Name = enemyForces[1].name;
		}
		if(enemyForces.Count >= 3){
			TroopInfo.troopInfo.enemy3Name = enemyForces[2].name;
		}


		TroopInfo.troopInfo.playerName = name;
		if(allyForces.Count >= 1){
			TroopInfo.troopInfo.ally1Name = allyForces[0].name;
		}
		if(allyForces.Count >= 2){
			TroopInfo.troopInfo.ally2Name = allyForces[1].name;
		}
	}

	void UnitToTroopInfo(){
		int unitNumber = units.childCount;
		for (int i = 0; i < unitNumber; i++){
			units.GetChild(0).SetParent(TroopInfo.troopInfo.playerUnits);
		}
	}

	void PortraitsToTroopInfo(){
		for (int i = 0; i < enemyForces.Count; i++) {
			enemyForces[i].portrait.SetParent(TroopInfo.troopInfo.enemyPortrait);
		}

		portrait.SetParent(TroopInfo.troopInfo.allyPortrait);

		for (int i = 0; i < allyForces.Count; i++) {
			allyForces[i].portrait.SetParent(TroopInfo.troopInfo.allyPortrait);
		}
	}

	void Battle(){
		if(enemyForces.Count>0){
			if(isPlayer){
				NamesToTroopInfo();
				UnitToTroopInfo();
				PortraitsToTroopInfo();
				Collection.collection.sceneControl.LoadBattlePrepareScene();
			}
		}
	}


	void ShowGoal(){
		goalText.text = goal;
	}


	void CheckMoving(){
		if( finalGoal != "Null" ){
			moveIcon.SetActive(true);
			moveTimeText.text = (int) (moveTime - timeOnRoad) + "";
		} else {
			moveIcon.SetActive(false);
		}
	} 


}


