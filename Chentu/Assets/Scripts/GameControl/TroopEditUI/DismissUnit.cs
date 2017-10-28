using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DismissUnit : MonoBehaviour, IPointerClickHandler  {


	// Use this for initialization
	void Start () {
		

	}
	public void OnPointerClick(PointerEventData eventData) {
		
		if (eventData.pointerId == -2){

			ItemSeletion.itemSeletion.CloseUnitItemSeletion();
			if(ItemSeletion.itemSeletion.currentPointingUnit == "a" & TroopInfo.troopInfo.a > 0){
				TroopInfo.troopInfo.a --;
				ItemSeletion.itemSeletion.unitANumber--;
				TroopScale.troopScale.DeleteUnitA();
			} else if(ItemSeletion.itemSeletion.currentPointingUnit == "b" & TroopInfo.troopInfo.b > 0){
				TroopInfo.troopInfo.b --;
				ItemSeletion.itemSeletion.unitBNumber--;
				TroopScale.troopScale.DeleteUnitB();
			} else if(ItemSeletion.itemSeletion.currentPointingUnit == "c" & TroopInfo.troopInfo.c > 0){
				TroopInfo.troopInfo.c --;
				ItemSeletion.itemSeletion.unitCNumber--;
				TroopScale.troopScale.DeleteUnitC();
			}

			ItemSeletion.itemSeletion.CountCost();
		}
	
	}
}
