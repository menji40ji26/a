using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour {

	static public Effect effect;

	public GameObject bleedEffect;
	public GameObject bloodPool;
	public GameObject actionIndicator;

	// Use this for initialization
	void Start () {
		effect = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
