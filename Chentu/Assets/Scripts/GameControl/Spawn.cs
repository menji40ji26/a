using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

	//Chariot Type
	public int chariotType;

	//Troop Model
	public int troopModel;

	//Unit Class
	//public List<string> unitModels;
	public string captainName;
	public string unitA; 
	public string unitB; 
	public string unitC; 


	//Unit Number
	public int a;
	public int b;
	public int c;

	//Equitment
	public int offHandA;
	public int offHandB;
	public int offHandC;

	public int mainHandA;
	public int mainHandB;
	public int mainHandC;

	public int armorA;
	public int armorB;
	public int armorC;

	public int helmetA;
	public int helmetB;
	public int helmetC;


	//Condition
	public int aLevel;
	public int bLevel;
	public int cLevel;

	public bool aMounted;
	public bool bMounted;
	public bool cMounted;

	public string aMountType;
	public string bMountType;
	public string cMountType;

	public bool isPlayer;
	public bool isAlly;


	public GameObject chariotPrefab;
	public GameObject playerPrefab;
	public GameObject manPrefab;
	public GameObject horsePrefab;
	public GameObject boarPrefab;
	GameObject unitPrefab;

	GameObject chariotClone;
	GameObject playerClone;
	GameObject captainClone;
	GameObject unitClone;
	GameObject enemyClone;
	GameObject horseClone;

	public TroopInfo troopInfo;

	void Start () {
		spawned = false;
		if(!isPlayer&!isAlly)
		transform.Rotate(0,0,180);
	}

	bool spawned;

	void Update(){
		if(!spawned){
			troopInfo = GameObject.FindWithTag("TroopInfo").GetComponent<TroopInfo>();
			SetUnit();
			SetChariot();
			//SetItemTag();
			if(isPlayer){
				unitPrefab = manPrefab;
			} else if(!isPlayer & isAlly){
				captainClone = Instantiate(manPrefab, transform.GetChild(1).transform.position, transform.rotation);
				unitPrefab = manPrefab;
				captainClone.GetComponent<AIClass>().name = leader;
			} else if(!isPlayer & !isAlly){
				captainClone = Instantiate(manPrefab, transform.GetChild(1).transform.position, transform.rotation);
				unitPrefab = manPrefab;

				captainClone.tag = "Enemy";
				captainClone.layer = LayerMask.NameToLayer( "Enemy" );
				captainClone.GetComponent<AIClass>().name = leader;
				captainClone.GetComponent<AIFight>().head.GetComponent<SpriteRenderer>().color = new Color(0.75f, 0.25f, 0.25f, 1);
				captainClone.GetComponent<AIFight>().body.GetComponent<SpriteRenderer>().color = new Color(0.75f, 0.25f, 0.25f, 1);
				captainClone.GetComponent<AIFight>().leftHand.GetComponent<SpriteRenderer>().color = new Color(0.75f, 0.25f, 0.25f, 1);
				captainClone.GetComponent<AIFight>().rightHand.GetComponent<SpriteRenderer>().color = new Color(0.75f, 0.25f, 0.25f, 1);

			}
			SpawnUnit();
			spawned = true;
		}
		
	}


	void SetChariot(){
		chariotType = troopInfo.chariotType;
		if(chariotType==0){
			chariotPrefab = null;
		} else if (chariotType==1){
			chariotPrefab = ItemCollection.itemCollection.chariot01;
		} else if (chariotType==2){
			chariotPrefab = ItemCollection.itemCollection.chariot02;
		} else if (chariotType==3){
			chariotPrefab = ItemCollection.itemCollection.chariot03;
		}

		if(chariotPrefab)
		chariotClone = Instantiate(chariotPrefab, transform.position, transform.rotation);
	}


	string tagAo;
	string tagAm;
	string tagBo;
	string tagBm;
	string tagCo;
	string tagCm;

	public void SetUnit(){

		if(troopModel==1){
			//A few of Roving bandits
			troopInfo.TroopModel01();
		} else if(troopModel==2){
			//a few of Deserters
			troopInfo.TroopModel02();
		} else if(troopModel==3){
			//a few of Majeok
			troopInfo.TroopModel03();
		} else if(troopModel==4){
			//a gang of Deserters
			troopInfo.TroopModel04();
		} else if(troopModel==5){
			//Garrison (Few)
			troopInfo.TroopModel05();
		} else if(troopModel==6){
			//Nomad (Few)
			troopInfo.TroopModel06();
		} else if(troopModel==7){
			//Nomad (Gang)
			troopInfo.TroopModel07();
		} else if(troopModel==8){
			//Deserters (Army)
			troopInfo.TroopModel08();
		}

		unitA = troopInfo.unitA;
		unitB = troopInfo.unitB;
		unitC = troopInfo.unitC;

		a = troopInfo.a;
		b = troopInfo.b;
		c = troopInfo.c;

		aMounted =  troopInfo.aMounted;
		bMounted = troopInfo.bMounted;
		cMounted = troopInfo.cMounted;

		aMountType =  troopInfo.aMountType;
		bMountType = troopInfo.bMountType;
		cMountType = troopInfo.cMountType;

		mainHandA = troopInfo.mainHandA;
		offHandA = troopInfo.offHandA;
		armorA = troopInfo.armorA;
		helmetA = troopInfo.helmetA;

		mainHandB = troopInfo.mainHandB;
		offHandB = troopInfo.offHandB;
		armorB = troopInfo.armorB;
		helmetB = troopInfo.helmetB;

		mainHandC = troopInfo.mainHandC;
		offHandC = troopInfo.offHandC;
		armorC = troopInfo.armorC;
		helmetC = troopInfo.helmetC;

		aLevel = troopInfo.aLevel;
		bLevel = troopInfo.bLevel;
		cLevel = troopInfo.cLevel;


		tagAo = troopInfo.tagAo;
		tagAm = troopInfo.tagAm;
		tagBo = troopInfo.tagBo;
		tagBm = troopInfo.tagBm;
		tagCo = troopInfo.tagCo;
		tagCm = troopInfo.tagCm;
	}





	void SetEnemy(){
		if(!isPlayer & !isAlly){
			unitClone.tag = "Enemy";
			unitClone.layer = LayerMask.NameToLayer( "Enemy" );

			unitClone.GetComponent<AIFight>().head.GetComponent<SpriteRenderer>().color = new Color(0.75f, 0.25f, 0.25f, 1);
			unitClone.GetComponent<AIFight>().body.GetComponent<SpriteRenderer>().color = new Color(0.75f, 0.25f, 0.25f, 1);
			unitClone.GetComponent<AIFight>().leftHand.GetComponent<SpriteRenderer>().color = new Color(0.75f, 0.25f, 0.25f, 1);
			unitClone.GetComponent<AIFight>().rightHand.GetComponent<SpriteRenderer>().color = new Color(0.75f, 0.25f, 0.25f, 1);


		}
	}

	public float firstAid;

	void SpawnUnit(){
		if(!isPlayer){
			for (int i = 2; i < a + 2; i++){
				unitClone = Instantiate(unitPrefab, transform.GetChild(i).transform.position, transform.rotation);
				SpawnA(i);
				SetEnemy();
			}
			for (int i = a + 2; i < a + b + 2; i++){
				unitClone = Instantiate(unitPrefab, transform.GetChild(i).transform.position, transform.rotation);
				SpawnB(i);
				SetEnemy();

			}
			for (int i = a + b + 2; i < a + b + c + 2; i++){
				unitClone = Instantiate(unitPrefab, transform.GetChild(i).transform.position, transform.rotation);
				SpawnC(i);
				SetEnemy();

			}
		} else if(isPlayer){
			SpawnPlayerUnits();
		}

	}

	void SpawnPlayerUnits(){
		TroopInfo troopInfo = GameObject.FindWithTag("TroopInfo").GetComponent<TroopInfo>();
		for (int i = 0; i < troopInfo.playerUnits.childCount; i++){
			print("SpawnPlayerUnits");
			unitClone = Instantiate(unitPrefab, transform.GetChild(i).transform.position, transform.rotation);
			unitClone.GetComponent<AIClass>().name = troopInfo.playerUnits.GetChild(i).GetComponent<Unit>().name;
			unitClone.GetComponent<AIClass>().level = troopInfo.playerUnits.GetChild(i).GetComponent<Unit>().level;
			unitClone.GetComponent<AIClass>().offHand.tag = troopInfo.playerUnits.GetChild(i).GetComponent<Unit>().offHandTag;
			unitClone.GetComponent<AIClass>().mainHand.tag = troopInfo.playerUnits.GetChild(i).GetComponent<Unit>().mainHandTag;
			unitClone.GetComponent<AIClass>().mainHandItem = troopInfo.playerUnits.GetChild(i).GetComponent<Unit>().mainHandItem; 
			unitClone.GetComponent<AIClass>().offHandItem = troopInfo.playerUnits.GetChild(i).GetComponent<Unit>().offHandItem; 
			unitClone.GetComponent<Armor>().armorType = troopInfo.playerUnits.GetChild(i).GetComponent<Unit>().armorType; 
			unitClone.GetComponent<Armor>().helmetType = troopInfo.playerUnits.GetChild(i).GetComponent<Unit>().helmetType; 
			if(troopInfo.playerUnits.GetChild(i).GetComponent<Unit>().mounted){
				if(troopInfo.playerUnits.GetChild(i).GetComponent<Unit>().mountType == "Boar"){
					horseClone = Instantiate(boarPrefab, transform.GetChild(i).transform.position, transform.rotation);
				} else {
					horseClone = Instantiate(horsePrefab, transform.GetChild(i).transform.position, transform.rotation);
				}
			}
			unitClone.GetComponent<AIClass>().unitCard = troopInfo.playerUnits.GetChild(i).gameObject;
			SetLeader(unitClone);
		}

		
	}


	public string leader;

	void SetLeader(GameObject unitClone){
		unitClone.GetComponent<AIClass>().leader = leader;
	}

	void SpawnA(int i){
		unitClone.GetComponent<AIClass>().name = troopInfo.unitA;
		unitClone.GetComponent<AIClass>().level = aLevel;
		unitClone.GetComponent<AIClass>().offHand.tag = tagAo;
		unitClone.GetComponent<AIClass>().mainHand.tag = tagAm;
		unitClone.GetComponent<AIClass>().mainHandItem = mainHandA; 
		unitClone.GetComponent<AIClass>().offHandItem = offHandA; 
		unitClone.GetComponent<Armor>().armorType = armorA; 
		unitClone.GetComponent<Armor>().helmetType = helmetA; 
		if(aMounted){
			if(aMountType == "Boar"){
				horseClone = Instantiate(boarPrefab, transform.GetChild(i).transform.position, transform.rotation);
			} else {
				horseClone = Instantiate(horsePrefab, transform.GetChild(i).transform.position, transform.rotation);
			}
		}
		SetLeader(unitClone);
	}
	void SpawnB(int i){
		unitClone.GetComponent<AIClass>().name = troopInfo.unitB;
		unitClone.GetComponent<AIClass>().level = bLevel;
		unitClone.GetComponent<AIClass>().offHand.tag = tagBo;
		unitClone.GetComponent<AIClass>().mainHand.tag = tagBm;
		unitClone.GetComponent<AIClass>().mainHandItem = mainHandB; 
		unitClone.GetComponent<AIClass>().offHandItem = offHandB; 
		unitClone.GetComponent<Armor>().armorType = armorB; 
		unitClone.GetComponent<Armor>().helmetType = helmetB; 
		if(bMounted){
			if(bMountType == "Boar"){
				horseClone = Instantiate(boarPrefab, transform.GetChild(i).transform.position, transform.rotation);
			} else {
				horseClone = Instantiate(horsePrefab, transform.GetChild(i).transform.position, transform.rotation);
			}
		}
		SetLeader(unitClone);
	}
	void SpawnC(int i){
		unitClone.GetComponent<AIClass>().name = troopInfo.unitC;
		unitClone.GetComponent<AIClass>().level = cLevel;
		unitClone.GetComponent<AIClass>().offHand.tag = tagCo;
		unitClone.GetComponent<AIClass>().mainHand.tag = tagCm;
		unitClone.GetComponent<AIClass>().mainHandItem = mainHandC; 
		unitClone.GetComponent<AIClass>().offHandItem = offHandC; 
		unitClone.GetComponent<Armor>().armorType = armorC; 
		unitClone.GetComponent<Armor>().helmetType = helmetC; 
		if(cMounted){
			if(cMountType == "Boar"){
				horseClone = Instantiate(boarPrefab, transform.GetChild(i).transform.position, transform.rotation);
			} else {
				horseClone = Instantiate(horsePrefab, transform.GetChild(i).transform.position, transform.rotation);
			}
		}
		SetLeader(unitClone);
	}

}
