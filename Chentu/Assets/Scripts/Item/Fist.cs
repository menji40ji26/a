using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : Weapon {
	// Use this for initialization
	void Start () {


		aiFight = transform.parent.parent.GetComponent<AIFight>();
		aiClass = transform.parent.parent.GetComponent<AIClass>();
		animator = transform.parent.parent.GetComponent<Animator>();
		
		mainHand = transform.parent.transform;
		body = mainHand.parent.transform;

		
		nextAttack = attackSpeed;
		attacking = animator.GetBool("Stabbing"); 
		attacking = false;
		distance = 0f;
		CheckType();
		CheckOffhand();


	}

	


	
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
	
	void PlayerStab(){

		if(Input.GetButton("Fire1") & Time.time > nextAttack & !playerFight.mouseOnUI){




				nextAttack = Time.time + attackSpeed;
				//animator.SetBool("Stabbing",true); 
				animator.SetTrigger("Stab"); 
				//animator.SetBool("Running", false);
				attacking = true;


				
		} else {

				animator.ResetTrigger("Stab"); 
		}

		if(attacking){
			//animator.SetBool("Running", false);
			//playerMovement.moveable = false;
			
		} else {
			//playerMovement.moveable = true;
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


	void DealDamage(){
		if(attacking){
			CountDamage();
			attacking = false;
		}
	}


}