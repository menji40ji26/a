using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIClass : MonoBehaviour {

	public bool isPlayer;
	public string leader;
	public int level;
	public string name;
	public GameObject unitCard;

	public bool unarmed;
	public bool useShield;
	public bool useBow;
	public bool useSword;
	public bool useSpear;
	public bool useAxe;
	public bool useTwohand;


	public float offHandItem;
	public float mainHandItem;

	Animator animator;
	AIFight thisMan;
	public PlayerFight thisPlayer;
	AIMovement aiMovement;
	PlayerMovements playerMovement;

	public GameObject offHand;
	public GameObject mainHand;
	public GameObject helmet;
	public GameObject bodyArmor;
	public GameObject back;
	public GameObject back2;
	public GameObject bowLeft;
	public GameObject bowRight;

	// public Sprite offHandSprite;
	//public Sprite mainHandSprite;
	// public Sprite backSprite;
	// public Sprite back2Sprite;

	//public Sprite bowLeftSprite;
	//public Sprite bowRightSprite;
	

	public Fist fist;
	public Sword sword;
	public Bow bow;
	public Spear spear;
	public Axe axe;
	public Twohand twohand;
	public Weapon weapon;

	public Armor armor;
	public Shield shield;


	void Start () {



		GetSprite();
		//SetItemList();
		armor = GetComponent<Armor>();
		animator = GetComponent<Animator>();
		thisMan = GetComponent<AIFight>();
		thisPlayer = GetComponent<PlayerFight>();
		aiMovement = GetComponent<AIMovement>();
		playerMovement = GetComponent<PlayerMovements>();
		offHand.GetComponent<SpriteRenderer>().sprite = null;
		mainHand.GetComponent<SpriteRenderer>().sprite = null;
		bodyArmor.GetComponent<SpriteRenderer>().sprite = null;
		back.GetComponent<SpriteRenderer>().sprite = null;
		back2.GetComponent<SpriteRenderer>().sprite = null;
		bowLeft.GetComponent<SpriteRenderer>().sprite = null;
		bowRight.GetComponent<SpriteRenderer>().sprite = null;
		SetLevel();


		CheckClass();

	}


	void SetLevel(){



		if(!isPlayer){
			if(level == 1){
				thisMan.hp = 10;
				aiMovement.speed = 1f;
			} else if(level == 2){
				thisMan.hp = 20;
				aiMovement.speed = 1f;
			} else if(level == 3){
				thisMan.hp = 30;
				aiMovement.speed = 1f;
			}
			//Wounded
			else if(level == 0){
				thisMan.hp = 1;
				aiMovement.speed = 1f;
			}
		} else {
			if(level == 1){
				thisPlayer.hp = 10;
				playerMovement.speed = 1f;
			} else if(level == 2){
				thisPlayer.hp = 20;
				playerMovement.speed = 1f;
			} else if(level == 3){
				thisPlayer.hp = 30;
				playerMovement.speed = 1f;
			}
			//Wounded
			else if(level == 0){
				thisPlayer.hp = 1;
				playerMovement.speed = 1f;
			}
		}
	}

	


	public Collider2D weaponRange;
	public Collider2D shieldRange;

	public void CheckClass(){


		if(isPlayer){
			BattleUI.battleUI.playerClass = this;
			Inventory.inventory.aiClass = this;
		}


		offHand.GetComponent<SpriteRenderer>().sprite = null;
		mainHand.GetComponent<SpriteRenderer>().sprite = null;
		back.GetComponent<SpriteRenderer>().sprite = null;


		bowLeft.GetComponent<SpriteRenderer>().sprite = null;
		bowRight.GetComponent<SpriteRenderer>().sprite = null;

		fist.enabled = false;
		axe.enabled = false;
		sword.enabled = false;
		bow.enabled = false;
		spear.enabled = false;
		twohand.enabled = false;
		shield.enabled = false;
		weaponRange.enabled = false;
		shieldRange.enabled = false;


		unarmed	= false;
		useShield = false;
		useBow = false;
		useSword = false; 
		useAxe = false; 

		//Mianhand

		if(offHand.tag == "Bow" | offHand.tag == "Crossbow"){

			weapon = bow;
			bow.enabled = true;
			useBow = true;
			bow.bow = true;


			offHand.GetComponent<SpriteRenderer>().sprite = null;
			//mainHand.GetComponent<SpriteRenderer>().sprite = arrow01;
			back.GetComponent<SpriteRenderer>().sprite = ItemCollection.itemCollection.quiver01;


			bowLeft.GetComponent<SpriteRenderer>().sprite = ItemCollection.itemCollection.bowLeft01;
			bowRight.GetComponent<SpriteRenderer>().sprite = ItemCollection.itemCollection.bowRight01;


			
		
			if(offHand.tag == "Crossbow" ){
				bow.bow = false;
				bow.crossbow = true;

				offHand.GetComponent<SpriteRenderer>().sprite = ItemCollection.itemCollection.crossbowBody01;
				back.GetComponent<SpriteRenderer>().sprite = ItemCollection.itemCollection.crossbowQuiver01;


				bowLeft.GetComponent<SpriteRenderer>().sprite = ItemCollection.itemCollection.crossbowLeft01;
				bowRight.GetComponent<SpriteRenderer>().sprite = ItemCollection.itemCollection.crossbowRight01;



				bow.CheckType();

			}
			
		} else if(mainHand.tag == "Axe" ){


			weapon = axe;

			axe.enabled = true;
			axe.axe = true;
			weaponRange.enabled = true;
			useAxe = true;
			weaponRange.offset = new Vector2(0,5);
			mainHand.GetComponent<BoxCollider2D>().size = new Vector2(2,4);
			SetAxe();

		} else	if(mainHand.tag == "Sword" ){


			weapon = sword;

			sword.enabled = true;
			//sword.sword = true;
			weaponRange.enabled = true;
			useSword = true;
			weaponRange.offset = new Vector2(0,6);
			mainHand.GetComponent<BoxCollider2D>().size = new Vector2(2,6);
			SetSword();

		} else  if(mainHand.tag == "Spear" ){


			weapon = spear;

			spear.enabled = true;
			spear.spear = true;
			weaponRange.enabled = true;
			useSpear = true;
			weaponRange.offset = new Vector2(0,32);
			mainHand.GetComponent<BoxCollider2D>().size = new Vector2(2,6);


			SetSpear();


		
		} else  if(mainHand.tag == "Twohand" ){


			weapon = twohand;

			twohand.enabled = true;
			twohand.twohand = true;
			weaponRange.enabled = true;
			useTwohand = true;
			weaponRange.offset = new Vector2(0,32);
			mainHand.GetComponent<BoxCollider2D>().size = new Vector2(2,6);


			SetTwohand();


		
		} else if(mainHand.tag == "Untagged" & offHand.tag !="Bow" & offHand.tag !="Crossbow" ){


			weapon = fist;

			fist.enabled = true;
			//fist.fist = true;
			unarmed = true;
		}

		//Offhand
		if(offHand.tag == "Shield"){

			shield.enabled = true;
			shieldRange.enabled = true;
			useShield = true;

			offHand.GetComponent<SpriteRenderer>().sprite = ItemCollection.itemCollection.shield01;
		}
		


		//Armor
		SetArmor();
		SetHelmet();
		SetShield();


	}

	void SetSpear(){
		if(mainHandItem == 0)
			mainHand.GetComponent<SpriteRenderer>().sprite = ItemCollection.itemCollection.spear01;
		else if(mainHandItem == 1)
			mainHand.GetComponent<SpriteRenderer>().sprite = ItemCollection.itemCollection.spear02;
		else if(mainHandItem == 2)
			mainHand.GetComponent<SpriteRenderer>().sprite = ItemCollection.itemCollection.spear03;
		spear.type = mainHandItem;
	}

	void SetTwohand(){
		if(mainHandItem == 0)
			mainHand.GetComponent<SpriteRenderer>().sprite = ItemCollection.itemCollection.twohand01;
		else if(mainHandItem == 1)
			mainHand.GetComponent<SpriteRenderer>().sprite = ItemCollection.itemCollection.twohand02;
		
		twohand.type = mainHandItem;
	}

	void SetSword(){
		if(mainHandItem == 0)
			mainHand.GetComponent<SpriteRenderer>().sprite = ItemCollection.itemCollection.sword01;
		else if(mainHandItem == 1)
			mainHand.GetComponent<SpriteRenderer>().sprite = ItemCollection.itemCollection.sword02;
		sword.type = mainHandItem;
	}
	
	void SetAxe(){
		if(mainHandItem == 0)
			mainHand.GetComponent<SpriteRenderer>().sprite = ItemCollection.itemCollection.axe01;
		else if(mainHandItem == 1)
			mainHand.GetComponent<SpriteRenderer>().sprite = ItemCollection.itemCollection.axe02;
		axe.type = mainHandItem;
	}

	//Armor

	public Transform armorTransform;
	public Transform helmetTransform;
	public Transform shieldTransform;

	public Sprite armor01;
	public Sprite armor02;

	public Sprite helmet01;
	public Sprite helmet02;




	void GetSprite(){
		armor01 = ItemCollection.itemCollection.armor01;
		armor02 = ItemCollection.itemCollection.armor02;
		helmet01 = ItemCollection.itemCollection.helmet01;
		helmet02 = ItemCollection.itemCollection.helmet02;
	}

	void SetArmor(){

		if(armor.armorType==0){
			armorTransform.GetComponent<SpriteRenderer>().sprite = null;
		} else if(armor.armorType==1){
			armorTransform.GetComponent<SpriteRenderer>().sprite = ItemCollection.itemCollection.armor01;
		} else if (armor.armorType==2){
			armorTransform.GetComponent<SpriteRenderer>().sprite = ItemCollection.itemCollection.armor02;
		} else if (armor.armorType==3){
			armorTransform.GetComponent<SpriteRenderer>().sprite = ItemCollection.itemCollection.armor03;
		} else if (armor.armorType==4){
			armorTransform.GetComponent<SpriteRenderer>().sprite = ItemCollection.itemCollection.armor04;
		} else if (armor.armorType==5){
			armorTransform.GetComponent<SpriteRenderer>().sprite = ItemCollection.itemCollection.armor05;
		}
	}

	void SetHelmet(){
		//Sprites in AIClass 
		if(armor.helmetType==0){
			helmetTransform.GetComponent<SpriteRenderer>().sprite = null;
		} else if(armor.helmetType==1){
			helmetTransform.GetComponent<SpriteRenderer>().sprite = ItemCollection.itemCollection.helmet01;
		} else if (armor.helmetType==2){
			helmetTransform.GetComponent<SpriteRenderer>().sprite = ItemCollection.itemCollection.helmet02;
		} else if (armor.helmetType==3){
			helmetTransform.GetComponent<SpriteRenderer>().sprite = ItemCollection.itemCollection.helmet03;
		} else if (armor.helmetType==4){
			helmetTransform.GetComponent<SpriteRenderer>().sprite = ItemCollection.itemCollection.helmet04;
		}else if (armor.helmetType==5){
			helmetTransform.GetComponent<SpriteRenderer>().sprite = ItemCollection.itemCollection.helmet05;
		}
	}

	void SetShield(){
		if(shield.transform.CompareTag("Shield")){
		}
	}

}
