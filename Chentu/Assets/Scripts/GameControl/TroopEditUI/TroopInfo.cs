using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TroopInfo : MonoBehaviour {

	static public TroopInfo troopInfo;

	public bool skirmish;
	public Transform allyPortrait;
	public Transform enemyPortrait;
	public Transform playerUnits;

	bool sceneSwitch;

	// Use this for initialization

	void Start () {
		troopInfo = this;
		if(SceneManager.GetActiveScene().name=="TroopEdit"){
			//GameObject.DontDestroyOnLoad(gameObject);
			sceneSwitch = false;
			CheckTroopInfo();
		}

			SetTag();
			SetUnit();
			SetAITroop();
	}

	void CheckTroopInfo(){
		GameObject[] allTroopInfo = GameObject.FindGameObjectsWithTag("TroopInfo");
		if(transform.childCount == 0 & allTroopInfo.Length != 1){
			Destroy(this.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(!sceneSwitch & SceneManager.GetActiveScene().name=="Main"){
			GenerateSpawnZone();
			sceneSwitch = true;
		}
		
	}

	//Chariot
	public int chariotType;

	//Unit Class
	public List<string> unitModels;
	public string unitA; 
	public string unitB; 
	public string unitC; 

	//Unit Number
	public int total;
	public int a;
	public int b;
	public int c;

	//Equitment Tag
	public string tagAo;
	public string tagAm;
	public string tagBo;
	public string tagBm;
	public string tagCo;
	public string tagCm;

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
	


	void SetUnit(){

		chariotType = 0;

		unitA = "Unarmed";
		unitB = "Unarmed";
		unitC = "Unarmed";

		unitModels.Add(unitA);
		unitModels.Add(unitB);
		unitModels.Add(unitC);

		aMounted = false;
		bMounted = false;
		cMounted = false;

		aMountType = "Null";
		bMountType = "Null";
		cMountType = "Null";

		mainHandA = 0;
		offHandA = 0;
		armorA = 0;
		helmetA = 1;

		mainHandB = 0;
		offHandB = 0;
		armorB = 0;
		helmetB = 1;

		mainHandC = 0;
		offHandC = 0;
		armorC = 0;
		helmetC = 1;

		aLevel = 1;
		bLevel = 1;
		cLevel = 1;
	}

	void SetTag(){
		tagAo = "Untagged";
		tagAm = "Untagged";
		tagBo = "Untagged";
		tagBm = "Untagged";
		tagCo = "Untagged";
		tagCm = "Untagged";
	}

	public int ally1TroopModel;
	public int ally2TroopModel;
	public int enemy1TroopModel;
	public int enemy2TroopModel;
	public int enemy3TroopModel;

	public string enemy1Name;
	public string enemy2Name;
	public string enemy3Name;
	public string playerName;
	public string ally1Name;
	public string ally2Name;

	void SetAITroop(){

		if(ally1TroopModel == 0 ){
			ally1TroopModel = Random.Range(1,totalModels);
		}if(ally2TroopModel == 0 ){
			ally2TroopModel = Random.Range(1,totalModels);
		}if(enemy1TroopModel == 0 ){
			enemy1TroopModel = Random.Range(1,totalModels);
		}if(enemy2TroopModel == 0 ){
			enemy2TroopModel = Random.Range(1,totalModels);
		}if(enemy3TroopModel == 0 ){
			enemy3TroopModel = Random.Range(1,totalModels);
		}
	}



	public GameObject spawnZone;
	public GameObject playerSpawnZoneClone;
	public GameObject allySpawnZoneClone1;
	public GameObject allySpawnZoneClone2;
	public GameObject enemySpawnZoneClone1;
	public GameObject enemySpawnZoneClone2;
	public GameObject enemySpawnZoneClone3;

	void GenerateSpawnZone(){
		Vector3 playerSpawnZonePosition = new Vector3(-3f, -10, 0);
		playerSpawnZoneClone = Instantiate(spawnZone, playerSpawnZonePosition, transform.rotation);
		playerSpawnZoneClone.GetComponent<Spawn>().isPlayer = true;
		playerSpawnZoneClone.GetComponent<Spawn>().isAlly = false;
		playerSpawnZoneClone.GetComponent<Spawn>().troopModel = 0;
		playerSpawnZoneClone.GetComponent<Spawn>().leader = "Player";

		if(ally1TroopModel>0){
			Vector3 allySpawnZone1Position = new Vector3(-13f, -10, 0);
			allySpawnZoneClone1 = Instantiate(spawnZone, allySpawnZone1Position, transform.rotation);
			allySpawnZoneClone1.GetComponent<Spawn>().isPlayer = false;
			allySpawnZoneClone1.GetComponent<Spawn>().isAlly = true;
			allySpawnZoneClone1.GetComponent<Spawn>().troopModel = ally1TroopModel;
			allySpawnZoneClone1.GetComponent<Spawn>().leader = ally1Name;
		}

		if(ally2TroopModel>0){
			Vector3 allySpawnZone2Position = new Vector3(7f, -10, 0);
			allySpawnZoneClone2 = Instantiate(spawnZone, allySpawnZone2Position, transform.rotation);
			allySpawnZoneClone2.GetComponent<Spawn>().isPlayer = false;
			allySpawnZoneClone2.GetComponent<Spawn>().isAlly = true;
			allySpawnZoneClone2.GetComponent<Spawn>().troopModel = ally2TroopModel;
			allySpawnZoneClone2.GetComponent<Spawn>().leader = ally2Name;
		}

		if(enemy1TroopModel>0){
			Vector3 enemySpawnZone1Position = new Vector3(3f, 10, 0);
			enemySpawnZoneClone1 = Instantiate(spawnZone, enemySpawnZone1Position, transform.rotation);
			enemySpawnZoneClone1.GetComponent<Spawn>().isPlayer = false;
			enemySpawnZoneClone1.GetComponent<Spawn>().isAlly = false;
			enemySpawnZoneClone1.GetComponent<Spawn>().troopModel = enemy1TroopModel;
			enemySpawnZoneClone1.GetComponent<Spawn>().leader = enemy1Name;
			
		}

		if(enemy2TroopModel>0){
			Vector3 enemySpawnZone2Position = new Vector3(13f, 10, 0);
			enemySpawnZoneClone2 = Instantiate(spawnZone, enemySpawnZone2Position, transform.rotation);
			enemySpawnZoneClone2.GetComponent<Spawn>().isPlayer = false;
			enemySpawnZoneClone2.GetComponent<Spawn>().isAlly = false;
			enemySpawnZoneClone2.GetComponent<Spawn>().troopModel = enemy2TroopModel;
			enemySpawnZoneClone2.GetComponent<Spawn>().leader = enemy2Name;
		}


		if(enemy3TroopModel>0){
			Vector3 enemySpawnZone3Position = new Vector3(-7f, 10, 0);
			enemySpawnZoneClone3 = Instantiate(spawnZone, enemySpawnZone3Position, transform.rotation);
			enemySpawnZoneClone3.GetComponent<Spawn>().isPlayer = false;
			enemySpawnZoneClone3.GetComponent<Spawn>().isAlly = false;
			enemySpawnZoneClone3.GetComponent<Spawn>().troopModel = enemy3TroopModel;
			enemySpawnZoneClone3.GetComponent<Spawn>().leader = enemy3Name;
		}
	 }

	//Troop Model
	public int troopModel;

	//a few of Roving bandits
	public void TroopModel01(){
		
		chariotType = 0;

		unitA = "Bandit Crossbowman";
		unitB = "Bandit";
		unitC = "Bandit Spearman";

		total = 5;
		a = (int)( 0.3f * total); 
		b = (int)( 0.3f * total); 
		c = total - a - b;

		aMounted = false;
		bMounted = false;
		cMounted = false;

		aMountType = "Null";
		bMountType = "Null";
		cMountType = "Null";

		mainHandA = 0;
		offHandA = 0;
		armorA = 1;
		helmetA = 1;

		mainHandB = 1;
		offHandB = 1;
		armorB = 1;
		helmetB = 1;

		mainHandC = 2;
		offHandC = 0;
		armorC = 0;
		helmetC = 1;

		aLevel = 1;
		bLevel = 1;
		cLevel = 1;

		tagAo = "Crossbow";
		tagAm = "Untagged";
		tagBo = "Shield";
		tagBm = "Axe";
		tagCo = "Untagged";
		tagCm = "Spear";
		
	}
	
	//a few of Deserters
	public void TroopModel02(){
		
		chariotType = 0;

		unitA = "Deserters Spearman";
		unitB = "Deserters";
		unitC = "Deserters Captain";
	
		total = 5;
		a = (int)( 0.4f * total); 
		b = (int)( 0.2f * total); 
		c = total - a - b;

		aMounted = false;
		bMounted = false;
		cMounted = false;
		
		aMountType = "Null";
		bMountType = "Null";
		cMountType = "Null";

		mainHandA = 0;
		offHandA = 0;
		armorA = 2;
		helmetA = 2;

		mainHandB = 0;
		offHandB = 0;
		armorB = 1;
		helmetB = 1;

		mainHandC = 1;
		offHandC = 0;
		armorC = 2;
		helmetC = 2;

		aLevel = 2;
		bLevel = 1;
		cLevel = 3;

		tagAo = "Untagged";
		tagAm = "Spear";
		tagBo = "Untagged";
		tagBm = "Axe";
		tagCo = "Crossbow";
		tagCm = "Sword";
	
	}

	//a few of Majeok
	public void TroopModel03(){
		
		chariotType = 0;

		unitA = "Majeok";
		unitB = "Majeok Horse Archer";
		unitC = "Majeok";
	
		total = 3;
		a = (int)( 0.5f * total); 
		b = (int)( 0.4f * total); 
		c = total - a - b;

		aMounted = true;
		bMounted = true;
		cMounted = false;
		
		aMountType = "Null";
		bMountType = "Null";
		cMountType = "Null";

		mainHandA = 0;
		offHandA = 0;
		armorA = 0;
		helmetA = 1;

		mainHandB = 0;
		offHandB = 0;
		armorB = 1;
		helmetB = 1;

		mainHandC = 0;
		offHandC = 0;
		armorC = 0;
		helmetC = 0;

		aLevel = 1;
		bLevel = 2;
		cLevel = 1;

		tagAo = "Untagged";
		tagAm = "Sword";
		tagBo = "Bow";
		tagBm = "Axe";
		tagCo = "Untagged";
		tagCm = "Untagged";
	
	}


	//a gang of Deserters
	public void TroopModel04(){
		
		chariotType = 1;

		unitA = "Deserters Spearman";
		unitB = "Deserters";
		unitC = "Deserters Captain";
	
		total = 10;
		a = (int)( 0.3f * total); 
		b = (int)( 0.6f * total); 
		c = total - a - b;


		aMounted = false;
		bMounted = false;
		cMounted = false;
		
		aMountType = "Null";
		bMountType = "Null";
		cMountType = "Null";

		mainHandA = 0;
		offHandA = 0;
		armorA = 2;
		helmetA = 2;

		mainHandB = 0;
		offHandB = 0;
		armorB = 1;
		helmetB = 1;

		mainHandC = 1;
		offHandC = 0;
		armorC = 2;
		helmetC = 2;

		aLevel = 2;
		bLevel = 1;
		cLevel = 3;

		tagAo = "Untagged";
		tagAm = "Spear";
		tagBo = "Untagged";
		tagBm = "Axe";
		tagCo = "Crossbow";
		tagCm = "Sword";
	
	}

	//Garrison Few
	public void TroopModel05(){
		
		chariotType = 3;

		unitA = "Crossbowman";
		unitB = "Crossbowman";
		unitC = "Swordman";
	
		total = 9;
		a = (int)( 0.2f * total); 
		b = (int)( 0.4f * total); 
		c = total - a - b;


		aMounted = false;
		bMounted = false;
		cMounted = false;
		
		aMountType = "Null";
		bMountType = "Null";
		cMountType = "Null";

		mainHandA = 0;
		offHandA = 0;
		armorA = 2;
		helmetA = 1;

		mainHandB = 0;
		offHandB = 0;
		armorB = 2;
		helmetB = 1;

		mainHandC = 1;
		offHandC = 0;
		armorC = 2;
		helmetC = 2;

		aLevel = 1;
		bLevel = 1;
		cLevel = 2;

		tagAo = "Crossbow";
		tagAm = "Sword";
		tagBo = "Crossbow";
		tagBm = "Untagged";
		tagCo = "Shield";
		tagCm = "Sword";
	
	}


	//Nomad Few
	public void TroopModel06(){
		
		chariotType = 0;


		unitA = "Nomad Boar Archer";
		unitB = "Nomad Boar Lancer";
		unitC = "Nomad Boarman";
	
		total = 8;
		a = (int)( 0.3f * total); 
		b = (int)( 0.4f * total); 
		c = total - a - b;


		aMounted = false;
		bMounted = false;
		cMounted = true;
		
		aMountType = "Null";
		bMountType = "Null";
		cMountType = "Boar";

		mainHandA = 0;
		offHandA = 0;
		armorA = 3;
		helmetA = 3;

		mainHandB = 2;
		offHandB = 0;
		armorB = 3;
		helmetB = 3;

		mainHandC = 1;
		offHandC = 0;
		armorC = 3;
		helmetC = 3;

		aLevel = 1;
		bLevel = 1;
		cLevel = 2;

		tagAo = "Bow";
		tagAm = "Axe";
		tagBo = "Untagged";
		tagBm = "Spear";
		tagCo = "Untagged";
		tagCm = "Axe";
	
	}

	//Nomad Gang
	public void TroopModel07(){
		
		chariotType = 0;

		unitA = "Nomad Boar Archer";
		unitB = "Nomad Boar Lancer";
		unitC = "Nomad Boar Archer";

		total = 10;
		a = (int)( 0.6f * total); 
		b = (int)( 0.2f * total); 
		c = total - a - b;


		aMounted = false;
		bMounted = true;
		cMounted = true;
		
		aMountType = "Null";
		bMountType = "Boar";
		cMountType = "Boar";

		mainHandA = 0;
		offHandA = 0;
		armorA = 3;
		helmetA = 3;

		mainHandB = 1;
		offHandB = 0;
		armorB = 3;
		helmetB = 3;

		mainHandC = 1;
		offHandC = 0;
		armorC = 3;
		helmetC = 3;

		aLevel = 1;
		bLevel = 1;
		cLevel = 2;

		tagAo = "Bow";
		tagAm = "Axe";
		tagBo = "Untagged";
		tagBm = "Axe";
		tagCo = "Bow";
		tagCm = "Axe";
	
	}

	//Deserters (Army)
	public void TroopModel08(){
		
		chariotType = 2;

		unitA = "Deserters Spearman";
		unitB = "Deserters Crossbowman";
		unitC = "Deserters Captain";
	
		total = 16;
		a = (int)( 0.4f * total); 
		b = (int)( 0.3f * total); 
		c = total - a - b;


		aMounted = false;
		bMounted = false;
		cMounted = true;
		
		aMountType = "Null";
		bMountType = "Boar";
		cMountType = "Null";

		mainHandA = 0;
		offHandA = 0;
		armorA = 2;
		helmetA = 2;

		mainHandB = 1;
		offHandB = 0;
		armorB = 2;
		helmetB = 2;

		mainHandC = 1;
		offHandC = 0;
		armorC = 2;
		helmetC = 2;

		aLevel = 1;
		bLevel = 2;
		cLevel = 3;

		tagAo = "Shield";
		tagAm = "Spear";
		tagBo = "Crossbow";
		tagBm = "Sword";
		tagCo = "Bow";
		tagCm = "Spear";
	
	}

	//Edit Total Model Number Here, 1 greater than real number;
	int totalModels;

	void Awake(){

		GameObject.DontDestroyOnLoad(gameObject);
		totalModels = 9;
	}


	//TroopModelEditting guide;
	//TroopInfo ( SetAITroop ) ==> Spawn ( SetUnit )==> AITroopSelection 

}
