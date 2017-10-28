using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon {

	

	
	void Update () {

		if(aiClass)
		if(!aiClass.isPlayer){
			if(!aiFight.dead & !aiFight.aiInteract.driving & aiFight.hasTarget){
				StopRunning();
				Stab();
				Targeting();
			}
		} else if(!playerFight.dead & !playerFight.playerInteract.driving){

			PlayerStab();
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



    float nextAttack;

	//public bool Attacking;

	void PlayerStab(){
		if(Input.GetButton("Fire1") & Time.time > nextAttack  & !playerFight.mouseOnUI){

				nextAttack = Time.time + attackSpeed;
				//animator.SetBool("Stabbing",true); 
				animator.SetTrigger("Stab"); 
				//animator.SetBool("Running", false);
				attacking = true;


				
		} else {

				animator.ResetTrigger("Stab"); 
		}


		if( Time.time > nextAttack - attackSpeed / 10 ){
			attacking = false;
		}
	}

	void Stab(){

		if(   target & Time.time > nextAttack){


			if(distance < range ){


				nextAttack = Time.time + attackSpeed;
				animator.SetBool("Stabbing",true); 
				animator.SetTrigger("Stab"); 

				animator.SetBool("Running", false);
				attacking = true;
				DealDamage();


				
			}
		} else {

				animator.SetBool("Stabbing",false); 
		}
	}

	//AIFight aiFight;

	/*
	void OnTriggerEnter2D(Collider2D other){
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("Stab")) {
			if(transform.parent.parent.CompareTag("Ally")){
				if(other.CompareTag("Enemy")){
					aiFight = other.GetComponent<AIFight>();
				}
			}
		}
	}
	 */




	void DealDamage(){
		if(attacking){
			CountDamage();
			attacking = false;
		}
	}


}
