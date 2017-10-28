using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BleedEffect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		fadeTime = 15;
	}
	
	// Update is called once per frame
	void Update () {
		 Vanish();
	}


	
	public float fadeTime;

	void Vanish(){
		
		fadeTime -= 0.5f * Time.deltaTime;
		if(fadeTime<=0){
			Destroy(this.gameObject);
		}

	}
}
