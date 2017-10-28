using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SettlementActions : MonoBehaviour {

	public bool showing;
	public Text nameText;
	public Text classText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(showing){
			Show();
		} else {
			Hide();
		}
	}

	public void SetShow(){
		showing = false;
		if(Collection.collection.player.GetComponent<Force>().localSettlement.name ==  Collection.collection.selectedLocation.name){
			showing = true;
			print("Show");
			Collection.collection.movementActions.showing = false;
			nameText.text = Collection.collection.selectedLocation.name;
			classText.text = Collection.collection.selectedLocation.settlementClass + " OF " + Collection.collection.selectedLocation.GetComponent<Settlement>().faction;
		}
	}

	void Show(){
		if(transform.position.y<300){
			transform.position += Vector3.up * 100;
		}
		// gameObject.SetActive(true);
	}

	void Hide(){
		if(transform.position.y>0){
			transform.position -= Vector3.up * 100;
		}

		// gameObject.SetActive(false);
	}
}
