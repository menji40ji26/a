using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour {


	//Attributes
	public string name;
	public float pay;
	public int level;
	public string offHandTag;
	public float offHandItem;
	public string mainHandTag;
	public float mainHandItem;
	public int armorType;
	public int helmetType;

	public bool mounted;
	public string mountType; 

	//Team Attributes
	public float firstAid;

	//GameObject
	public Text nameText;
	public Text payText;


	// Use this for initialization
	void Start () {

		nameText.text = name + "";
		payText.text = pay + "";
	}

	public void AddUnit(){

		GameObject unitClone = Instantiate(Collection.collection.unitPrefab, transform.position, transform.rotation);
		unitClone.transform.SetParent(Collection.collection.player.GetComponent<Force>().units);
		unitClone.transform.localPosition = Vector3.zero;
		SetInfo(unitClone);
		GetTeamAttribute(unitClone);
	}

	void GetTeamAttribute(GameObject unitClone){
		unitClone.GetComponent<Unit>().firstAid = Collection.collection.player.GetComponent<Force>().firstAid;
	}

	void SetInfo(GameObject unitClone){
		unitClone.GetComponent<Unit>().name = name;
		unitClone.GetComponent<Unit>().pay = pay;
		unitClone.GetComponent<Unit>().level = level;
		unitClone.GetComponent<Unit>().offHandTag = offHandTag;
		unitClone.GetComponent<Unit>().offHandItem = offHandItem;
		unitClone.GetComponent<Unit>().mainHandTag = mainHandTag;
		unitClone.GetComponent<Unit>().mainHandItem = mainHandItem;
		unitClone.GetComponent<Unit>().armorType = armorType;
		unitClone.GetComponent<Unit>().helmetType = helmetType;
		unitClone.GetComponent<Unit>().mounted = mounted;
		unitClone.GetComponent<Unit>().mountType = mountType; 
	}

	
	// Update is called once per frame
	void Update () {
		
	}
}
