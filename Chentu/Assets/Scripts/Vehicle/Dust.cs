using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dust : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		Vanish();
	}

	float fadeTime = 3f;

	void Vanish(){
		
		fadeTime -= 0.5f * Time.deltaTime;
		if(fadeTime<=0){
			Destroy(this.gameObject);
		}
	}
}
