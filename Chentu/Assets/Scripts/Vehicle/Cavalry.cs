using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cavalry : Vehicle {

	public Transform saddle;

	void Start () {
		if(transform.name =="BoarCavalry(Clone)"){
			category = 4;
		} else {
			category = 1;
		}
		SetSpeed();
	}

	void FixedUpdate () {

		SetControl();
		CheckBroken();

	}

}
