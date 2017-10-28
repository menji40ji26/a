using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	static public Inventory inventory;

	public Item mainHandItem;
	public Item offHandItem;
	public Item headItem;
	public Item bodyItem;
	public Item back1Item;
	public Item back2Item;

	public GameObject item1;
	public GameObject item2;
	public GameObject item3;
	public GameObject item4;
	public GameObject item5;
	public GameObject item6;

	public float gold;

	public AIClass aiClass;
	public GameObject itemPrefab;

	// Use this for initialization
	// Update is called once per frame
	void Awake(){
		inventory = this;
	}

	void Start(){
		loaded = false;
	}


	public string tempItem1;
	bool loaded;

	void LoadItem(){
		if(PlayerPrefs.GetString("item1") != "Null") {
			tempItem1 = PlayerPrefs.GetString("item1");
			GameObject item1Clone = Instantiate(itemPrefab, aiClass.mainHand.transform.position, aiClass.mainHand.transform.rotation);
			item1Clone.transform.SetParent(aiClass.mainHand.transform);
			item1Clone.GetComponent<Item>().name = PlayerPrefs.GetString("item1");
			print("loaded");
		} else if(aiClass.mainHand.transform.childCount!=0){
			Destroy(aiClass.mainHand.transform.GetChild(0));

			print("Clear");
		}
	}

	void FixedUpdate () {



		if(aiClass)
		if(aiClass.isPlayer){


			aiClass.CheckClass();
			CheckItem();
			if(!loaded){
				LoadItem();
				loaded = true;
			}
		}
	}


	public void CheckItem(){



		//MainHand

		if(aiClass.mainHand.transform.childCount != 0 ){
			item1 = aiClass.mainHand.transform.GetChild(0).gameObject;
			mainHandItem = item1.GetComponent<Item>();
			aiClass.mainHand.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
			if(mainHandItem.tagName == "Sword"){
				aiClass.mainHand.tag = mainHandItem.tagName;
				aiClass.sword.name = mainHandItem.name;
				aiClass.sword.CheckType();
				aiClass.mainHandItem = mainHandItem.type;
				if(mainHandItem.type==0)
					aiClass.mainHand.GetComponent<SpriteRenderer>().sprite = ItemCollection.itemCollection.sword01;
				else if(mainHandItem.type==1){
					aiClass.mainHand.GetComponent<SpriteRenderer>().sprite = ItemCollection.itemCollection.sword02;
				}

			} else if(mainHandItem.tagName == "Axe"){
				aiClass.mainHand.tag = mainHandItem.tagName;
				aiClass.axe.name = mainHandItem.name;
				aiClass.axe.CheckType();
				aiClass.mainHandItem = mainHandItem.type;
				
				if(mainHandItem.type==0)
					aiClass.mainHand.GetComponent<SpriteRenderer>().sprite = ItemCollection.itemCollection.axe01;
				else if(mainHandItem.type==1){
					aiClass.mainHand.GetComponent<SpriteRenderer>().sprite = ItemCollection.itemCollection.axe02;
				}

			}

			//Icon
			BattleUI.battleUI.mainHandItemImage.sprite = mainHandItem.image;
		} else {

			aiClass.mainHand.tag = ("Untagged");
			mainHandItem = null;
			BattleUI.battleUI.mainHandItemImage.sprite = ItemCollection.itemCollection.itemIcon;
		}

		//OffHand

		if(aiClass.offHand.transform.childCount != 2 ){

			item2 = aiClass.offHand.transform.GetChild(2).gameObject;
			offHandItem = item2.GetComponent<Item>();
			aiClass.offHand.transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
			if(offHandItem.crossbow){
				aiClass.offHand.tag = "Crossbow";
				aiClass.bow.name = offHandItem.name;
				aiClass.bow.CheckType();
				aiClass.offHandItem = offHandItem.type;
				aiClass.mainHand.GetComponent<SpriteRenderer>().sprite = ItemCollection.itemCollection.crossbowArrow01;
			}


			BattleUI.battleUI.offHandItemImage.sprite = offHandItem.image;
		} else {

			aiClass.offHand.tag = ("Untagged");
			offHandItem = null;
			BattleUI.battleUI.offHandItemImage.sprite = ItemCollection.itemCollection.itemIcon;
		}


		//Head
		if(aiClass.helmet.transform.childCount != 0){
			
			item3 = aiClass.helmet.transform.GetChild(0).gameObject;
			bodyItem = aiClass.helmet.transform.GetChild(0).GetComponent<Item>();
			BattleUI.battleUI.headItemImage.sprite = headItem.image;

		} else {
			headItem = null;
			aiClass.helmet.GetComponent<SpriteRenderer>().sprite = ItemCollection.itemCollection.helmet01;
			BattleUI.battleUI.headItemImage.sprite = ItemCollection.itemCollection.itemIcon;
		}


		//Body
		if(aiClass.bodyArmor.transform.childCount != 0){
			
			item4 = aiClass.bodyArmor.transform.GetChild(0).gameObject;
			bodyItem = aiClass.bodyArmor.transform.GetChild(0).GetComponent<Item>();
			BattleUI.battleUI.bodyItemImage.sprite = bodyItem.image;

		} else {
			bodyItem = null;
			aiClass.bodyArmor.GetComponent<SpriteRenderer>().sprite = null;
			BattleUI.battleUI.bodyItemImage.sprite = ItemCollection.itemCollection.itemIcon;
		}




		//Back
		if(aiClass.back.transform.childCount == 0){
			back1Item = null;
			BattleUI.battleUI.back1ItemImage.sprite = ItemCollection.itemCollection.itemIcon;
		} else {
			item5 = aiClass.back.transform.GetChild(0).gameObject;
			back1Item = aiClass.back.transform.GetChild(0).GetComponent<Item>();
			BattleUI.battleUI.back1ItemImage.sprite = back1Item.image;
		}

		if(aiClass.back2.transform.childCount == 0){
			back2Item = null;
			BattleUI.battleUI.back2ItemImage.sprite = ItemCollection.itemCollection.itemIcon;
		} else {
			item6 = aiClass.back2.transform.GetChild(0).gameObject;
			back2Item = aiClass.back2.transform.GetChild(0).GetComponent<Item>();
			BattleUI.battleUI.back2ItemImage.sprite = back2Item.image;
		}
	}
}
