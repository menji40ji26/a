using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TroopEditUIControl : MonoBehaviour {

	//GameObject
	public GameObject skirmishCanvas;
	public GameObject battlePrepareCanvas;

	public GameObject aiTroopEdit;
	public GameObject enemy1;
	public GameObject enemy2;
	public GameObject enemy3;
	public GameObject ally1;
	public GameObject ally2;

	public Text unitALevel;
	public Text unitBLevel;
	public Text unitCLevel;
	public TroopInfo troopInfo;

	public Transform enmey1Portrait;
	public Transform enmey2Portrait;
	public Transform enmey3Portrait;
	public Text enmey1NameText;
	public Text enmey2NameText;
	public Text enmey3NameText;

	public Transform playerPortrait;
	public Transform ally1Portrait;
	public Transform ally2Portrait;	
	public Text playerNameText;
	public Text ally1NameText;
	public Text ally2NameText;

	// Use this for initialization
	void Start () {
		FindTroopInfo();
		CheckMode();
	}

	void FindTroopInfo(){
		troopInfo = GameObject.FindWithTag("TroopInfo").GetComponent<TroopInfo>();
	}

	void CheckMode(){
		if(troopInfo.skirmish){
			skirmishCanvas.SetActive(true);
			battlePrepareCanvas.SetActive(false);
			//aiTroopEdit.SetActive(true);
		} else {
			skirmishCanvas.SetActive(false);
			battlePrepareCanvas.SetActive(true);
			//aiTroopEdit.SetActive(false);
			TransferPortraits();
			SetNames();
		}
	}

		Transform portrait1;
		Transform portrait2;
		Transform portrait3;

		Transform playerPortrait1;
		Transform allyPortrait1;
		Transform allyPortrait2;

	void SetNames(){
		if(portrait1){
			enmey1NameText.text = TroopInfo.troopInfo.enemy1Name;
		}
		if(portrait2){
			enmey2NameText.text = TroopInfo.troopInfo.enemy2Name;
		}
		if(portrait3){
			enmey3NameText.text = TroopInfo.troopInfo.enemy3Name;
		}


		playerNameText.text = TroopInfo.troopInfo.playerName;
		if(allyPortrait1){
			ally1NameText.text = TroopInfo.troopInfo.ally1Name;
		}
		if(allyPortrait2){
			ally2NameText.text = TroopInfo.troopInfo.ally2Name;
		}
	}

	void TransferPortraits(){

		//Enemy
		if(TroopInfo.troopInfo.enemyPortrait.childCount > 0){
			portrait1 = TroopInfo.troopInfo.enemyPortrait.GetChild(0);
		}
		if(TroopInfo.troopInfo.enemyPortrait.childCount > 1){
			portrait2 = TroopInfo.troopInfo.enemyPortrait.GetChild(1);
		}
		if(TroopInfo.troopInfo.enemyPortrait.childCount > 2){
			portrait3 = TroopInfo.troopInfo.enemyPortrait.GetChild(2);
		}


		if(portrait1 ){
			portrait1.SetParent(enmey1Portrait);
			portrait1.localPosition = Vector3.zero;
			portrait1.localScale = new Vector3(1,1,1);
		} else {
			enmey1Portrait.parent.parent.gameObject.SetActive(false);
		}

		if(portrait2 ){
			portrait2.SetParent(enmey2Portrait);
			portrait2.localPosition = Vector3.zero;
			portrait2.localScale = new Vector3(1,1,1);
		} else {
			enmey2Portrait.parent.parent.gameObject.SetActive(false);
		}

		if(portrait3 ){
			portrait3.SetParent(enmey3Portrait);
			portrait3.localPosition = Vector3.zero;
			portrait3.localScale = new Vector3(1,1,1);
		} else {
			enmey3Portrait.parent.parent.gameObject.SetActive(false);
		}

		//Ally
		playerPortrait1 = TroopInfo.troopInfo.allyPortrait.GetChild(0);

		if(TroopInfo.troopInfo.allyPortrait.childCount > 1){
			allyPortrait1 = TroopInfo.troopInfo.allyPortrait.GetChild(1);
		}
		if(TroopInfo.troopInfo.allyPortrait.childCount > 2){
			allyPortrait2 = TroopInfo.troopInfo.allyPortrait.GetChild(2);
		}


		playerPortrait1.SetParent(playerPortrait);
		playerPortrait1.localPosition = Vector3.zero;
		playerPortrait1.localScale = new Vector3(1,1,1);


		if(allyPortrait1 ){
			allyPortrait1.SetParent(ally1Portrait);
			allyPortrait1.localPosition = Vector3.zero;
			allyPortrait1.localScale = new Vector3(1,1,1);
		} else {
			ally1Portrait.parent.parent.gameObject.SetActive(false);
		}

		if(allyPortrait2 ){
			allyPortrait2.SetParent(ally2Portrait);
			allyPortrait2.localPosition = Vector3.zero;
			allyPortrait2.localScale = new Vector3(1,1,1);
		} else {
			ally2Portrait.parent.parent.gameObject.SetActive(false);
		}
	}

	
	// Update is called once per frame
	void Update () {
		UpdateUnitInfo();
	}

	void UpdateUnitInfo(){
		if(troopInfo.aLevel==0){
			unitALevel.text = "Wounded";
		} else if(troopInfo.aLevel==1){
			unitALevel.text = "Rookie";
		} else if(troopInfo.aLevel==2){
			unitALevel.text = "Veteran";
		} else if(troopInfo.aLevel==3){
			unitALevel.text = "Elite";
		} 

		if(troopInfo.bLevel==0){
			unitBLevel.text = "Wounded";
		} else if(troopInfo.bLevel==1){
			unitBLevel.text = "Rookie";
		} else if(troopInfo.bLevel==2){
			unitBLevel.text = "Veteran";
		} else if(troopInfo.bLevel==3){
			unitBLevel.text = "Elite";
		} 
		
		if(troopInfo.cLevel==0){
			unitCLevel.text = "Wounded";
		} else if(troopInfo.cLevel==1){
			unitCLevel.text = "Rookie";
		} else if(troopInfo.cLevel==2){
			unitCLevel.text = "Veteran";
		} else if(troopInfo.cLevel==3){
			unitCLevel.text = "Elite";
		} 
	}

}
