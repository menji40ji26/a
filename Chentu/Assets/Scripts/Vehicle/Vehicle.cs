using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour {


	public int category;

	public float hp;
	public Transform targetPoint;
	public bool isHorseless;

	public float speed = 0;

	public float moveSpeed;
	public float maxSpeed;
	public float acceleration;
	public float drag;

	public float turnSpeed;
	public float turnAngle;

	public bool playerControlling;
	public bool aiControlling;




	public Transform driver;
	public Transform aiTarget;

	public bool turnLeft;
	public bool turnRight;
	public bool goStraight;
	public bool noHorse;

	public Vector3 movement;
	public Animator animator;
	public ChariotPassengers chariotPassengers;




	public  void SetSpeed(){
		//Cavalry
		if(category==1){
			maxSpeed = 0.15f;
			acceleration = 0.1f;
			drag = 0.1f;
			turnSpeed = 8;
		//Chariot
		} else if(category==0){
			maxSpeed = 0.08f;
			acceleration = 0.04f;
			drag = 0.03f;
			turnSpeed = 6;
		//FourHorseChariot
		} else if(category==2){
			maxSpeed = 0.10f;
			acceleration = 0.05f;
			drag = 0.02f;
			turnSpeed = 5;
		}
		//Ballista
		else if(category==3){
			maxSpeed = 0.1f;
			acceleration = 0.01f;
			drag = 0.1f;
			turnSpeed = 8;
		}
		//BoarCavalry
		else if(category==4){
			maxSpeed = 0.13f;
			acceleration = 0.15f;
			drag = 0.1f;
			turnSpeed = 12;
		}
	}

	public void SetControl(){
		if(playerControlling){
			//CameraSizeControl();
		} else if (aiControlling){
			Targeting();
			AIDirection();
		}
	}

	public void Controlling(){
		if(!isHorseless){

			CheckHorse();
			Turning();
		}
		Moving();
	}


	void Targeting(){
		if(aiTarget){
			targetPoint.position = aiTarget.position;
		}
	}

	void AIDirection(){
			if(aiTarget != null ) {

				//Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

				Vector2 direction = targetPoint.localPosition - transform.localPosition;
				//Vector2 direction = aiTarget.position - transform.position;
				float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) ;
				//Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
				//transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);
				turnRight = false;
				turnLeft = false;
				goStraight = false;
				if(angle< 85f & angle>-90f){
				//if(angle< 45f & angle>-90f){
					turnLeft = false;
					turnRight = true;

				} else if (angle< -90f & angle > -180f | angle<180 & angle >105){
				//} else if (angle< -90f & angle > -180f | angle<180 & angle >135){
					turnLeft = true;
					turnRight = false;
				} else{
					goStraight = true;
				}

			}
	}
	
	void CheckHorse(){

		
		noHorse = true;
		speed = 0f;
		for (int i = 0; i < transform.childCount ; i++){
			if(transform.GetChild(i).name == "Bar"){
				for (int n = 0; n < transform.GetChild(i).childCount; n++){
					if(transform.GetChild(i).GetChild(n).CompareTag("Horse")){
						//horses.Add(transform.GetChild(i).GetChild(n).GetComponent<Horse>());
						noHorse = false;
						speed += transform.GetChild(i).GetChild(n).GetComponent<Horse>().power;
						if(speed>6)
						speed = 6;
					}
				}
			}
		}



	}


	void Moving(){

		bool moving = false;
		float dragValue = drag;

		if(GetComponent<ChariotPassengers>().driver.childCount != 0)
		driver = GetComponent<ChariotPassengers>().driver.GetChild(0);
		
		if(playerControlling){

			//if(Input.GetKey(KeyCode.W) & Input.GetKey(KeyCode.LeftShift) & moveSpeed < maxChargeSpeed){
				//moveSpeed += chargeSpeed * Time.deltaTime;
			//} else 
			if(Input.GetKey(KeyCode.W) & moveSpeed < maxSpeed){
				moveSpeed += acceleration * Time.deltaTime;
				moving = true;
			} else if (Input.GetKey(KeyCode.S) & moveSpeed > -maxSpeed/20) {
				moveSpeed -= acceleration * Time.deltaTime;
				moving = true;
			} else {
				moving = false;
			}

		} else if(aiControlling & driver){

			if(driver.GetComponent<AIFight>().hasTarget){
				if( moveSpeed < maxSpeed){
					moveSpeed += acceleration * Time.deltaTime;
					moving = true;
				} else {
					moving = false;
				}
			}
			
		} else {
			moving = false;
		}


		if(moving){
			dragValue = 0;
		} else {
			dragValue = drag;
		}

		if (moveSpeed < 0 & moveSpeed > - dragValue * Time.deltaTime){
			moveSpeed = 0;
		} else if (moveSpeed > 0 & moveSpeed < dragValue * Time.deltaTime){
			moveSpeed = 0;
		} else if(moveSpeed>0){
			moveSpeed -= dragValue * Time.deltaTime;
		} else if(moveSpeed<0) {
			moveSpeed += dragValue * Time.deltaTime;
		} else {
			moveSpeed = 0;
		}


		
		transform.position += transform.up * moveSpeed * speed/10;


	}

	

	public void Turning(){


		if(playerControlling){
			goStraight = false;
			if(Input.GetKey(KeyCode.A)){

				turnAngle += turnSpeed * Time.deltaTime * 10;
				transform.eulerAngles = new Vector3(0, 0, turnAngle);
				turnLeft = true;
				turnRight = false;
			} else if(Input.GetKey(KeyCode.D)){

				turnAngle -= turnSpeed * Time.deltaTime * 10;
				transform.eulerAngles = new Vector3(0, 0, turnAngle);
				turnLeft = false;
				turnRight = true;
			} else {
				goStraight = true;
			}
		}
		 else if(aiControlling & driver) {

			 
			if( aiTarget){
				Vector2 direction = aiTarget.position - transform.position;
				float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90;
				Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
				if(!goStraight)
				transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turnSpeed/10 * Time.deltaTime);

			}
		}
		
		
	}

	public void CheckBroken(){

		if(!noHorse){
 			Controlling();
		} else {
			Broken();
		}
	}	

	public void Broken(){
		gameObject.layer = LayerMask.NameToLayer( "DeadBody" );
		gameObject.tag = "DeadBody";
	}
	
	void CameraSizeControl(){
		if(moveSpeed>0.01 & Camera.main.orthographicSize < 6 & playerControlling){
			Camera.main.orthographicSize += 1 * Time.deltaTime;
		} else if (moveSpeed < 0.01 & Camera.main.orthographicSize > 5 & playerControlling){
			Camera.main.orthographicSize -= 1 * Time.deltaTime;
		} else if(Camera.main.orthographicSize > 5 & !playerControlling){
			Camera.main.orthographicSize -= 1 * Time.deltaTime;
		}
	}
}
