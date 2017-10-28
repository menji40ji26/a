using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour {


	public bool took;
	public bool reached;

	public string questName;
	public string questType;
	public float questAmount;
	public string region;
	public string goal;
	public string reward;
	public float rewardAmount;
	

	public string nodeA;
	public string a;
	public string nodeB;
	public string b;
	public string nodeC;
	public string c;



	// Use this for initialization
	void Start () {
		SetQuest();
		inventory = GameController.gameController.inventory;
	}

	void ClearQuest(){
		nodeA = null;
		a = null;
		nodeB = null;
		b = null;
		nodeC = null;
		c = null;
	}

	void FixedUpdate(){
		if(took)
		CheckGoal();
	}

	Inventory inventory;
	public GameObject target;

	void CheckGoal(){
		if(questType == "Item"){
			reached = false;
			if(inventory.item5){
				if(inventory.back1Item.name == goal)
					reached = true;
			} else if(inventory.item6){
				if(inventory.back1Item.name == goal)
					reached = true;
			}
		} else 
		if(questType == "Hunt"){
			reached = false;

			if(region == GameController.gameController.region.regionName){
				target = GameObject.FindWithTag("Wildlife");
				if(target){
					target.GetComponent<Animal>().isHuntTarget = true;
					target.GetComponent<Animal>().quest = this;
				}
			}

			if(questAmount <= 0){
				reached = true;
			}
		}

		if(questAmount < 0){
			questAmount = 0;
		}
	}



	public void Finish(){
		if(reward == "Prestige") {
			GameController.gameController.player.GetComponent<AI>().prestige += rewardAmount;
		}

		Destroy(this.gameObject);
	}
	
	public void SetQuest(){
		if(questName == "MEAT FOR SACRIFICE"){
			questType = "Item";

			nodeA =  "祭典期近，寒舍之下无物可献，不知作何是好……";
			a = "我去为足下寻些肉来。";
			nodeB = "如此无以为报，给你磕头了……";
			b = "这有一些肉，拿作祭祀用吧。";
			c = "我马上回来。";
			nodeC = "感激不尽……";

			goal = "Meat";
			reward = "Prestige";
			rewardAmount = 10;
		} else 

		if(questName == "BOAR IN THE FARM"){
			questType = "Hunt";

			nodeA =  "有野豕夜里拱庄稼。我儿数夜蹲守，那野豕察觉到人便不靠近。哪一夜无人又出没，实在无可奈何……";
			a = "我往林中走走，看能否有所猎获。";
			nodeB = "山野险恶多保重……";
			b = "射杀一头，只望其余近期不敢再在附近出没。";
			c = "我正在追猎。";
			nodeC = "足下技艺了得，多有劳驾。";

			goal = "Boar";
			questAmount = 1;
			region = GameController.gameController.region.regionName;
			reward = "Prestige";
			rewardAmount = 10;
		}
		
	}







}