using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSeletionButton : MonoBehaviour {

	public string unitSequence;
	public ItemSeletion itemSeletion;
	public TroopInfo troopInfo;
	public Transform itemList;

	public GameObject unitItemSeletion;

	// Use this for initialization
	void Start () {
		//itemSeletion = transform.parent.GetComponent<ItemSeletion>();
		troopInfo = GameObject.FindWithTag("TroopInfo").GetComponent<TroopInfo>();

	}
	
	// Update is called once per frame
	void Update () {
	}

	public void SeleteUnit(){
		itemSeletion.currentEditingUnit = unitSequence; 
		unitItemSeletion.SetActive(true);
		itemList.transform.position = new Vector3(transform.position.x - 0.32f, itemList.transform.position.y, 0);

	}




	public void AddUnitNumber(){

		ItemSeletion.itemSeletion.CloseUnitItemSeletion();

		ItemSeletion.itemSeletion.CountCost();

		if(unitSequence == "a" & ItemSeletion.itemSeletion.currentGold>=ItemSeletion.itemSeletion.unitATotalCost ){
			troopInfo.a ++;
			TroopScale.troopScale.AddUnitA();
			
		}
		else if(unitSequence == "b" & ItemSeletion.itemSeletion.currentGold>=ItemSeletion.itemSeletion.unitBTotalCost ){
			troopInfo.b ++;
			TroopScale.troopScale.AddUnitB();
		}
		else if(unitSequence == "c" & ItemSeletion.itemSeletion.currentGold>=ItemSeletion.itemSeletion.unitCTotalCost ){
			troopInfo.c ++;
			TroopScale.troopScale.AddUnitC();
		} else {
			Hint.hint.InsufficientFund();
		}
		
	}

	public void DismissUnit(){


		ItemSeletion.itemSeletion.CloseUnitItemSeletion();

		if(unitSequence == "a" & troopInfo.a > 0){
			troopInfo.a --;
			ItemSeletion.itemSeletion.unitANumber--;
		}
		else if(unitSequence == "b" & troopInfo.b > 0){
			troopInfo.b --;
			ItemSeletion.itemSeletion.unitBNumber--;
		}
		else if(unitSequence == "c" & troopInfo.c > 0){
			troopInfo.c --;
			ItemSeletion.itemSeletion.unitCNumber--;
		}
		ItemSeletion.itemSeletion.CountCost();
	}

	public void Promote(){


		ItemSeletion.itemSeletion.CloseUnitItemSeletion();

		if(unitSequence == "a" & troopInfo.aLevel < 3 ){
			troopInfo.aLevel ++;
			TroopScale.troopScale.PromoteListA();
		} else if(unitSequence == "b" & troopInfo.bLevel < 3 ){
			troopInfo.bLevel ++;
			TroopScale.troopScale.PromoteListB();
		} else if(unitSequence == "c" & troopInfo.cLevel < 3 ){
			troopInfo.cLevel ++;
			TroopScale.troopScale.PromoteListC();
		} 

		ItemSeletion.itemSeletion.CountCost();

	}

	public void Demote(){


		ItemSeletion.itemSeletion.CloseUnitItemSeletion();

		if(unitSequence == "a" & troopInfo.aLevel > 1 ){
			troopInfo.aLevel --;
			TroopScale.troopScale.DemoteListA();
		} else if(unitSequence == "b" & troopInfo.bLevel > 1 ){
			troopInfo.bLevel --;
			TroopScale.troopScale.DemoteListB();
		} else if(unitSequence == "c" & troopInfo.cLevel > 1 ){
			troopInfo.cLevel --;
			TroopScale.troopScale.DemoteListC();
		} 

		ItemSeletion.itemSeletion.CountCost();
	}



}
