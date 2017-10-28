using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataControl : MonoBehaviour {

	public int testingString;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//TroopInfo
	//Chariot
	public int chariotType;

	//Unit Class
	public string unitA; 
	public string unitB; 
	public string unitC; 

	//Unit Number
	public int a;
	public int b;
	public int c;

	//Equitment Tag
	public string tagAo;
	public string tagAm;
	public string tagBo;
	public string tagBm;
	public string tagCo;
	public string tagCm;

	//Equitment
	public int offHandA;
	public int offHandB;
	public int offHandC;

	public int mainHandA;
	public int mainHandB;
	public int mainHandC;

	public int armorA;
	public int armorB;
	public int armorC;

	public int helmetA;
	public int helmetB;
	public int helmetC;


	//Condition
	public int aLevel;
	public int bLevel;
	public int cLevel;

	public bool aMounted;
	public bool bMounted;
	public bool cMounted;

	public string aMountType;
	public string bMountType;
	public string cMountType;


	void TroopInfo(){

		chariotType = 0;

		a = 0;
		b = 0;
		c = 0;

		aMounted = false;
		bMounted = false;
		cMounted = false;

		aMountType = "Null";
		bMountType = "Null";
		cMountType = "Null";

		mainHandA = 0;
		offHandA = 0;
		armorA = 0;
		helmetA = 1;

		mainHandB = 0;
		offHandB = 0;
		armorB = 0;
		helmetB = 1;

		mainHandC = 0;
		offHandC = 0;
		armorC = 0;
		helmetC = 1;

		aLevel = 1;
		bLevel = 1;
		cLevel = 1;

		tagAo = "Untagged";
		tagAm = "Untagged";
		tagBo = "Untagged";
		tagBm = "Untagged";
		tagCo = "Untagged";
		tagCm = "Untagged";
		
	}
	//TroopInfo End

	public void Save(){
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

		PlayerData data = new PlayerData();
		data.testingString = testingString;
		
		bf.Serialize(file, data);
		file.Close();
	}

	public void Load(){
		if(File.Exists(Application.persistentDataPath + "/playerInfo.dat")){
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize(file);
			file.Close();

			testingString = data.testingString;
		}
	}

	public void AddInt(){
		testingString ++;
	}
}

[Serializable]
class PlayerData{
	public int testingString;
}