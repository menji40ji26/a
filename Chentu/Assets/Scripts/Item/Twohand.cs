using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twohand : Weapon {

	void Update () {
		if(!aiClass.isPlayer){
			if(!aiFight.dead & !aiFight.aiInteract.driving & aiFight.hasTarget){
				Stab();
				Targeting();
			}
		} else if(!playerFight.dead & !playerFight.playerInteract.driving){
			PlayerStab();
		}
	}
	


	void PlayerStab(){

		if(Input.GetButton("Fire1") & Time.time > nextAttack  & !playerFight.mouseOnUI){




				nextAttack = Time.time + attackSpeed;
				animator.SetTrigger("Hew"); 
				attacking = true;


				
		} else {
				animator.ResetTrigger("Hew"); 
		}

		if( Time.time > nextAttack - attackSpeed / 10 ){
			attacking = false;
		}
	}

	void Targeting(){
		target = transform.parent.parent.GetComponent<AIFight>().target;
		if(target)
		distance = Vector2.Distance(target.transform.position, body.position);
	}

	void StopRunning(){
		if(target & distance < range )
			animator.SetBool("Running",false); 
	}

	void FixedUpdate(){
		 StopRunning();
	}


    float nextAttack;


	void Stab(){

		if(   target & Time.time > nextAttack){


			if(distance < range ){


				nextAttack = Time.time + attackSpeed;
				animator.SetTrigger("Hew"); 

				attacking = true;
				DealDamage();


				
			}
		}
	}


	void DealDamage(){
		if(attacking){
			CountDamage();
			attacking = false;
		}
	}


}