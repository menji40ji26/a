using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Running : MonoBehaviour {

	public Animator animator;

	// Use this for initialization
	void Start () {
		animator.SetBool("Running", true);
	}
	
	// Update is called once per frame
	void LateUpdate () {
		animator.SetBool("Running", true);
		print("Running");
		
	}
}
