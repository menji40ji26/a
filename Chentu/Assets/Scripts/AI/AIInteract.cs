using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIInteract : MonoBehaviour {

	public Rigidbody2D rb; 
	public AIMovement aiMovement;
	public AIFight aiFight;

	// Use this for initialization
	void Start () {
	}
	

	void Update() {
		if(!aiFight.dead){
			FindSpot();
			CulculateSpotDistance();
			MoveToSpot();
			Drive();

			if(chariotMovement){
				SwitchToDriver();
				if(chariotMovement.noHorse | dropOffable){
					GetOff();
					chariotMovement = null;
				}
			}
		}
	}



	void OnCollisionStay2D(Collision2D vehicle) {
			
			if( !onSpot){
				CheckVehicleFaction(vehicle);
			}

    }



	public Transform spot;
	public bool onSpot;
	public bool dropOffable;

	bool isDriver = false;


	int rbOriginalLayer;
	int originalLayer;
	
    public GameObject[] allSpots;
    public GameObject[] factionSpots;
	public Transform nearestSpot;
	int nearestSpotNumber;
	int nearestFactiontSpotNumber;
	float distance;
	float lastDistance;
	float factionSpotDistance;
	float lastFactionSpotDistance;
	public float nearestSpotDistance;

	int nearestTargetNumber;

	void FindSpot(){

		allSpots =  GameObject.FindGameObjectsWithTag("Chariot");
		if(transform.CompareTag("Ally")){
			factionSpots = GameObject.FindGameObjectsWithTag("FriendlyChariot");
		} else if(transform.CompareTag("Enemy")){
			factionSpots = GameObject.FindGameObjectsWithTag("EnemyChariot");
		} 
	}

	void CulculateSpotDistance(){

		nearestSpot = null;
		nearestSpotNumber = 0;
		nearestFactiontSpotNumber = 0;
		distance = 0;
		lastDistance = 0;





		for (int i = 0; i < allSpots.Length; i++){

			if(allSpots[i].GetComponent<ChariotPassengers>().hasSpot){ 
				distance = Vector2.Distance(allSpots[i].transform.position, transform.position);
				if(i-1>=0){
					lastDistance = Vector2.Distance(allSpots[i-1].transform.position, transform.position);
					if(allSpots.Length-1 > nearestSpotNumber ){
						if(distance<lastDistance & distance<Vector2.Distance(allSpots[nearestSpotNumber].transform.position, transform.position)){
							nearestSpot = allSpots[i].transform;
							nearestSpotNumber = i;
						}
					} else {
						nearestSpotNumber -= 1;
					}

				}
			}
		}

		for (int i = 0; i < factionSpots.Length; i++){

			if(factionSpots[i].GetComponent<ChariotPassengers>().hasSpot){ 
				distance = Vector2.Distance(factionSpots[i].transform.position, transform.position);
				if(i-1>=0){
					lastDistance = Vector2.Distance(factionSpots[i-1].transform.position, transform.position);
					if(factionSpots.Length-1 > nearestFactiontSpotNumber ){
						if(distance<lastDistance & distance<Vector2.Distance(factionSpots[nearestFactiontSpotNumber].transform.position, transform.position)){
							nearestSpot = factionSpots[i].transform;
							nearestFactiontSpotNumber = i;
						}
					} else {
						nearestFactiontSpotNumber -= 1;
					}

				}
			}
		}

		if(allSpots.Length> nearestSpotNumber & factionSpots.Length >nearestFactiontSpotNumber){
			if(Vector2.Distance(allSpots[nearestSpotNumber].transform.position, transform.position) 
			<  Vector2.Distance(factionSpots[nearestFactiontSpotNumber].transform.position, transform.position)) {
				nearestSpot = allSpots[nearestSpotNumber].transform;
			} else {
				nearestSpot = factionSpots[nearestFactiontSpotNumber].transform;
			} 
		} else if (allSpots.Length == 1){
				nearestSpot = allSpots[0].transform;
		} else if (factionSpots.Length == 1){
				nearestSpot = factionSpots[0].transform;
		}


		if(nearestSpot){

			nearestSpotDistance =  Vector2.Distance(nearestSpot.position, transform.position);
			if(nearestSpotDistance>4 | !nearestSpot.GetComponent<ChariotPassengers>().hasSpot){
				nearestSpot = null;
			}
		}
	}


	void MoveToSpot(){
		if(nearestSpot & !onSpot){

			aiMovement.target = nearestSpot.transform;
			aiFight.movingToSpot = true;
		} else {


			aiFight.movingToSpot = false;
		}

	}



	void CheckVehicleFaction(Collision2D vehicle){


		//wait til slow down

			if(aiFight.gameObject.CompareTag("Ally") 
			& vehicle.gameObject.layer == LayerMask.NameToLayer( "FriendlyChariot" )){
				GetOn(vehicle);
			} else if(aiFight.gameObject.CompareTag("Enemy") 
			& vehicle.gameObject.layer == LayerMask.NameToLayer( "EnemyChariot" )){
				GetOn(vehicle);
			} else if(transform.CompareTag("Ally") 
			& vehicle.gameObject.layer == LayerMask.NameToLayer( "Chariot" )){
				GetOn(vehicle);
			}
			else if (transform.CompareTag("Enemy") 
			& vehicle.gameObject.layer == LayerMask.NameToLayer( "Chariot" )) {
				GetOn(vehicle);
			}






	}


	void GetOn(Collision2D vehicle){
		

		//Driver Spot
		Transform chariotTransform = vehicle.gameObject.GetComponent(typeof(Transform)) as Transform;
		string vehicleType = "Chariot";
		string spotType1 = "Driver";
		string spotType2 = "Archer";
		string spotType3 = "Passenger1";

		if (aiFight.CompareTag("Ally") &  chariotTransform.CompareTag("Chariot") 
		||transform.CompareTag("Ally") &  chariotTransform.CompareTag("FriendlyChariot")){

			if(chariotTransform.GetComponent<Vehicle>().category!=1 | chariotTransform.GetComponent<Vehicle>().moveSpeed<chariotTransform.GetComponent<Vehicle>().maxSpeed/5)
			for (int n = 0; n < chariotTransform.childCount; n++){
				if (chariotTransform.GetChild(n).name == spotType2 | chariotTransform.GetChild(n).name == spotType1  | chariotTransform.GetChild(n).name == spotType3
				&& chariotTransform.GetChild(n).childCount == 0){
					rb.GetComponent<Transform>().SetParent(chariotTransform.GetChild(n));
					onSpot = true;
					spot = chariotTransform.GetChild(n);
				}
			}

		} else if (aiFight.CompareTag("Enemy") &  chariotTransform.CompareTag("Chariot") 
		||transform.CompareTag("Enemy") & chariotTransform.CompareTag("EnemyChariot")){

			if(chariotTransform.GetComponent<Vehicle>().category!=1 | chariotTransform.GetComponent<Vehicle>().moveSpeed<chariotTransform.GetComponent<Vehicle>().maxSpeed/5)
			for (int n = 0; n < chariotTransform.childCount; n++){
				if (chariotTransform.GetChild(n).name == spotType2 | chariotTransform.GetChild(n).name == spotType1  | chariotTransform.GetChild(n).name == spotType3
				&& chariotTransform.GetChild(n).childCount == 0){
					rb.GetComponent<Transform>().SetParent(chariotTransform.GetChild(n));
					onSpot = true;
					spot = chariotTransform.GetChild(n);
				}
			}
		}
		if(onSpot){

			GetControl(chariotTransform);
			rb.GetComponent<Transform>().position = spot.position;
			rbOriginalLayer = rb.gameObject.layer;
			originalLayer = gameObject.layer;
			rb.isKinematic = true;
			gameObject.layer = LayerMask.NameToLayer( "NoInteract" );
			if(rb.transform.CompareTag("Enemy")){

				rb.gameObject.layer = LayerMask.NameToLayer( "EnemyPassenger" );
			}else {

				rb.gameObject.layer = LayerMask.NameToLayer( "Passenger" );
			}
		}
	}


	public Vehicle chariotMovement;

	void GetControl(Transform vehicle){

		chariotMovement = vehicle.GetComponent<Vehicle>();
		chariotPassengers = vehicle.GetComponent<ChariotPassengers>();

		if(spot.name=="Driver"){
			chariotMovement.aiControlling = true;
			isDriver = true;
		}
	}

	public bool driving = false;

	void Drive(){
		if(chariotMovement){
			chariotMovement.aiTarget = aiFight.target;
			if(aiFight.transform.parent){

				if(aiFight.transform.parent.name == "Driver"){
					driving = true;
					
					GetComponent<Animator>().SetBool("Driving", true);
					rb.GetComponent<Transform>().position = spot.position;
					CheckRide();
				}
			}
		}
	}

	void CheckRide(){
		if(transform.parent.parent.name == "Cavalry(Clone)" | transform.parent.parent.name == "BoarCavalry(Clone)" ){
			driving = false;
			GetComponent<Animator>().SetBool("Driving", false);
		} else {
			transform.rotation = chariotMovement.GetComponent<Transform>().rotation;
		}
	}

	public ChariotPassengers chariotPassengers;

	void SwitchToDriver(){
		if(chariotPassengers.driver.childCount==0){
			spot = chariotPassengers.driver;
			transform.SetParent(chariotPassengers.driver);
			isDriver = true;
			chariotMovement.aiControlling = true;
		}
		
	}

	void LossControl(){
		onSpot = false;
	}



	public void GetOff(){
		if(onSpot){
			aiFight.transform.parent = null;
			spot = null;
			onSpot = false;
			dropOffable = false;
			driving = false;

			GetComponent<Animator>().SetBool("Driving", false);
			LossControl();

			gameObject.layer = originalLayer;
			rb.gameObject.layer = rbOriginalLayer;
			chariotMovement = null;
		}
	}


}
