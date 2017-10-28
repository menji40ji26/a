using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour {

	static public BattleUI battleUI;
	public bool inCargoRange;
	//public bool cargoOpened;
	//Inventory
	public Image mainHandItemImage;
	public Image offHandItemImage;
	public Image headItemImage;
	public Image bodyItemImage;
	public Image back1ItemImage;
	public Image back2ItemImage;

	public string mainHandItemName;
	public string offHandItemName;
	public string headItemName;
	public string bodyItemName;
	public string back1ItemName;
	public string back2ItemName;

	public int cargoType;
	public int cargoCapacity;
	public List<GameObject> cargoItems;

	public Inventory playerInventory;
	public AIClass playerClass;
	public PlayerFight playerFight;

	// Use this for initialization
	void Awake () {
		battleUI = this;
		SetCargo();
		playerInventory = transform.GetChild(1).GetComponent<Inventory>();
	}

	public Transform cargo;
	public List<GameObject> itemSlots;
	public GameObject itemSlotPrefab;
	GameObject cargoSlotClone;

	void FixedUpdate(){
		CheckCargo();
	}


	public void MouseOverUI(){
		if(playerClass)
		playerClass.thisPlayer.mouseOnUI = true;
	}

	public void MouseLeftUI(){
		if(playerClass)
		playerClass.thisPlayer.mouseOnUI = false;
	}

	void SetCargo(){

		//Horse 
		if(cargoType==0){
			cargoCapacity = 6;
			for (int i = 0; i < cargoCapacity; i++){
				cargoSlotClone = Instantiate(itemSlotPrefab, cargo.position, cargo.rotation);
				itemSlots.Add(cargoSlotClone);
				cargoSlotClone.GetComponent<InventoryButton>().slotNumber = i + 6;
				cargoSlotClone.transform.SetParent(cargo);
			}
		}
		cargo.gameObject.SetActive(false);
	}

	void CheckCargo(){

		//Close Cargo
		if(Input.GetKeyDown(KeyCode.Escape)){
				cargo.gameObject.SetActive(false);
				//cargoOpened = false;
		}

		for (int i = 0; i < cargoItems.Count; i++){
			itemSlots[i].transform.GetChild(1).GetComponent<Image>().sprite = cargoItems[i].GetComponent<Item>().image;
		}
	}

	public void ClearCargo(){

		for (int i = 0; i < itemSlots.Count; i++){
			itemSlots[i].transform.GetChild(1).GetComponent<Image>().sprite = ItemCollection.itemCollection.itemIcon;
		}
	}

	
}
