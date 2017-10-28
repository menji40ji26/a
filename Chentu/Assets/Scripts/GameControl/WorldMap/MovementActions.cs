using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementActions : MonoBehaviour {

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
		if(Collection.collection.player.GetComponent<Force>().localSettlement.name !=  Collection.collection.selectedLocation.name){
			showing = true;
			Collection.collection.settlementActions.showing = false;
			nameText.text = Collection.collection.selectedLocation.name;
			classText.text = Collection.collection.selectedLocation.settlementClass + " OF " + Collection.collection.selectedLocation.GetComponent<Settlement>().faction;
			SetColor();
		}
	}


	void SetColor(){
		Color factionColor = new Color(75, 75, 75, 255);
		// if(Collection.collection.selectedLocation.GetComponent<Settlement>().faction == "NEUTRAL"){
		// 	factionColor =  Collection.collection.neutralColor;
		// } else if(Collection.collection.selectedLocation.GetComponent<Settlement>().faction == "QU"){
		// 	factionColor =  Collection.collection.quColor;
		// } else if(Collection.collection.selectedLocation.GetComponent<Settlement>().faction == "JU") {
		// 	factionColor =  Collection.collection.juColor;
		// } else if(Collection.collection.selectedLocation.GetComponent<Settlement>().faction == "PU") {
		// 	factionColor =  Collection.collection.puColor;
		// } else if(Collection.collection.selectedLocation.GetComponent<Settlement>().faction == "XING") {
		// 	factionColor =  Collection.collection.xingColor;
		// }


		//classText.color = factionColor;
	}

	void Show(){
		if(transform.position.y<150){
			transform.position += Vector3.up * 10;
		}
	}

	void Hide(){
		if(transform.position.y>0){
			transform.position -= Vector3.up * 10;
		}
	}

	public void Move(){
		for (int i = 0; i <  Collection.collection.locations.Length; i++){
			if(Collection.collection.selectedLocation.name == Collection.collection.locations[i].GetComponent<Settlement>().name ){

				// for (int n = 0; n < Collection.collection.player.GetComponent<Force>().localSettlement.nearbyPlaces.Count; n++){
				// 	if(Collection.collection.player.GetComponent<Force>().localSettlement.nearbyPlaces[n].GetComponent<Settlement>().name == Collection.collection.selectedLocation.name){

						Collection.collection.player.GetComponent<Force>().finalGoal = Collection.collection.locations[i].GetComponent<Settlement>().name;
						Collection.collection.player.GetComponent<Force>().timeOnRoad = 0;
						showing = false;
				// 	}
				// }

			}
		}
	}
}