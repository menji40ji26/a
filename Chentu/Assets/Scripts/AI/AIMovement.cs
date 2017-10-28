using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour {

	public AIFight aiFight;
	AIInteract aiInteract;
	AIClass aiClass;
	
	Vector3 movement;
	public bool moveable = true;
	
	public float speed;

	public float turnSpeed;
 
	public Transform target;


	// Use this for initialization
	void Start () {
		aiInteract = GetComponent<AIInteract>();
		aiClass = GetComponent<AIClass>();
		target = null;
		distance = 0.0f;
		turnSpeed = 5f;
	}

	public bool idle = false;

	// Update is called once per frame
	void Update () {

		ResetDrawTime();
		if(!aiFight.dead){
			CheckRange();
			//Drag();

			if(target & !aiInteract.onSpot & moveable & !aiFight.aimming & !idle){

				//aiFight.animator.SetBool("Running", true);
				PlayRunAnimation();
				Moving();
				LeaveTrack();
			} else {

				aiFight.animator.SetBool("Running", false);
			}
			Turning();
		}
	}

	void ResetDrawTime(){
		if(aiFight.movingToSpot  ){
			aiFight.drawTime = 0;
		}
	}

	void PlayRunAnimation(){

		if(!aiInteract.onSpot & moveable & !aiFight.aimming & !idle
		//& !aiFight.animator.GetBool("Drawing") 
		//& !aiFight.animator.GetBool("Driving")
		//& !aiFight.animator.GetBool("Stabbing")
		//& !aiFight.animator.GetBool("SpearStabbing")
		& moving
		//& distance > range
		//& !aiFight.animator.GetCurrentAnimatorStateInfo(0).IsName("Chop") 
		//& !aiFight.animator.GetCurrentAnimatorStateInfo(0).IsName("Stab") 
		//& !aiFight.animator.GetCurrentAnimatorStateInfo(0).IsName("SpearStab") 
		){
			aiFight.animator.SetBool("Running", true);
		} else {

			aiFight.animator.SetBool("Running", false);
		}

		if(aiFight.movingToSpot){

			aiFight.animator.SetBool("Running", true);
		}
	}



	public GameObject trackPrefab;
	GameObject trackClone;
	float nextLeftTrack;
	float nextRightTrack;
	float trackRate = 0.3f;

	void LeaveTrack(){
		if(moving){
			if( Time.time > nextLeftTrack){

				nextLeftTrack = Time.time + trackRate + Random.Range(0,0.3f);
				trackClone = Instantiate(trackPrefab, transform.position, transform.rotation);
			}

			if( Time.time > nextRightTrack + Random.Range(0,0.3f)){

				nextRightTrack = Time.time + trackRate + Random.Range(0,0.3f);
				trackClone = Instantiate(trackPrefab, transform.position, transform.rotation);
				trackClone.GetComponent<SpriteRenderer>().flipX = true;
			}
		}
		
	}

	public float distance;
	public float range;
	public bool inRange;
	
	Weapon weapon;

	void CheckRange(){

		if(aiClass.unarmed)
		weapon = GetComponentInChildren<Fist>();
		if(aiClass.useAxe)
		weapon = GetComponentInChildren<Axe>();
		if(aiClass.useSword)
		weapon = GetComponentInChildren<Sword>();
		if(aiClass.useBow)
		weapon = GetComponentInChildren<Bow>();
		if(aiClass.useSpear)
		weapon = GetComponentInChildren<Spear>();
		if(aiClass.useTwohand)
		weapon = GetComponentInChildren<Twohand>();

		range = weapon.range;

		if(target)
		distance = Vector2.Distance(target.transform.position, transform.position);

		if(distance>range){
			inRange = false;
		} else {
			inRange = true;
		}
	}


	public bool moving;

	void Moving(){
		
		moving = true;
		if(distance>range & aiFight.hasTarget |aiFight.movingToSpot  ){

			transform.position += transform.up * speed * Time.deltaTime;
		} else if (distance<range * 0.7f & target ) {

			transform.position -= transform.up * speed  * Time.deltaTime/2;
			
		}else{
			moving = false;
		}


 		
	}
	
	
	void Turning(){
		if(!aiFight.aimming){

			if(target != null ) {

				Vector2 direction = target.position - transform.position;
				float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90;
				Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
				transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);
			}
		}
	}

	public void Look(Transform attentionPoint){
		Vector2 direction = attentionPoint.position - transform.position;
		float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90;
		Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1 );
	}




}