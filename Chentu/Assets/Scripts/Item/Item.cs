using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour {

	public string name;
	public string tagName;
	public float type;
	
	//public bool sword;
	public bool bow;
	public bool crossbow;
	public bool spear;
	public bool axe;
	public bool twohand;
	public bool shield;
	public bool helmet;
	public bool armor;

	public float price;

	public int harvestType;
	public GameObject item01;
	public GameObject item02;
	public Sprite image;

	
	Vector3 dropOffset;

	void Start(){

		SetInfo();
	}

	void LateUpdate(){
		ShowItem();
	}

	void ShowItem(){
		if(!transform.parent & GetComponent<SpriteRenderer>()){
			if(!GetComponent<SpriteRenderer>().enabled){
				
				GetComponent<SpriteRenderer>().enabled = true;
			}
		}
	}

	public void Harvest(){

		if(harvestType==1){
			//DeadBoar
			item01 = ItemCollection.itemCollection.meat;
			item02 = ItemCollection.itemCollection.meat;
		} else if(harvestType==2){
			//DeadHorse
			item01 = ItemCollection.itemCollection.meat;
			item02 = ItemCollection.itemCollection.meat;
		}

		Transform player = GameObject.FindWithTag("Player").GetComponent<Transform>();
		
		if(harvestType>0){

			player.GetComponent<PlayerInteract>().operating = true;
		} else {

			if(player.GetComponent<AIClass>().back.transform.childCount==0){
				transform.SetParent(player.GetComponent<AIClass>().back.transform);
				transform.localPosition = new Vector3(-1,-3,0);
				GetComponent<SpriteRenderer>().sortingLayerName = "Enemy";
				GetComponent<SpriteRenderer>().sortingOrder = 3;
				BattleUI.battleUI.playerInventory.back1Item =  this;
			} else if(player.GetComponent<AIClass>().back2.transform.childCount==0){
				transform.SetParent(player.GetComponent<AIClass>().back2.transform);
				transform.localPosition = new Vector3(2,-3,0);
				GetComponent<SpriteRenderer>().sortingLayerName = "Enemy";
				GetComponent<SpriteRenderer>().sortingOrder = 3;
				BattleUI.battleUI.playerInventory.back2Item =  this;
			}

		}

	}

	public void DropItem(){
		if(harvestType==1){

			dropOffset = new Vector3(Random.Range(-0.5f,0.5f),Random.Range(-0.5f,0.5f),0);
			Instantiate(item01, transform.position + dropOffset, transform.rotation);
			if(Random.Range(0,100)>70){
				print("Drop another");
			dropOffset = new Vector3(Random.Range(-0.5f,0.5f),Random.Range(-0.5f,0.5f),0);
				Instantiate(item02, transform.position + dropOffset, transform.rotation);
			}

			Destroy(gameObject);
		} else if(harvestType==2){

			dropOffset = new Vector3(Random.Range(-0.5f,0.5f),Random.Range(-0.5f,0.5f),0);
			Instantiate(item01, transform.position + dropOffset, transform.rotation);
			if(Random.Range(0,100)>50){
				print("Drop another");
			dropOffset = new Vector3(Random.Range(-0.5f,0.5f),Random.Range(-0.5f,0.5f),0);
				Instantiate(item02, transform.position + dropOffset, transform.rotation);
			}

			Destroy(gameObject);
		}
	}

	public void ActiveImage(){
		GetComponent<SpriteRenderer>().enabled = true;
		GetComponent<SpriteRenderer>().sprite = image;
	}

	public void SetInfo(){
		if(name == "RingHeadedSword"){
			tagName = "Sword";
			type = 0;
			image = ItemCollection.itemCollection.ringHeadedSwordSprite;
			price = 25;
		} else if (name == "BronzeSword"){
			tagName = "Sword";
			type = 1;
			image = ItemCollection.itemCollection.bronzeSwordSprite;
			price = 31;
		} else if (name == "Axe"){
			tagName = "Axe";
			type = 1;
			image = ItemCollection.itemCollection.axeSprite;
			price = 15;
		} else if (name == "Crossbow"){
			tagName = "Crossbow";
			image = ItemCollection.itemCollection.crossbow01;
			price = 40;
		} else if (name == "Meat"){
			image = ItemCollection.itemCollection.meatSprite;
			price = 12;
		}
	}


}
