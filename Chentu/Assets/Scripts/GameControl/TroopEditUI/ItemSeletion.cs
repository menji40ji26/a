using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSeletion : MonoBehaviour {

	static public ItemSeletion itemSeletion;
	public float gold;
	public float currentGold;
	public float totalCost;
	public Text goldText;
	public string currentEditingUnit;
	public float currentEditingUnitNumber;
	TroopInfo troopInfo;

	public GameObject unitItemSeletion;

	public Transform manPreviewA;
	public Text unitACost;
	public Text unitADamage;
	public Text unitADefense;
	public Text unitAHealth;

	public Transform manPreviewB;
	public Text unitBCost;
	public Text unitBDamage;
	public Text unitBDefense;
	public Text unitBHealth;

	public Transform manPreviewC;
	public Text unitCCost;
	public Text unitCDamage;
	public Text unitCDefense;
	public Text unitCHealth;



	// Use this for initialization
	void Start () {
		gold = 1000;
		currentGold = gold;
		goldText.text = gold + " ";
		itemSeletion = this;
		troopInfo = GameObject.FindWithTag("TroopInfo").GetComponent<TroopInfo>();
		currentEditingUnit = null;
		CloseUnitItemSeletion();
		manPreviewA = transform.GetChild(0).GetChild(1);
		manPreviewB = transform.GetChild(1).GetChild(1);
		manPreviewC = transform.GetChild(2).GetChild(1);
		unitACost = transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>();
		unitBCost = transform.GetChild(1).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>();
		unitCCost = transform.GetChild(2).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>();
	}
	

	// Update is called once per frame
	void Update () {
		unitANumber = troopInfo.a;
		unitBNumber = troopInfo.b;
		unitCNumber = troopInfo.c;
		CountCurrentEditingUnitNumber();
	}

	public void CloseUnitItemSeletion(){
		unitItemSeletion.SetActive(false);
	}


	public string currentPointingUnit;
	public void PointingA(){
		currentPointingUnit = "a";
	}
	public void PointingB(){
		currentPointingUnit = "b";
	}
	public void PointingC(){
		currentPointingUnit = "c";
	}

	//CharotCost
	public float chariotCost;

	//Unit Number
	public float unitANumber;
	public float unitBNumber;
	public float unitCNumber;

	//A
	//Count Cost

	public float unitATotalCost;
	public float unitAMainHandCost;
	public float unitAOffHandCost;
	public float unitAHelmetCost;
	public float unitAArmorCost;
	public float unitABackCost;
	public float unitAMountCost;

	//Count Damage
	public float unitAMinDamage;
	public float unitAMaxDamage;

	public float unitAHeadDefense;
	public float unitABodyDefense;
	public float unitAShieldDefense;

	//B
	//Count Cost
	public float unitBTotalCost;
	public float unitBMainHandCost;
	public float unitBOffHandCost;
	public float unitBHelmetCost;
	public float unitBArmorCost;
	public float unitBBackCost;
	public float unitBMountCost;

	//Count Damage
	public float unitBMinDamage;
	public float unitBMaxDamage;

	public float unitBHeadDefense;
	public float unitBBodyDefense;
	public float unitBShieldDefense;

	//C
	//Count Cost
	public float unitCTotalCost;
	public float unitCMainHandCost;
	public float unitCOffHandCost;
	public float unitCHelmetCost;
	public float unitCArmorCost;
	public float unitCBackCost;
	public float unitCMountCost;

	//Count Damage
	public float unitCMinDamage;
	public float unitCMaxDamage;

	public float unitCHeadDefense;
	public float unitCBodyDefense;
	public float unitCShieldDefense;

	void CountCurrentEditingUnitNumber(){
		if(currentEditingUnit=="a"){
			currentEditingUnitNumber = unitANumber;
		} else if(currentEditingUnit=="b"){
			currentEditingUnitNumber = unitBNumber;
		} else if(currentEditingUnit=="c"){
			currentEditingUnitNumber = unitCNumber;
		} 
	}


	public void CountCost(){
		unitATotalCost = unitAMainHandCost + unitAOffHandCost + unitAHelmetCost + unitAArmorCost + unitABackCost + unitAMountCost;
		unitBTotalCost = unitBMainHandCost + unitBOffHandCost + unitBHelmetCost + unitBArmorCost + unitBBackCost + unitBMountCost;
		unitCTotalCost = unitCMainHandCost + unitCOffHandCost + unitCHelmetCost + unitCArmorCost + unitCBackCost + unitCMountCost;
		totalCost = unitANumber * unitATotalCost + unitBNumber * unitBTotalCost + unitCNumber * unitCTotalCost + chariotCost;

		unitACost.text = unitATotalCost + " ";
		unitBCost.text = unitBTotalCost + " ";
		unitCCost.text = unitCTotalCost + " ";

		currentGold = gold - totalCost;
		goldText.text = currentGold + " ";
	}


	public void CountAttribute(){
		unitADamage.text = (int)(unitAMinDamage+unitAMaxDamage)/2 + " ";
		unitADefense.text = (int)(unitAHeadDefense + unitABodyDefense + unitAShieldDefense) + " ";

		unitBDamage.text = (int)(unitBMinDamage+unitBMaxDamage)/2 + " ";
		unitBDefense.text = (int)(unitBHeadDefense + unitBBodyDefense + unitBShieldDefense) + " ";

		unitCDamage.text = (int)(unitCMinDamage+unitCMaxDamage)/2 + " ";
		unitCDefense.text = (int)(unitCHeadDefense + unitCBodyDefense + unitCShieldDefense) + " ";
	}

	public void CountHealth(){
		if(troopInfo.aLevel==0){
			unitAHealth.text = 10 + " ";
		} else if(troopInfo.aLevel==1){
			unitAHealth.text = 10 + " ";
		} else if(troopInfo.aLevel==2){
			unitAHealth.text = 20 + " ";
		} else if(troopInfo.aLevel==3){
			unitAHealth.text = 30 + " ";
		} 

		if(troopInfo.bLevel==0){
			unitBHealth.text = 10 + " ";
		} else if(troopInfo.bLevel==1){
			unitBHealth.text = 10 + " ";
		} else if(troopInfo.bLevel==2){
			unitBHealth.text = 20 + " ";
		} else if(troopInfo.bLevel==3){
			unitBHealth.text = 30 + " ";
		} 

		if(troopInfo.cLevel==0){
			unitCHealth.text = 10 + " ";
		} else if(troopInfo.cLevel==1){
			unitCHealth.text = 10 + " ";
		} else if(troopInfo.cLevel==2){
			unitCHealth.text = 20 + " ";
		} else if(troopInfo.cLevel==3){
			unitCHealth.text = 30 + " ";
		} 
	}

}
