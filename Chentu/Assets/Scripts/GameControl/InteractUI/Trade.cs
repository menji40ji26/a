using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trade : MonoBehaviour {

	public GameObject tradeWindow;
	public List<GameObject> playerItems;
	public List<GameObject> playerItemsInWindow;
	public GameObject itemSlotPrefab;
	public Transform playerItemWindow;


	void OnEnable(){
	}

	// Use this for initialization
	void Start () {
		
	}


	
	// Update is called once per frame
	void Update () {
	}

	public void LoadPlayerItems(){
		playerItems = new List<GameObject>();
		playerItems.Add(Inventory.inventory.item5);
		playerItems.Add(Inventory.inventory.item6);
		GenerateItemSlot();
	}

	void GenerateItemSlot(){
		for (int i = 0; i < playerItemsInWindow.Count; i++){
			Destroy(playerItemsInWindow[i]);
			playerItemsInWindow = new List<GameObject>();
		}


		for (int i = 0; i < playerItems.Count; i++)	{

			if(playerItems[i] != null){
				GameObject cargoClone = Instantiate(itemSlotPrefab, transform.position, transform.rotation);
				cargoClone.transform.SetParent(playerItemWindow);
				playerItemsInWindow.Add(cargoClone);
			}
		}
	}

	public void Close(){
		tradeWindow.SetActive(false);
	}
}
