using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFight : MonoBehaviour {

	public float hp = 10;
	public bool dead = false;

	public PlayerMovements playerMovements;
	public PlayerInteract playerInteract;



	public Transform body;
	public Transform head;
	public Transform leftHand;
	public Transform rightHand;
	public Transform mainHand;
	public Transform offHand;


	public GameObject arrow;
	public Transform shotSpawn;

    public float fireRate;
    private float nextFire;

	Animator animator;
	public AIClass aiClass;

	// Use this for initialization
	void Start () {
		
		aiClass = GetComponent<AIClass>();
		playerMovements = GetComponent<PlayerMovements>();
		playerInteract = GetComponent<PlayerInteract>();
		animator = GetComponentInChildren<Animator>();
		hpBar = GameObject.FindWithTag("BattleUI").transform.GetChild(0);
		maxHP = hp;
		BattleUI.battleUI.playerFight = this;

	}
	
	public bool mouseOnUI;

	// Update is called once per frame
	void Update () {
		if(!dead & aiClass.useBow & !mouseOnUI){


					HoldBow();
					ShootArrow();
		}
		if(hp<=0){
			dead = true;
			playerInteract.GetOff();
			animator.SetBool("Running", false);
			down();
		}
		DontAttack();
		HPBar();
	}

	public Transform hpBar;
	float maxHP;

	void HPBar(){
		hpBar.localScale = new Vector3(hp/maxHP,0.05f,1);
		if(hp<=0)
		hpBar.localScale = new Vector3(0,0.05f,1);
	}




	public float drawTime;
	float erroRanger = 30f;
	Quaternion angle;

	Quaternion error;


	void DontAttack(){
		if(mouseOnUI){
			nextFire = Time.time + fireRate;
		}
	}


	public float minDraw;
	public float maxDraw;
	public Bow bow;

	void ShootArrow(){



		if (Input.GetButton("Fire1") & Time.time > nextFire & aiClass.bow.playerAmmo>0)  {
			drawTime += 3 * Time.deltaTime;
			erroRanger -= 30 * Time.deltaTime;

			if(!bow.crossbow){
				animator.SetTrigger("Draw");
			} else if(bow.crossbow){
				animator.SetTrigger("DrawCrossbow");
			}
		} else if(Input.GetButtonUp("Fire1")& Time.time > nextFire & aiClass.bow.playerAmmo>0){

			nextFire = Time.time + fireRate;

			if(!bow.crossbow){
				animator.SetTrigger("Loss");
				animator.ResetTrigger("Draw");
			} else if(bow.crossbow){
				animator.SetTrigger("LossCrossbow");
				animator.ResetTrigger("DrawCrossbow");
			}

			if(drawTime< 5){
				drawTime = 5f;
			} else if (drawTime!= 10 ){
				drawTime = 10;
			}

			if(erroRanger< 2){
				erroRanger = 2f;
			}

			arrow.GetComponent<Arrow>().speed = drawTime;
			nextFire = Time.time + fireRate;
			angle = shotSpawn.rotation;
			ShootingError();
			GameObject newArrow = Instantiate(arrow, shotSpawn.position, angle);
			newArrow.layer = LayerMask.NameToLayer( "FriendlyFire" );
			drawTime = 0;
			erroRanger = 30f;


			bow.UseAmmo();
		}else {
			drawTime = 0;
		} 
	}


	void ShootingError(){
		error.eulerAngles = new Vector3(0, 0, Random.Range(-erroRanger, erroRanger));
		angle.eulerAngles += error.eulerAngles; 
		//print(erroRanger);
	}

	Quaternion aimmingDirection;
	Quaternion rightHandRotation;
	Quaternion bodyRotation;
	public bool aimming = false;

	void HoldBow(){

		if(drawTime>0){
			aimming = true;

		} else {
			aimming = false;
		}

	}

	




	Quaternion downAngle;

	bool HasDownDirection = false;
	SpriteRenderer bodySprite;
	SpriteRenderer headSprite;
	SpriteRenderer leftHandSprite;
	SpriteRenderer rightHandSprite;
	SpriteRenderer mianHandSprite;
	SpriteRenderer offHandSprite;
	SpriteRenderer armorSprite;
	SpriteRenderer helmetSprite;

	void down(){
		if(!HasDownDirection){

			head.localPosition = new Vector3 (0,-6,0);
			leftHand.localPosition = new Vector3 (-5,4,0);
			leftHand.localPosition = new Vector3 (-5,4,0);

			downAngle.eulerAngles = new Vector3(0, 0, Random.Range(0, 359) + 15);
			transform.rotation = downAngle;
			HasDownDirection = true;

			gameObject.layer = LayerMask.NameToLayer( "DeadBody" );
			bodySprite = body.GetComponent<SpriteRenderer>();
			headSprite = head.GetComponent<SpriteRenderer>();
			leftHandSprite = leftHand.GetComponent<SpriteRenderer>();
			rightHandSprite = rightHand.GetComponent<SpriteRenderer>();
			mianHandSprite = mainHand.GetComponent<SpriteRenderer>();
			offHandSprite = offHand.GetComponent<SpriteRenderer>();
			armorSprite = mainHand.GetComponent<SpriteRenderer>();
			helmetSprite = offHand.GetComponent<SpriteRenderer>();

			bodySprite.sortingLayerName = "DeadBody";
			headSprite.sortingLayerName = "DeadBody";
			leftHandSprite.sortingLayerName = "DeadBody";
			rightHandSprite.sortingLayerName = "DeadBody";
			mianHandSprite.sortingLayerName = "DeadBody";
			offHandSprite.sortingLayerName = "DeadBody";
			armorSprite.sortingLayerName = "DeadBody";
			helmetSprite.sortingLayerName = "DeadBody";

			playerMovements.moveable = false;

        	gameObject.tag = "DeadBody";
		}
		
	}

}
