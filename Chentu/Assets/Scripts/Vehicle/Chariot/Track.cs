using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vanish();
	}

	float fadeTime = 5f;

	void Vanish(){
		
		GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, fadeTime/15);
		fadeTime -= 0.5f * Time.deltaTime;
		if(fadeTime<=0){
			Destroy(this.gameObject);
		}
	}
}
