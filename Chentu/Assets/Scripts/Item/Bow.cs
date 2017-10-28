using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon {

	public float playerAmmo;
	bool setAmmo;

	void LateUpdate () {

		if(!aiClass.isPlayer){
			if(!aiFight.dead & !aiFight.aiInteract.driving & aiFight.hasTarget){
				aiFight.fireRate = attackSpeed;
			}
		} else {
			playerFight.fireRate = attackSpeed;
			playerFight.bow = this;
			if(!setAmmo){
				playerAmmo = ammo;
				setAmmo = true;
			}
		}
	}



	public void UseAmmo(){
		if(ammo>=1){
			ammo --;
		} else if(aiFight) {
			aiFight.aiClass.useBow = false;
			aiFight.aiClass.offHand.tag = "Untagged";
			aiFight.aiClass.CheckClass();

		}
		
		if (playerFight & playerAmmo > 0) {
			playerAmmo --;
			// playerFight.aiClass.useBow = false;
			// playerFight.aiClass.offHand.tag = "Untagged";
		}
	}


		




}