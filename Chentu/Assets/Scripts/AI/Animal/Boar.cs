using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar : Animal {


	// Use this for initialization

	void Start () {
		animator = GetComponent<Animator>();
		speed = 2.3f;

		hp = 20;

		
	}
	
	// Update is called once per frame
	void Update () {
		if(!dead){

			player = GameObject.FindWithTag("Player");
			allAllies = GameObject.FindGameObjectsWithTag("Ally");
			allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

			Detect();
			Run();
			if(!escaped ){
				Wonder();
				Drag();
				Turn();
				LeaveTrack();
			}
		} else {
			Vanish();
		}
		Lame();
	}

	public bool moving;
	public float range;
	public float distance;
	public bool tooClose;

	void Run(){
		distance = Vector2.Distance(targetPoint.position, transform.position);

		range = 1.5f;
		if(distance>range | escaped | tooClose){


			animator.SetBool("Running",true);
			transform.position += transform.up * speed * Time.deltaTime;
			moving = true;
		} else {
			moving = false;
		}
	}

	float dragSpeed;

	void Drag(){
		if(moving){
			dragSpeed = speed;
		} else {
			dragSpeed -= 3 * Time.deltaTime;
		}
		
		if(dragSpeed<0){
			dragSpeed = 0;
		}
		if(!moving) {
			animator.SetBool("Running",false);
			//animator.SetBool("TurningLeft",false);
			//animator.SetBool("TurningRight",false);
			transform.position += transform.up * dragSpeed * Time.deltaTime;
		}


	}




	public float turnSpeed;
	public Transform targetPoint;

	void Turn(){
		turnSpeed = 3;
		targetPoint.position = randomWonderDestination;
		if(destination != null & !tooClose ) {

			animator.SetBool("TurningRight",false);
			animator.SetBool("TurningLeft",false);

			Vector2 turnDirection = randomWonderDestination - transform.localPosition;
			float turnAngle = (Mathf.Atan2(turnDirection.y, turnDirection.x) * Mathf.Rad2Deg) ;
			Vector3 direction = targetPoint.position - transform.position;
			float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90;
			Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);
			
			if(turnAngle< 45f & turnAngle>-90f){
				animator.SetBool("TurningRight",true);
				animator.SetBool("TurningLeft",false);

			} else if (turnAngle< -90f & turnAngle > -180f | turnAngle<180 & turnAngle >135){
				animator.SetBool("TurningRight",false);
				animator.SetBool("TurningLeft",true);
			} 
		}
	


	}

	float wonderTime;
	public float nextWonder;
	Vector3 randomWonderDestination;
	

	void Wonder(){
		wonderTime = 7;
		if(Time.time>nextWonder){

			nextWonder = Time.time + wonderTime;
			randomWonderDestination = new Vector3(Random.Range(-11,11),Random.Range(-11,11),0);
		}


	}

	//Runaway when player gets close
	public float hunterDistance;
	void Detect(){

		if(hunterDistance>5){
			tooClose = false;
		}

		if(player){
			hunterDistance = Vector2.Distance(player.transform.position, transform.transform.position);

			if(hunterDistance<5){
				tooClose = true;
			}
		}

		for (int i = 0; i < allAllies.Length; i++){
			hunterDistance = Vector2.Distance(allAllies[i].transform.position, transform.transform.position);

			if(hunterDistance<5){
				tooClose = true;
			}
		}


		for (int i = 0; i < allEnemies.Length; i++){
			hunterDistance = Vector2.Distance(allEnemies[i].transform.position, transform.transform.position);

			if(hunterDistance<5){
				tooClose = true;
			}
		}


	}



	public bool escaped;

	void OnCollisionEnter2D(Collision2D other){
		if(other.collider.gameObject.CompareTag("Border")){

			escaped = true;
			gameObject.layer = LayerMask.NameToLayer( "Border" );
		}
	}







	//Effect
	public GameObject trackPrefab;
	public GameObject dustPrefab;
	GameObject trackClone;
	float nextLeftTrack;
	float nextRightTrack;
	float nextDust;
	float trackRate = 0.25f;

	void LeaveTrack(){
		if( Time.time > nextLeftTrack &  moving){

			nextLeftTrack = Time.time + trackRate;
			trackClone = Instantiate(trackPrefab, transform.position, transform.rotation);
		}

		if( Time.time > nextRightTrack + Random.Range(0,0.2f) &  moving){

			nextRightTrack = Time.time + trackRate;
			trackClone = Instantiate(trackPrefab, transform.position, transform.rotation);
			trackClone.GetComponent<SpriteRenderer>().flipX = true;
		}

		if( Time.time > nextDust &  moving){

			nextDust = Time.time + trackRate + Random.Range(0,0.5f) ;
			Instantiate(dustPrefab, transform.position, transform.rotation);
		}
	}



	void Lame(){
		if(hp<=0){
			dead = true;
			CountHunt();
			GameObject deadBoar = Instantiate(ItemCollection.itemCollection.deadBoar, transform.position, transform.rotation);
			Destroy(this.gameObject);
			//animator.SetBool("Dead",true);
			// Stop();
			// Loss();
			// Vanish();
		}
	}



	void Stop(){
		animator.SetBool("Running", false);
		//animator.SetBool("RunningLeft", false);
		//animator.SetBool("RunningRight", false);
	}
	
	public float fadeTime = 1000;

	void Loss(){
		transform.parent = null;
		gameObject.tag = "DeadBody";
		gameObject.layer = LayerMask.NameToLayer("Item");
	}



	void Vanish(){

		
		//GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, fadeTime);
		fadeTime -= 0.5f * Time.deltaTime;
		if(fadeTime<=0){
			Destroy(this.gameObject);
			//print("Destoried");
		}
		//print(fadeTime);
	}
}
