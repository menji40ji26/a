using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour {

	public bool driving;
	
	Rigidbody2D rb; 
	public PlayerFight playerFight;

	GameObject actionIndicator;
	GameObject actionIndicatorClone;

	Animator animator;
	

	// Use this for initialization
	void Start () {
		rb = transform.parent.GetComponent<Rigidbody2D>();
		playerMovements =  GetComponent<PlayerMovements>();
		animator = GetComponent<Animator>();
	}
	

	void Update() {
		CountGetOnCD();
		CountDropOffCD();
		Harvest();

		if(Input.GetKeyUp(KeyCode.Escape)){
			EndTalk();
		}
	}

	void ClearIndicator(){
		if(worldSpaceUIClone){
			Destroy(worldSpaceUIClone);
		}
	}

	public bool operating;
	float harverstAnimalTime = 3; 
	float beginTime;

	//Skills
	void Harvest(){
		if(!playerFight.dead & interactTarget){
			if(Input.GetKeyUp(KeyCode.E)){
				interactTarget.GetComponent<Item>().Harvest();
			}
		}
		if(operating){
			animator.SetBool("Operating",true);
			if(Time.time - beginTime > harverstAnimalTime){
				print("Harvested");
				interactTarget.GetComponent<Item>().DropItem();
				operating = false;
			}
		} else {
			beginTime = Time.time;
			animator.SetBool("Operating",false);
		}


	}

	// Update is called once per frame
	void FixedUpdate () {

		GetOff();
		if(transform.parent.parent){

			transform.parent.position = transform.parent.parent.position;
			if(chariotTransform.GetComponent<Vehicle>().category==1){
				transform.position = chariotTransform.GetComponent<Cavalry>().saddle.position;
			}
		}
	}



	void OnTriggerStay2D(Collider2D other) {

		Talk(other);

		if(other.gameObject.CompareTag("Chariot")){

			
			//ActionIndicator
			if(!worldSpaceUIClone){
				worldSpaceUIClone = Instantiate(worldSpaceUI, other.gameObject.transform.position + new Vector3(0,0.3f,0), worldSpaceUI.transform.rotation);
				worldSpaceUIClone.transform.GetChild(0).GetComponent<Text>().text = " [E] Get on";
			}
			if(Input.GetKeyDown(KeyCode.E) & !onSpot & getOnable){
				GetOn(other);
				dropOffCountDown = 0.0f;
			}

		} else if(other.GetComponent<Weapon>()){

		}

		if(other.gameObject.transform.childCount > 0)
		if(other.gameObject.transform.GetChild(0).CompareTag("PlayerVehicle")){
			BattleUI.battleUI.inCargoRange = true;

			if(worldSpaceUIClone){

				Destroy(worldSpaceUIClone);
				worldSpaceUIClone = null;
			}

			if(!worldSpaceUIClone){
				worldSpaceUIClone = Instantiate(worldSpaceUI, other.gameObject.transform.position + new Vector3(0,0.3f,0), worldSpaceUI.transform.rotation);
				worldSpaceUIClone.transform.GetChild(0).GetComponent<Text>().text = " [E] Get on  [C] Cargo";
			}


			if(Input.GetKeyDown(KeyCode.C)){
				BattleUI.battleUI.cargo.gameObject.SetActive(true);
				//BattleUI.battleUI.cargoOpened = true;
			}

		}

    }

	void OnTriggerEnter2D(Collider2D other){

		DetectItem(other);

	}

	void OnTriggerExit2D(Collider2D other){
		if(other.transform == interactTarget){
			interactTarget = null;
		}
		ClearIndicator();

		if(other.transform == interactTarget){
			interactTarget = null;
		}

		if(other.transform.childCount>0)
		if(other.transform.GetChild(0).CompareTag("PlayerVehicle")){
			BattleUI.battleUI.inCargoRange = false;
			BattleUI.battleUI.cargo.gameObject.SetActive(false);
			//BattleUI.battleUI.cargoOpened = false;
		}
	}


	public GameObject worldSpaceUI;
	GameObject worldSpaceUIClone;

	//bool hasIndicator;
	public Transform interactTarget;

	void DetectItem(Collider2D other){


		if(other.gameObject.layer == LayerMask.NameToLayer("Item")){
			if(other.gameObject.name == "DeadBoar(Clone)"
			|  other.gameObject.name == "DeadHorse(Clone)"
			){
				interactTarget = other.transform;
				if(worldSpaceUIClone){
					Destroy(worldSpaceUIClone);
				}
				//ActionIndicator
				worldSpaceUIClone = Instantiate(worldSpaceUI, other.gameObject.transform.position + new Vector3(0,0.3f,0), worldSpaceUI.transform.rotation);
				worldSpaceUIClone.transform.GetChild(0).GetComponent<Text>().text = " [E] Harverst";
			}

			else if(other.gameObject.CompareTag("Item")){
				interactTarget = other.transform;
				if(worldSpaceUIClone){
					Destroy(worldSpaceUIClone);
				}
				//ActionIndicator
				worldSpaceUIClone = Instantiate(worldSpaceUI, other.gameObject.transform.position + new Vector3(0,0.3f,0), worldSpaceUI.transform.rotation);
				worldSpaceUIClone.transform.GetChild(0).GetComponent<Text>().text = " [E] Pick up";
			}
		}
	}



	Transform spot;
	public bool onSpot;
	float dropOffCD = 0.01f;
	float dropOffCountDown;
	public bool dropOffable;

	float getOnCD = 0.01f;
	float getOnCountDown;
	bool getOnable;

	Transform aiDriver;
	
	public PlayerMovements playerMovements;
	Transform chariotTransform;
	void GetOn(Collider2D vehicle){
		

		//Driver Spot
		//GameObject spot = vehicle.gameObject.FindWith("Car/Driver");
		chariotTransform = vehicle.gameObject.GetComponent(typeof(Transform)) as Transform;
		string vehicleType = "Chariot";
		string spotType = "Driver";


		Transform aiDriver = null;
		//if (chariotTransform.name == vehicleType  &  
		if(chariotTransform.gameObject.layer != LayerMask.NameToLayer("EnemyChariot")){
			for (int n = 0; n < chariotTransform.childCount; n++){
				if (chariotTransform.GetChild(n).name == spotType){

					//Take Control From AI
					if(chariotTransform.GetChild(n).childCount!=0){
						aiDriver = chariotTransform.GetChild(n).GetChild(0);

						//If no Seat, kick him off
						//aiDriver.parent = null;

						transform.parent.SetParent(chariotTransform.GetChild(n));

					}

					//Send Him to Another Spot of the same car

					for (int i = 0; i < chariotTransform.childCount; i++){
						if(aiDriver & chariotTransform.GetChild(i).childCount==0){
							aiDriver.SetParent(chariotTransform.GetChild(n));
						} 
					}



					transform.parent.SetParent(chariotTransform.GetChild(n));
					onSpot = true;
					spot = chariotTransform.GetChild(n);
					//print("Found Spot");
				}

				//print("Searched " + chariotTransform.GetChild(i).name + n);
			}

			//print("Found Car");
			
		}

		if(onSpot){
			transform.parent.transform.position = spot.position;
			playerMovements.onVehicle = true;
			GetControl(chariotTransform);
			getOnable = false;
			dropOffCountDown = 0.0f;
			gameObject.layer = LayerMask.NameToLayer( "Passenger" );
			
		}

		//print("Get On driver");
		

	}



	void CountDropOffCD(){

		if(onSpot){

			if(!dropOffable){
				dropOffCountDown += 1 * Time.deltaTime;
			}
			if(dropOffCountDown > dropOffCD){
				dropOffable = true;

					//print("Drop Off able" );
			}

		}
		//print(dropOffCountDown);
	}


	void CountGetOnCD(){

		if(!onSpot){

			if(!getOnable){
				getOnCountDown += 1 * Time.deltaTime;
			}
			if(getOnCountDown > getOnCD){
				getOnable = true;

				//print("get On able" );
			}
		}

	}

	public Vehicle chariotMovement;

	void GetControl(Transform vehicle){
		chariotMovement = vehicle.GetComponent<Vehicle>();
		chariotMovement.playerControlling = true;
		rb.isKinematic = true;
	}


	void LossControl(){
		transform.position = chariotMovement.GetComponent<ChariotPassengers>().dropOffSpotTransform.position;
		chariotMovement.playerControlling = false;
		rb.isKinematic = false;
		onSpot = false;
		//playerMovements.onVehicle = false;
	}

	//Transform dropOffSpotTransform

	public void GetOff(){
		if(playerMovements.onVehicle){
			if(onSpot & dropOffable & Input.GetKeyDown(KeyCode.E) || chariotTransform.GetComponent<Vehicle>().noHorse ){
				//transform.position = drop
				transform.parent.parent = null;
				onSpot = false;
				//print("Dropped off");
				dropOffable = false;
				getOnCountDown = 0.0f;
				playerMovements.onVehicle = false;
				LossControl();

				gameObject.layer = LayerMask.NameToLayer( "Player" );

			} else if (playerFight.hp<=0) {
				transform.parent.parent = null;
				onSpot = false;
				dropOffable = false;
				getOnCountDown = 0.0f;
				playerMovements.onVehicle = false;
				LossControl();


				gameObject.layer = LayerMask.NameToLayer( "DeadBody" );
			} 

			transform.position = transform.parent.position;
		}
		


	}

	
	public bool taking;
	//Interact with NPC
	void Talk(Collider2D other){
		if(other.CompareTag("Civilian")){


			//ActionIndicator
			if(!worldSpaceUIClone){
				worldSpaceUIClone = Instantiate(worldSpaceUI, other.gameObject.transform.position + new Vector3(0,0.3f,0), worldSpaceUI.transform.rotation);
				worldSpaceUIClone.transform.GetChild(0).GetComponent<Text>().text = " [C] 交谈";
			}
				
			//Chat
			if(Input.GetKeyUp(KeyCode.C) & !taking){


				GameController.gameController.chatList.SetActive(true);
				print("Talk");
				//playerMovements.moveable = false;
				playerMovements.Look(other);
				playerFight.mouseOnUI = true;
				other.GetComponent<AIMovement>().Look(this.transform);
				taking = true;

				//Hide Other UI
				BattleUI.battleUI.gameObject.SetActive(false);

				//Set both side of talker= GetComponent<AI>();
				GameController.gameController.chat.SetBothSide(other.GetComponent<AI>(),GetComponent<AI>());
				
			}
		}
	}

	public void EndTalk(){
		if(taking){
				
			GameController.gameController.CloseChatWindow();
			//playerMovements.moveable = true;
			playerFight.mouseOnUI = false;
			taking = false;

			//Show Other UI
			BattleUI.battleUI.gameObject.SetActive(true);
		}
	}





}
