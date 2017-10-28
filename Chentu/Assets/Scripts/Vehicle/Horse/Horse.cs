using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horse : MonoBehaviour {

	public bool isBoar;
	public Vehicle movement;
	public float hp;
	public float power;
	public float impact;
	public Animator animator;

	public Sprite deadHorse;


	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		if(transform.parent){

			movement = transform.parent.parent.GetComponent<Vehicle>();
		}
		power = 3f;
		hp = 100;
		impact = 30f;
		fadeTime = 200f;
	}
	

	void Fatigue(){
		if(movement.category!=1){

			hp -= Random.Range(0,20) * Time.deltaTime * movement.moveSpeed;

		} 

		if(movement.moveSpeed>movement.maxSpeed/2 & power > 2f){

			power -= Time.deltaTime * movement.moveSpeed / 7;
		} else if (power<3){
			power += Time.deltaTime * 0.3f;
		}

	}

	// Update is called once per frame
	void Update () {
		if(hp>0 & movement){
			Run();
			LeaveTrack();
			Fatigue();
		}
		Lame();


	}


	void Run(){
		if(movement.moveSpeed>movement.maxSpeed/Random.Range(1,4f)){
			animator.SetBool("Running", true);
			Turn();
		} else {

			Stop();
		}
	}

	void Turn(){
		if(movement.turnLeft & !movement.goStraight){
			animator.SetBool("RunningLeft", true);
			animator.SetBool("RunningRight", false);
		} else if(movement.turnRight & !movement.goStraight){
			animator.SetBool("RunningLeft", false);
			animator.SetBool("RunningRight", true);
		} else if (movement.goStraight){
			animator.SetBool("Running", true);
			animator.SetBool("RunningLeft", false);
			animator.SetBool("RunningRight", false);
		} else {
			Stop();
		}
	}

	public GameObject trackPrefab;
	public GameObject dustPrefab;
	GameObject trackClone;
	float nextLeftTrack;
	float nextRightTrack;
	float nextDust;
	float trackRate = 0.25f;

	void LeaveTrack(){
		if( Time.time > nextLeftTrack &  movement.moveSpeed > 0){

			nextLeftTrack = Time.time + trackRate;
			trackClone = Instantiate(trackPrefab, transform.position, transform.rotation);
		}

		if( Time.time > nextRightTrack + Random.Range(0,0.2f) &  movement.moveSpeed > 0){

			nextRightTrack = Time.time + trackRate;
			trackClone = Instantiate(trackPrefab, transform.position, transform.rotation);
			trackClone.GetComponent<SpriteRenderer>().flipX = true;
		}

		if( Time.time > nextDust &  movement.moveSpeed > movement.maxSpeed*  3/4){

			nextDust = Time.time + trackRate + Random.Range(0,0.5f) ;
			Instantiate(dustPrefab, transform.position, transform.rotation);
		}
	}


	void Stop(){
		animator.SetBool("Running", false);
		animator.SetBool("RunningLeft", false);
		animator.SetBool("RunningRight", false);
	}

	void OnCollisionEnter2D(Collision2D other){
		Impact(other);
	}




	void Impact(Collision2D other) {
		float impactDamge = 0;
		if(gameObject.layer == LayerMask.NameToLayer( "FriendlyChariot" ) &  other.transform.CompareTag("Enemy")  
		| gameObject.layer == LayerMask.NameToLayer( "EnemyChariot" ) &  other.transform.CompareTag("Ally") ){
			impactDamge = movement.moveSpeed * impact;
			other.collider.GetComponent<AIFight>().hp -= impactDamge;
			movement.moveSpeed *= 0.8f;
		} else if(gameObject.layer == LayerMask.NameToLayer( "EnemyChariot" ) &  other.transform.CompareTag("Player")) {
			impactDamge = movement.moveSpeed * impact ;
			other.collider.GetComponent<PlayerFight>().hp -= impactDamge;
			movement.moveSpeed *= 0.8f;
		}

	}

	void Lame(){
		if(hp<=0){
			if(isBoar){
				GetComponent<SpriteRenderer>().sprite = ItemCollection.itemCollection.deadBoarSprite;
			} else {
				GetComponent<SpriteRenderer>().sprite = deadHorse;
			}
			animator.SetBool("Dead",true);
			Stop();
			Loss();
			Vanish();
		}
	}
	
	public float fadeTime;

	void Loss(){
		transform.parent = null;
		gameObject.tag = "DeadBody";
		gameObject.layer = LayerMask.NameToLayer("DeadBody");
	}

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
