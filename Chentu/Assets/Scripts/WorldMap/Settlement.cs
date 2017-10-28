using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settlement : MonoBehaviour {

	//Nearby Settlements

	public string name;
	public string settlementClass;

	public string faction;
	public Text nameText;
	public Text classText;
	public Transform forces;
	public List<Transform> nearbyPlaces;

	//Facilities
	public float roadLevel;


	// Use this for initialization
	void Start () {
		SetInfo();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void Select(){
		 Collection.collection.selectedLocation = this;
		 Collection.collection.settlementActions.SetShow();
		 Collection.collection.movementActions.SetShow();
	}

	void SetInfo(){
		if(classText){
			classText.text = settlementClass  + " OF " + faction; ;
	 		SetColor();
		}
		if(nameText)
			nameText.text = name;
		SetIcon();
	} 

	void SetIcon(){
		if(settlementClass == "HAMLET"){
			GetComponent<Image>().sprite = Collection.collection.hamletSprite;
		} else if(settlementClass == "PASS"){
			GetComponent<Image>().sprite = Collection.collection.passSprite;
		} else if(settlementClass == "CITY"){
			GetComponent<Image>().sprite = Collection.collection.citySprite;
		}
	}

	void SetColor(){
		Color factionColor = new Color(255, 255, 255, 255);
		if(faction == "NEUTRAL"){
			factionColor =  Collection.collection.neutralColor;
		} else if(faction == "QU"){
			factionColor =  Collection.collection.quColor;
		} else if(faction == "PU"){
			factionColor =  Collection.collection.quColor;
		} else if(faction == "XING"){
			factionColor =  Collection.collection.quColor;
		} else if(faction == "JU"){
			factionColor =  Collection.collection.quColor;
		} else if(faction == "SUI"){
			factionColor =  Collection.collection.quColor;
		} else if(faction == "GONG"){
			factionColor =  Collection.collection.quColor;
		} else if(faction == "RONG") {
			factionColor =  Collection.collection.rongColor;
		} else if(faction == "YI") {
			factionColor =  Collection.collection.yiColor;
		}


		GetComponent<Image>().color = factionColor;
		classText.color = factionColor;
	}


}
