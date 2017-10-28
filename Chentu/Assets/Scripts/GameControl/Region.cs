using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Region : MonoBehaviour {

	public string regionName;
	public string lordName;
	public int fertility;

	// Use this for initialization
	void Start () {
		GameController.gameController.region = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
