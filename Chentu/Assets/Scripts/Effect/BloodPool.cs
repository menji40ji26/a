using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodPool : MonoBehaviour {

	// Use this for initialization
	void Start () {
		alpha = 1;
	}
	
	// Update is called once per frame

	float alpha;

	void Update () {
		if(transform.localScale.x<0.02f){

			transform.localScale += new Vector3(0.01f,0.01f,0) * Time.deltaTime;
		}else if(transform.localScale.x<0.03f){

			transform.localScale += new Vector3(0.003f,0.003f,0) * Time.deltaTime;
		} else if(alpha>0.4f) {
			alpha -= 0.02f * Time.deltaTime;
			GetComponent<SpriteRenderer>().color = new Color(1,1,1,alpha);
		}
	}
}
