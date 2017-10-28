using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballista : Vehicle {


	// Use this for initialization
	void Start () {
		SetSpeed();
		chariotPassengers = GetComponent<ChariotPassengers>();
		animator = GetComponent<Animator>();
		isHorseless = true;
		fireRate = 8f;
		nextFire = fireRate;
		maxDamage = 200;
		maxDamage = 300;
		hp = 100;
	}
	
	// Update is called once per frame
	void Update () {

		if(aiControlling | playerControlling && !noHorse){

			SetControl();
			CheckBroken();
			animator.ResetTrigger("Loss");
			if(driver.GetComponent<AIFight>()){
				if(driver.GetComponent<AIFight>().hasTarget)
				Load();
				Aim();
			}
		}

		Break();
	}

	public float fireRate;
	public float nextFire;

	Quaternion aimmingRotation;
	Vector2 direction;
	void Aim(){

		if(driver.GetComponent<AIFight>().target){
			if(aiTarget){

				AimHorse();
				direction = aiTarget.position - transform.position;
			}

			if(aim){
				direction = aim.position - transform.position;

			}




			float turnAngle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90;
			aimmingRotation = Quaternion.AngleAxis(turnAngle, Vector3.forward);

			if(chariotPassengers.hasSpot){
				transform.rotation = Quaternion.Slerp(transform.rotation, aimmingRotation, Time.deltaTime * 0.6f );
			} else {
				transform.rotation = Quaternion.Slerp(transform.rotation, aimmingRotation, Time.deltaTime * 1f );
			}
	}
	}

	public Transform aim;

	void AimHorse(){
		if(gameObject.layer == LayerMask.NameToLayer( "EnemyChariot" ) & aiTarget.gameObject.layer == LayerMask.NameToLayer( "Passenger" )
		| gameObject.layer == LayerMask.NameToLayer( "FriendlyChariot" ) & aiTarget.gameObject.layer == LayerMask.NameToLayer( "EnemyPassenger" )){

			for (int i = 0; i < aiTarget.parent.parent.childCount; i++){	
				if(aiTarget.parent.parent.GetChild(i).CompareTag("Bar")){


					for (int n = 0; n < aiTarget.parent.parent.GetChild(i).childCount; n++){	
						if(aiTarget.parent.parent.GetChild(i).GetChild(n).CompareTag("Horse")){



							for (int x = 0; x < aiTarget.parent.parent.GetChild(i).GetChild(n).childCount; x++){	
								if(aiTarget.parent.parent.GetChild(i).GetChild(n).GetChild(x).name == "Aim"){
									aim = aiTarget.parent.parent.GetChild(i).GetChild(n).GetChild(x);
								}
							}
						}
					}

				}
			}
		} else {
			aim = null;
		}
	}

	void Load() {
		if(Time.time>nextFire){
			nextFire = Time.time + fireRate;
			Loss();
		}
	}

	public GameObject ballistaArrow;
	float minDamage;
	float maxDamage;
	public Transform shootSpawn;

	void Loss(){
		animator.SetTrigger("Loss");
		animator.SetTrigger("Load");








		GameObject newArrow = Instantiate(ballistaArrow, shootSpawn.position, shootSpawn.rotation);
		newArrow.transform.localScale = new Vector3(0.2f,0.2f,1);
		newArrow.GetComponent<Arrow>().speed = 12;
		newArrow.GetComponent<Arrow>().shooter = driver.GetComponent<AIClass>();

		//Use Ammo
		//aiClass.bow.UseAmmo();
		newArrow.GetComponent<Arrow>().basicDamage = Random.Range(minDamage,maxDamage);
		if(gameObject.layer == LayerMask.NameToLayer( "EnemyChariot" )){
			newArrow.layer = LayerMask.NameToLayer( "EnemyFire" );
		} else if (gameObject.layer == LayerMask.NameToLayer( "FriendlyChariot" )){

			newArrow.layer = LayerMask.NameToLayer( "FriendlyFire" );
		}




	}

	void Break(){
		if(hp<=0){
			animator.SetTrigger("Break");
			noHorse = true;
			chariotPassengers.hasSpot = false;
		}
	}
	





}
