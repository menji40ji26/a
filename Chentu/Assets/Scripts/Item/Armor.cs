using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Item {

	public float defense;
	public float bodyArmor;
	public float helmetArmor;
	public float shieldArmor;
	public float weight;
	public int armorType;
	public int helmetType;
	public int shieldType;

	

	// Use this for initialization
	void Start () {
		defense = 0;
		weight = 0;
		CheckType();
	}
	

	public void CheckType(){
		SetArmor();
		SetHelmet();
		SetShield();
		defense = bodyArmor + helmetArmor + shieldArmor;
	}

	void SetArmor(){
		if(armorType==0){
			price = 0;
		} else if(armorType==1){
			bodyArmor = 2;
			weight = 1;
			price = 8;
		}else if (armorType==2){
			bodyArmor += 6;
			weight = 6;
			price = 32;
		}else if (armorType==3){
			bodyArmor += 4;
			weight = 3;
			price = 28;
		}else if (armorType==4){
			bodyArmor += 0;
			weight = 1;
			price = 2;
		}else if (armorType==5){
			bodyArmor += 2;
			weight = 3;
			price = 23;
		}
	}

	void SetHelmet(){
		if(helmetType==0){
		} else if(helmetType==1){
			price = 0;

		}else if (helmetType==2){
			helmetArmor = 3;
			weight = 2;
			price = 21;
		}else if (helmetType==3){
			price = 0;
		}else if (helmetType==4){
			weight = 1;
			price = 0;
		}else if (helmetType==5){
			weight = 1;
			price = 18;
		}
	}

	void SetShield(){
		if(shieldType==1){
			shieldArmor = 3;
			weight = 2;
			price = 9;
		}
	}


	// Update is called once per frame
	void Update () {
		
	}
}
