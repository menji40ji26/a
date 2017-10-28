using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class Weapon : Item {

	public float minDamage;
	public float maxDamage;
	public float attackSpeed;
	public float range;
	public float distance;

	public float ammo;

	public bool attacking;


	
	public AIClass aiClass;
	public AIFight aiFight;
	public Animator animator;
	public Transform target;

	public AIMovement aiMovement;
	public PlayerMovements playerMovement;


	public Transform mainHand;
	public Transform offHand;
	public Transform body;

	public float totalDamage;
	float maxValue;
	public bool isBlunt;

	public PlayerFight playerFight;

	void Start () {


		
		aiClass = transform.parent.parent.GetComponent<AIClass>();
		aiMovement = transform.parent.parent.GetComponent<AIMovement>();
		playerMovement = transform.parent.parent.GetComponent<PlayerMovements>();
		aiFight = transform.parent.parent.GetComponent<AIFight>();
		animator = transform.parent.parent.GetComponent<Animator>();
		mainHand = transform.parent.transform;
		body = mainHand.parent.transform;


		attacking = false;
		distance = 0f;


		CheckType();
	}




	public void CountDamage(){

		if(target.GetComponent<AIFight>()){
			maxValue = maxDamage - target.GetComponent<Armor>().defense;
			if(maxValue<minDamage){
				maxValue = minDamage;
			}
			totalDamage = Random.Range(minDamage,maxValue);
			target.GetComponent<AIFight>().hp -= totalDamage;

			BleedEffect(other);
			SendReport();
		}
		
		else if(target.GetComponent<PlayerFight>()){
			maxValue = maxDamage - target.GetComponent<Armor>().defense;
			if(maxValue<minDamage){
				maxValue = minDamage;
			}
			totalDamage = Random.Range(minDamage,maxValue);
			target.GetComponent<PlayerFight>().hp -= totalDamage;
		}

		
		

	}


	void SendReport(){
		if(target.GetComponent<AIFight>().hp<=0){
			GameController.gameController.report.GenerateMessage(aiClass.name,target.GetComponent<AIClass>().name, aiClass.gameObject.tag );
		}
	}


	public void CheckOffhand(){
		if(aiClass){
			if(aiClass.offHand.CompareTag("Shield") & aiClass.offHand.CompareTag("Spear")){
				maxDamage *= 0.4f;
				if(maxDamage<minDamage)
				maxDamage = minDamage;
			}
		} 
	}

	GameObject bleedEffectClone;

	void BleedEffect(Collider2D other){
		if(!aiClass.isPlayer){
			if(!isBlunt)
			bleedEffectClone = Instantiate(Effect.effect.bleedEffect, target.position, target.rotation);
		} else {
			if(!isBlunt)
			bleedEffectClone = Instantiate(Effect.effect.bleedEffect, other.transform.position, other.transform.rotation);
		}
	}

	Collider2D other;

	void OnTriggerEnter2D(Collider2D other){
		if(other.transform.CompareTag("Shield") ){
			//This case does not include player
			if(other.transform.parent.parent.tag != transform.parent.parent.tag ){
			}
		}

		PlayerAttack(other);
	}

	void PlayerAttack(Collider2D other){
		if(aiClass)
		if(aiClass.isPlayer & attacking){
			attacking = false;
			totalDamage = Random.Range(minDamage,maxValue);
			if( other.CompareTag("Enemy")){

				BleedEffect(other);
				other.GetComponent<AIFight>().hp -= totalDamage;
			} else if( other.CompareTag("Wheel")){
				other.GetComponent<Wheel>().hp -= totalDamage;
			} else if( other.CompareTag("Horse")){

				BleedEffect(other);
				other.GetComponent<Horse>().hp -= totalDamage;
			} else if( other.CompareTag("EnemyChariot")){
				other.GetComponent<Vehicle>().hp -= totalDamage;
			} else if( other.CompareTag("Shield")){

				BleedEffect(other);
				other.GetComponent<Shield>().aiFight.hp -= totalDamage;
			}
		}
	}



	//DataBase
	public void CheckType(){


		if(GameObject.FindWithTag("Player"))
		playerFight = GameObject.FindWithTag("Player").GetComponent<PlayerFight>();


		isBlunt = false;
		if(tagName=="Sword"){

			if(type == 0 | name == "RingHeadedSword"){
				minDamage = 2f;
				maxDamage = 6f;
				attackSpeed = 1.3f;
				range = 0.5f;
			} else if (type == 1 | name == "BronzeSword"){
				minDamage = 3f;
				maxDamage = 7f;
				attackSpeed = 1.5f;
				range = 0.5f;
				type = 1;
			}
		}

		else if (bow){

			ammo = 12;
			if(type == 0){
				name = "Bow";
				minDamage = 3f;
				maxDamage = 8f;
				attackSpeed = 2.2f;
				range = 10f;
				price = 27;
			}
		} 

		else if (crossbow){

			ammo = 9;
			if (type == 0){
				name = "Crossbow";
				minDamage = 7f;
				maxDamage = 12f;
				attackSpeed = 3f;
				range = 8f;
				price = 39;
			}
		}

		else if (spear){
			if (type == 0){
				name = "Spear";
				minDamage = 2f;
				maxDamage = 9f;
				attackSpeed = 2f;
				range = 1.5f;
				price = 18;
			} else if (type == 1){
				name = "ShortSpear";
				minDamage = 1.5f;
				maxDamage = 6f;
				attackSpeed = 1.6f;
				range = 0.9f;
				price = 12;
			} else if (type == 2){
				name = "WoodSpear";
				minDamage = 0.5f;
				maxDamage = 2f;
				attackSpeed = 1.4f;
				range = 0.8f;
				price = 3;
			}
		}


		else if (axe){
			if(type == 0){
				name = "Club";
				minDamage = 0.5f;
				maxDamage = 1.2f;
				attackSpeed = 1.5f;
				range = 0.4f;
				isBlunt = true;
				price = 1;
			} else if(type == 1){
				name = "Axe";
				minDamage = 4f;
				maxDamage = 6f;
				attackSpeed = 1.9f;
				range = 0.4f;
			}
		}

		else if (twohand){
			if(type == 0){
				name = "Shu";
				minDamage = 4f;
				maxDamage = 5f;
				attackSpeed = 1.8f;
				range = 0.8f;
				isBlunt = true;
				price = 1;
			} else if(type == 1){
				name = "HengDao";
				minDamage = 2f;
				maxDamage = 9f;
				attackSpeed = 1.3f;
				range = 0.6f;
				price = 1;
			}
		}

		else {
			name = "Fist";
			minDamage = 0.1f;
			maxDamage = 0.9f;
			attackSpeed = 1.0f;
			range = 0.3f;
			isBlunt = true;
			price = 0;
		}

		SetInfo();
	
	}
	
	
}
