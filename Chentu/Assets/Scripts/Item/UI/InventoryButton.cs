using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryButton : MonoBehaviour ,IPointerClickHandler{

	public int slotNumber;
	
	public Inventory playerInventory;
	public AIClass playerClass;

	// Use this for initialization
	void Start () {
		playerInventory = BattleUI.battleUI.playerInventory;
		playerClass = BattleUI.battleUI.playerClass;
		itemPrefab = ItemCollection.itemCollection.itemPrefab;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(!playerInventory)
		playerInventory = BattleUI.battleUI.playerInventory;
		if(!playerClass)
		playerClass = BattleUI.battleUI.playerClass;
		if(!itemPrefab)
		itemPrefab = ItemCollection.itemCollection.itemPrefab;


	}



	public void OnPointerClick(PointerEventData eventData) {
		//Drop item
       if (eventData.pointerId == -2 & !BattleUI.battleUI.inCargoRange){
		   if(slotNumber == 0 & BattleUI.battleUI.playerInventory.mainHandItem){
				playerInventory.mainHandItem.transform.gameObject.layer = LayerMask.NameToLayer("Item");
			  	playerClass.mainHand.transform.GetChild(0).parent = null;
				playerInventory.mainHandItem.GetComponent<SpriteRenderer>().sortingLayerName = "Item";
		   } else if(slotNumber == 1 & playerInventory.offHandItem){
				playerInventory.offHandItem.transform.gameObject.layer = LayerMask.NameToLayer("Item");
			  	playerClass.offHand.transform.GetChild(2).parent = null;
				playerInventory.offHandItem.GetComponent<SpriteRenderer>().sortingLayerName = "Item";
		   } else if(slotNumber == 2 & playerInventory.headItem){
				playerInventory.headItem.transform.gameObject.layer = LayerMask.NameToLayer("Item");
			  	playerClass.helmet.transform.GetChild(0).parent = null;
				playerInventory.headItem.GetComponent<SpriteRenderer>().sortingLayerName = "Item";
		   } else if(slotNumber == 3 & playerInventory.bodyItem){
				playerInventory.bodyItem.transform.gameObject.layer = LayerMask.NameToLayer("Item");
			  	playerClass.armor.transform.GetChild(0).parent = null;
				playerInventory.bodyItem.GetComponent<SpriteRenderer>().sortingLayerName = "Item";
		   } else if(slotNumber == 4 & playerInventory.back1Item){
				playerInventory.back1Item.transform.gameObject.layer = LayerMask.NameToLayer("Item");
			  	playerClass.back.transform.GetChild(0).parent = null;
				playerInventory.back1Item.GetComponent<SpriteRenderer>().sortingLayerName = "Item";
		   } else if(slotNumber == 5 & playerInventory.back2Item){
				playerInventory.back2Item.transform.gameObject.layer = LayerMask.NameToLayer("Item");
			  	playerClass.back2.transform.GetChild(0).parent = null;
				playerInventory.back2Item.GetComponent<SpriteRenderer>().sortingLayerName = "Item";
		   }
		} 
		//Transfer item to cargo
		else if (eventData.pointerId == -2 & BattleUI.battleUI.inCargoRange  & BattleUI.battleUI.cargoItems.Count <  BattleUI.battleUI.cargoCapacity){


			currentItem = new Item ();

		   if(slotNumber == 0 & playerClass.mainHand.transform.childCount != 0){

				BattleUI.battleUI.cargoItems.Add(  playerClass.mainHand.transform.GetChild(0).gameObject );
				playerClass.mainHand.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("NoInteract");
				playerClass.mainHand.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
			  	playerClass.mainHand.transform.GetChild(0).SetParent(BattleUI.battleUI.transform);

		   } else if(slotNumber == 1 & playerClass.offHand.transform.childCount != 2){

				BattleUI.battleUI.cargoItems.Add(  playerClass.offHand.transform.GetChild(2).gameObject );
				playerClass.offHand.transform.GetChild(2).gameObject.layer = LayerMask.NameToLayer("NoInteract");
				playerClass.offHand.transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
			  	playerClass.offHand.transform.GetChild(2).SetParent(BattleUI.battleUI.transform);

		   } else if(slotNumber == 2 & playerClass.armor.helmetType>1){

				BattleUI.battleUI.cargoItems.Add(  playerClass.bodyArmor.transform.GetChild(0).gameObject );
				playerClass.bodyArmor.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("NoInteract");
				playerClass.bodyArmor.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
			  	playerClass.bodyArmor.transform.GetChild(0).SetParent(BattleUI.battleUI.transform);

		   } else if(slotNumber == 3 & playerClass.armor.armorType>1){
			   
				BattleUI.battleUI.cargoItems.Add(  playerClass.helmet.transform.GetChild(0).gameObject );
				playerClass.helmet.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("NoInteract");
				playerClass.helmet.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
			  	playerClass.helmet.transform.GetChild(0).SetParent(BattleUI.battleUI.transform);

		   } else if(slotNumber == 4 & playerClass.back.transform.childCount>0){
				BattleUI.battleUI.cargoItems.Add(  playerClass.back.transform.GetChild(0).gameObject );
				playerClass.back.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("NoInteract");
				playerClass.back.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
			  	playerClass.back.transform.GetChild(0).SetParent(BattleUI.battleUI.transform);

		   } else if(slotNumber == 5 & playerClass.back2.transform.childCount>0){

				BattleUI.battleUI.cargoItems.Add(  playerClass.back2.transform.GetChild(0).gameObject );
				playerClass.back2.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("NoInteract");
				playerClass.back2.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
			  	playerClass.back2.transform.GetChild(0).SetParent(BattleUI.battleUI.transform);
		   } 

		if(slotNumber < 6)
		TransferItem();

		//Withdraw item from cargo
		} if (eventData.pointerId == -2 & BattleUI.battleUI.inCargoRange & slotNumber > 5 & BattleUI.battleUI.cargoItems.Count>slotNumber-6 ){
			if(playerClass.back.transform.childCount==0  ) {
				BattleUI.battleUI.cargoItems[slotNumber-6].transform.SetParent(playerClass.back.transform);
				BattleUI.battleUI.cargoItems[slotNumber-6].transform.localPosition = new Vector3(-1,-3,0);
				BattleUI.battleUI.cargoItems[slotNumber-6].GetComponent<SpriteRenderer>().sortingLayerName = "Enemy";
				BattleUI.battleUI.cargoItems[slotNumber-6].GetComponent<SpriteRenderer>().sortingOrder = 3;
				BattleUI.battleUI.playerInventory.back1Item =  BattleUI.battleUI.cargoItems[slotNumber-6].GetComponent<Item>();
				BattleUI.battleUI.cargoItems[slotNumber-6].GetComponent<Item>().ActiveImage();


				BattleUI.battleUI.cargoItems.RemoveAt(slotNumber-6);
				BattleUI.battleUI.ClearCargo();
			}else if(playerClass.back2.transform.childCount==0) {
				BattleUI.battleUI.cargoItems[slotNumber-6].transform.SetParent(playerClass.back2.transform);
				BattleUI.battleUI.cargoItems[slotNumber-6].transform.localPosition = new Vector3(2,-3,0);
				BattleUI.battleUI.cargoItems[slotNumber-6].GetComponent<SpriteRenderer>().sortingLayerName = "Enemy";
				BattleUI.battleUI.cargoItems[slotNumber-6].GetComponent<SpriteRenderer>().sortingOrder = 3;
				BattleUI.battleUI.playerInventory.back2Item =  BattleUI.battleUI.cargoItems[slotNumber-6].GetComponent<Item>();
				BattleUI.battleUI.cargoItems[slotNumber-6].GetComponent<Item>().ActiveImage();

				BattleUI.battleUI.cargoItems.RemoveAt(slotNumber-6);
				BattleUI.battleUI.ClearCargo();
			}
		}
		
		//Change Equitment
		 else if (eventData.pointerId == -1){
			 //Mainhand
			 if(playerClass.mainHand.transform.childCount==0 & slotNumber > 5 & BattleUI.battleUI.cargoItems.Count>slotNumber-6) {

				 if(BattleUI.battleUI.cargoItems[slotNumber-6].GetComponent<Item>().tagName == "Sword" | BattleUI.battleUI.cargoItems[slotNumber-6].GetComponent<Item>().tagName == "Axe" |BattleUI.battleUI.cargoItems[slotNumber-6].GetComponent<Item>().spear  ){
					BattleUI.battleUI.cargoItems[slotNumber-6].transform.SetParent(playerClass.mainHand.transform);
					BattleUI.battleUI.cargoItems[slotNumber-6].transform.localPosition = new Vector3(0,0,0);
					BattleUI.battleUI.cargoItems[slotNumber-6].GetComponent<SpriteRenderer>().sortingLayerName = "Enemy";
					BattleUI.battleUI.cargoItems[slotNumber-6].GetComponent<SpriteRenderer>().sortingOrder = 0;
					BattleUI.battleUI.playerInventory.mainHandItem = BattleUI.battleUI.cargoItems[slotNumber-6].GetComponent<Item>();
					BattleUI.battleUI.cargoItems[slotNumber-6].GetComponent<Item>().ActiveImage();

					BattleUI.battleUI.cargoItems.RemoveAt(slotNumber-6);
					BattleUI.battleUI.ClearCargo();
				 }
			} else if(playerClass.mainHand.transform.childCount==0 & slotNumber == 4 & playerClass.back.transform.childCount != 0) {
				if(playerClass.back.transform.GetChild(0).GetComponent<Item>().tagName == "Sword" | playerClass.back.transform.GetChild(0).GetComponent<Item>().tagName == "Axe" |playerClass.back.transform.GetChild(0).GetComponent<Item>().spear  ) {
					playerClass.back.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = "Enemy";
					playerClass.back.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 0;
					BattleUI.battleUI.playerInventory.mainHandItem = playerClass.back.transform.GetChild(0).GetComponent<Item>();
					playerClass.back.transform.GetChild(0).SetParent(playerClass.mainHand.transform);
				}
			} else if(playerClass.mainHand.transform.childCount==0 & slotNumber == 5 & playerClass.back2.transform.childCount != 0) {
				if(playerClass.back2.transform.GetChild(0).GetComponent<Item>().tagName == "Sword" | playerClass.back2.transform.GetChild(0).GetComponent<Item>().tagName == "Axe" |playerClass.back2.transform.GetChild(0).GetComponent<Item>().spear  ) {
					playerClass.back2.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = "Enemy";
					playerClass.back2.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 0;
					BattleUI.battleUI.playerInventory.mainHandItem = playerClass.back2.transform.GetChild(0).GetComponent<Item>();
					playerClass.back2.transform.GetChild(0).SetParent(playerClass.mainHand.transform);
				}
			}
			//Take off
			else if(playerClass.mainHand.transform.childCount==1 & slotNumber == 0 & playerClass.back.transform.childCount == 0) {
				if(playerClass.mainHand.transform.GetChild(0).GetComponent<Item>().tagName == "Sword" | playerClass.mainHand.transform.GetChild(0).GetComponent<Item>().axe |playerClass.mainHand.transform.GetChild(0).GetComponent<Item>().spear  ) {
					playerClass.mainHand.transform.GetChild(0).SetParent(playerClass.back.transform);
				}
			} else if(playerClass.mainHand.transform.childCount==1 & slotNumber == 0 & playerClass.back2.transform.childCount == 0) {
				if(playerClass.mainHand.transform.GetChild(0).GetComponent<Item>().tagName == "Sword" | playerClass.mainHand.transform.GetChild(0).GetComponent<Item>().axe |playerClass.mainHand.transform.GetChild(0).GetComponent<Item>().spear  ) {
					playerClass.mainHand.transform.GetChild(0).SetParent(playerClass.back2.transform);
				}
			}


			//Offhand
			if(playerClass.offHand.transform.childCount==2 & slotNumber > 5 & BattleUI.battleUI.cargoItems.Count>slotNumber-6) {
			
				if(BattleUI.battleUI.cargoItems[slotNumber-6].GetComponent<Item>().shield | BattleUI.battleUI.cargoItems[slotNumber-6].GetComponent<Item>().crossbow |BattleUI.battleUI.cargoItems[slotNumber-6].GetComponent<Item>().bow  ){
					BattleUI.battleUI.cargoItems[slotNumber-6].transform.SetParent(playerClass.offHand.transform);
					BattleUI.battleUI.cargoItems[slotNumber-6].transform.localPosition = new Vector3(0,0,0);
					BattleUI.battleUI.cargoItems[slotNumber-6].GetComponent<SpriteRenderer>().sortingLayerName = "Enemy";
					BattleUI.battleUI.cargoItems[slotNumber-6].GetComponent<SpriteRenderer>().sortingOrder = 0;
					BattleUI.battleUI.playerInventory.offHandItem = BattleUI.battleUI.cargoItems[slotNumber-6].GetComponent<Item>();
					BattleUI.battleUI.cargoItems[slotNumber-6].GetComponent<Item>().ActiveImage();

					BattleUI.battleUI.cargoItems.RemoveAt(slotNumber-6);
					BattleUI.battleUI.ClearCargo();
				}
			} else if(playerClass.offHand.transform.childCount==2 & slotNumber == 4 & playerClass.back.transform.childCount != 0) {
				if(playerClass.back.transform.GetChild(0).GetComponent<Item>().shield | playerClass.back.transform.GetChild(0).GetComponent<Item>().crossbow |playerClass.back.transform.GetChild(0).GetComponent<Item>().bow  ) {
					playerClass.back.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = "Enemy";
					playerClass.back.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 0;
					BattleUI.battleUI.playerInventory.offHandItem = playerClass.back.transform.GetChild(0).GetComponent<Item>();
					playerClass.back.transform.GetChild(0).SetParent(playerClass.offHand.transform);
				}
			} else if(playerClass.offHand.transform.childCount==2 & slotNumber == 5 & playerClass.back2.transform.childCount != 0) {
				if(playerClass.back2.transform.GetChild(0).GetComponent<Item>().shield | playerClass.back2.transform.GetChild(0).GetComponent<Item>().crossbow |playerClass.back2.transform.GetChild(0).GetComponent<Item>().bow  ) {
					playerClass.back2.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = "Enemy";
					playerClass.back2.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 0;
					BattleUI.battleUI.playerInventory.offHandItem = playerClass.back2.transform.GetChild(0).GetComponent<Item>();
					playerClass.back2.transform.GetChild(0).SetParent(playerClass.offHand.transform);
				}
			}
			//Take off
			else if(playerClass.offHand.transform.childCount==3 & slotNumber == 1 & playerClass.back.transform.childCount == 0) {
				if(playerClass.offHand.transform.GetChild(2).GetComponent<Item>().shield | playerClass.offHand.transform.GetChild(2).GetComponent<Item>().crossbow |playerClass.offHand.transform.GetChild(2).GetComponent<Item>().bow  ) {
					playerClass.offHand.transform.GetChild(2).SetParent(playerClass.back.transform);
				}
			} else if(playerClass.offHand.transform.childCount==3 & slotNumber == 1 & playerClass.back2.transform.childCount == 0) {
				if(playerClass.offHand.transform.GetChild(2).GetComponent<Item>().shield | playerClass.offHand.transform.GetChild(2).GetComponent<Item>().crossbow |playerClass.offHand.transform.GetChild(2).GetComponent<Item>().bow  ) {
					playerClass.offHand.transform.GetChild(2).SetParent(playerClass.back2.transform);
				}
			}
		}


	}

	Item currentItem;
	GameObject itemPrefab;
	GameObject itemClone;

	void TransferItem(){
		if( currentItem.name!=null){

				print("Run");

			itemClone = Instantiate(itemPrefab, transform.position, transform.rotation);
			itemClone.GetComponent<Item>().name = currentItem.name;
			BattleUI.battleUI.cargoItems.Add( itemClone );
		}

	}


	public void MouseOverUI(){
		if(playerClass)
		playerClass.thisPlayer.mouseOnUI = true;
	}

	public void MouseLeftUI(){
		playerClass.thisPlayer.mouseOnUI = false;
	}

}
