using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

	public int arrowType;

	Rigidbody2D rb;
    public float speed;
	public float basicDamage;
	public bool stop = false;


	// Use this for initialization
	void Start () {
		transform.parent = null;
		fadeTime = 200;
		shieldArmor = 0.0f;
		rb = GetComponent<Rigidbody2D>();
		Loss();
		//print(speed);




	}
	
	// Update is called once per frame
	void Update () {
		Fly();
		AdjustPosition();
	}

	void AdjustPosition(){
		if(transform.parent){
			speed = 0;
			rb.mass = 0;
			transform.position = transform.parent.position;
			//transform.rotation = transform.parent.rotation;
			gameObject.layer = LayerMask.NameToLayer( "DeadBody" );

		}
	}


	void Loss(){
		rb.velocity = transform.up * speed * 2 ;
	}

	Transform hitSpot;


	void Fly(){

		if(speed <=0 ){
			stop = true;
		}

		if(rb.velocity.magnitude < 3 | speed <5) {
			speed = 0.0f;
			//rb.velocity =  transform.up * speed;

			//transform.position = transform.up * speed;

			rb.drag = 100f;
			Vanish();
		}
	}


	void OnCollisionEnter2D(Collision2D other){


			DealDamage(other);
			rb.velocity =  transform.up * speed;
			//rb.position = Vector3.zero;
			//rb.freezeRotation = true;
			//print(other.collider.gameObject.tag);
			hitSpot = other.collider.transform;
			Hit(other);
	}

	void OnTriggerEnter2D(Collider2D other){

			HitShield(other);
	}


	void Hit(Collision2D other){
		transform.SetParent(hitSpot);
		if (other.collider.CompareTag("Player")){

			transform.SetParent(hitSpot.GetChild(0));
		}

		rb.drag = 100f;

		speed = 0.0f;
		//rb.isKinematic = true;
	}

	
	float shieldArmor;
	void HitShield(Collider2D other){
		if(other.CompareTag("Shield")){
			shieldArmor = other.GetComponent<Shield>().shieldArmor;
		}
	}
	

	Vehicle chariot;
	void HitVehicle(Collision2D other){
		if(other.collider.CompareTag("FriendlyChariot") | other.collider.CompareTag("EnemyChariot")){
	
			chariot = other.collider.GetComponent<Vehicle>();
			totalDamage = basicDamage + (rb.velocity.magnitude/20) * basicDamage;
			chariot.hp -= totalDamage;
		}
	}

	public AIClass shooter;
	public AIFight aifight;
	public PlayerFight playerFight;
	public float totalDamage;

	void DealDamage(Collision2D other){

		if(!stop){
			totalDamage = basicDamage + (rb.velocity.magnitude/60) * basicDamage - shieldArmor;
			if(totalDamage < 0){
				totalDamage = 0;
			}
			HitHorse(other);
			HitWheel(other);
			HitVehicle(other);
			HitAnimal(other);
			if (other.collider.CompareTag("Enemy")){
				aifight = other.collider.GetComponent<AIFight>();
				aifight.hp -= totalDamage ;
				BleedEffect();
				SendReport(aifight);
			} else if (other.collider.CompareTag("Ally")){
				aifight = other.collider.GetComponent<AIFight>();
				aifight.hp -= totalDamage ;
				BleedEffect();
				SendReport(aifight);
			} else if (other.collider.CompareTag("Player")){
				playerFight = other.collider.GetComponent<PlayerFight>();
				playerFight.hp -= totalDamage ;
				BleedEffect();
			} 



		}
		

	}

	void SendReport(AIFight aiFight){
		if(aifight.hp<=0){
			GameController.gameController.report.GenerateMessage(shooter.name, aiFight.aiClass.name, shooter.gameObject.tag);
		}
	}

	GameObject bleedEffectClone;

	void BleedEffect(){
		bleedEffectClone = Instantiate(Effect.effect.bleedEffect, transform.position, transform.rotation);
	}

	public Horse horse;

	void HitHorse(Collision2D other){
		if(other.collider.CompareTag("Horse") ){
			horse = other.collider.GetComponent<Horse>();
			totalDamage = basicDamage + (rb.velocity.magnitude/20) * basicDamage;
			horse.hp -= totalDamage ;

			BleedEffect();
		}

	}



	public Wheel wheel;

	void HitWheel(Collision2D other){
		if(other.collider.CompareTag("Wheel") ){
			wheel = other.collider.GetComponent<Wheel>();
			totalDamage = basicDamage + (rb.velocity.magnitude/20) * basicDamage;
			wheel.hp -= totalDamage ;
		}

	}
	


	public Boar boar;
	GameObject arrowPrefab;

	void HitAnimal(Collision2D other){
		if(other.collider.CompareTag("Wildlife") ){



			if(arrowType==0){
				arrowPrefab = ItemCollection.itemCollection.bowArrow;
			} else if(arrowType==1) {
				arrowPrefab = ItemCollection.itemCollection.crossbowArrow;
			} else if(arrowType==2) {
				arrowPrefab = ItemCollection.itemCollection.ballistaArrow;
			}
			Instantiate(Effect.effect.bloodPool, transform.position, transform.rotation);

			boar = other.collider.GetComponent<Boar>();
			boar.nextWonder = 0;
			totalDamage = basicDamage + (rb.velocity.magnitude/20) * basicDamage;
			boar.hp -= totalDamage ;
			if(boar.hp<=0){
				GameObject newArrow = Instantiate(arrowPrefab, transform.position, transform.rotation);
				arrowPrefab.layer = LayerMask.NameToLayer( "DeadBody" );
				newArrow.GetComponent<Arrow>().speed = 0;
			}
		}

	}

	
	public float fadeTime;

	void Vanish(){
		
		GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, fadeTime);
		fadeTime -= 0.5f * Time.deltaTime;
		if(fadeTime<=0){
			Destroy(this.gameObject);
			//print("Destoried");
		}

		//print(fadeTime);
	}

}
