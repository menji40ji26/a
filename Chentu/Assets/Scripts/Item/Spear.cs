using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : Weapon {




	
	void Update () {
		if(!aiClass.isPlayer){
			if(!aiFight.dead & !aiFight.aiInteract.driving & aiFight.hasTarget){
				FindHorseWithTag();
				Targeting();
				Stab();
			}
			StopAttackingDeadBody();
		} else if(!playerFight.dead & !playerFight.playerInteract.driving){
			PlayerStab();
		}
	}



	void PlayerStab(){

		if(Input.GetButton("Fire1") & Time.time > nextAttack & !playerFight.mouseOnUI ){

				nextAttack = Time.time + attackSpeed;
				animator.SetTrigger("SpearStab"); 
				attacking = true;


				
		} else {
				animator.ResetTrigger("SpearStab"); 
		}

		if( Time.time > nextAttack - attackSpeed / 10 ){
			attacking = false;
		}
	}

	
    public GameObject[] allHorses;
	public Transform nearestHorse;
	public float nearestHorseDistance;
	int nearestHorseNumber;
	float horseDistance;
	float lastDistance;
	public Horse horse;
	public bool horseIsCloser;

	void FindHorseWithTag(){
		allHorses =  GameObject.FindGameObjectsWithTag("Horse");
		


		for (int i = 0; i < allHorses.Length; i++){
			if(transform.parent.parent.CompareTag("Ally") & allHorses[i].gameObject.layer == LayerMask.NameToLayer("EnemyChariot")
			|  transform.parent.parent.CompareTag("Enemy") & allHorses[i].gameObject.layer == LayerMask.NameToLayer("FriendlyChariot")
			){
				horseDistance = Vector2.Distance(allHorses[i].transform.position, transform.position);


				if(i-1>=0){
					lastDistance = Vector2.Distance(allHorses[i-1].transform.position, transform.position);

					if(allHorses.Length-1 > nearestHorseNumber ){

						if(horseDistance<lastDistance & horseDistance<Vector2.Distance(allHorses[nearestHorseNumber].transform.position, transform.position)){
							nearestHorse = allHorses[i].transform;
							nearestHorseNumber = i;
						}
					} else {
						nearestHorseNumber -= 1;
					}


				}


			}
		}
			



		if(allHorses.Length-1 >= nearestHorseNumber){
			nearestHorse = allHorses[nearestHorseNumber].transform;
		}
		
		if(allHorses.Length == 0)  {
			nearestHorse = null;
		}



		if(nearestHorse){


			nearestHorseDistance = Vector2.Distance(nearestHorse.transform.position, transform.position);
			if(nearestHorseDistance<aiFight.nearestTargetDistance){

				horseIsCloser = true;
				if(transform.parent.parent.CompareTag("Ally") & nearestHorse.gameObject.layer == LayerMask.NameToLayer("EnemyChariot")){
					target = nearestHorse.GetComponent<Transform>();
					horse = target.GetComponent<Horse>();
					aiMovement.target = target;
					aiFight.target = target;
					aiFight.hasTarget = true;
				} else if(transform.parent.parent.CompareTag("Enemy") & nearestHorse.gameObject.layer == LayerMask.NameToLayer("FriendlyChariot")){
					target = nearestHorse.GetComponent<Transform>();
					horse = target.GetComponent<Horse>();
					aiMovement.target = target;
					aiFight.target = target;
					aiFight.hasTarget = true;
				}
			} else {
				horseIsCloser = false;
				target = null;
				
				horse = null;

			}



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


				if(target.CompareTag("Horse")){
					if(target.GetComponent<Horse>().hp>0 & transform.parent.parent.CompareTag("Ally") & target.gameObject.layer == LayerMask.NameToLayer("EnemyChariot")
					|  target.GetComponent<Horse>().hp>0 & transform.parent.parent.CompareTag("Enemy") & target.gameObject.layer == LayerMask.NameToLayer("FriendlyChariot")){
						animator.SetTrigger("SpearStab"); 
						attacking = true;
						DealDamage();
					}
				} else if(target.CompareTag("Enemy") | target.CompareTag("Ally")){
					if(!target.GetComponent<AIFight>().dead){
						animator.SetTrigger("SpearStab"); 
						attacking = true;
						DealDamage();
					}
				} else if ( target.CompareTag("Player")){
						if(!target.GetComponent<PlayerFight>().dead){
						animator.SetTrigger("SpearStab"); 
						attacking = true;
						DealDamage();
					}
				}

				nextAttack = Time.time + attackSpeed;
				animator.SetBool("Running", false);
	

				
			} else {

				animator.SetBool("SpearStabbing",false); 
			}


		} else {

				animator.SetBool("SpearStabbing",false); 
		}
	}



	void StopAttackingDeadBody(){
		if(target){
			if(target.CompareTag("Horse")){

				if(target.GetComponent<Horse>().hp<=0){
					target = null;
					aiMovement.target = null;
					aiFight.target = null;
					//print(transform.parent.parent.name + " Killed " + target.parent.parent.name + "'s " + target.name + " Distance " + distance + " Range " + range );
				}
			}
		}
		
	}

	void DealDamage(){
		if(attacking){
			CountDamage();
			//TooClose
			if(distance<range*0.6){
				totalDamage = minDamage;
			}



			if(target.CompareTag("Horse")){

				totalDamage = maxDamage * 3f;
			}

			if(target){

				if (target.GetComponent<Horse>()){
					target.GetComponent<Horse>().hp -= totalDamage;
				}
			}

			attacking = false;

		}
	}


}