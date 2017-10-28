using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChariotMovement : Vehicle {



	public GameObject bar;


	// Use this for initialization
	void Start () {
		SetSpeed();
	}

	
	// Update is called once per frame
	void FixedUpdate () {

		SetControl();
		BarTurn();
		CheckBroken();

	}






	void BarTurn(){

		if(!noHorse){
			if(playerControlling){
				if(Input.GetKey(KeyCode.A)){

					if(bar.transform.localRotation.z <= 0.10){
						bar.transform.Rotate(0,0,50 * Time.deltaTime);
					}

				} else if(Input.GetKey(KeyCode.D)){

					if(bar.transform.localRotation.z >= -0.10){
						bar.transform.Rotate(0,0,-50 * Time.deltaTime);
					}
					
				} else {
					
					AdjustBar();

				}

			} else if (aiControlling) {
				
				if(turnLeft & !goStraight){

					if(bar.transform.localRotation.z <= 0.10){
						bar.transform.Rotate(0,0,50 * Time.deltaTime);
					}

				} else if(turnRight  & !goStraight){

					if(bar.transform.localRotation.z >= -0.10){
						bar.transform.Rotate(0,0,-50 * Time.deltaTime);
					}
				} else {

				AdjustBar();

				}
			} else {
				
				AdjustBar();
			}
		}
		
	}
	

	void AdjustBar(){
		if(bar.transform.localRotation.z > 0){
			bar.transform.Rotate(0,0,-20 * Time.deltaTime);
		} else if(bar.transform.localRotation.z < 0){
			bar.transform.Rotate(0,0,20 * Time.deltaTime);
		}
	}

}
