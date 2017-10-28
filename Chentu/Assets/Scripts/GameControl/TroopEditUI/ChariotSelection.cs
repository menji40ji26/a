using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChariotSelection : MonoBehaviour {

	static public ChariotSelection chariotSelection;
	public GameObject chariotList;
	public GameObject currentProview;

	// Use this for initialization
	void Start () {
		chariotSelection = this;
		chariotList.SetActive(false);
		showing = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool showing;
	public void ShowList(){
		if(!showing){
			chariotList.SetActive(true);
			showing = true;
		} else {
			chariotList.SetActive(false);
			showing = false;
		}
	}
}
