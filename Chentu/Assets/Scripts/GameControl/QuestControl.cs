using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestControl : MonoBehaviour {

	//public string questName;
	public GameObject questPrefab;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GenerateQuest(string job, AI ai){
		if(Random.Range(0,10) <= 10){
			if(job == "Farmer"){
				GetQuest(ai, "BOAR IN THE FARM");
			}
		}
	}


	void GetQuest(AI ai, string questToAdd){
		GameObject questClone = Instantiate(questPrefab, ai.transform.position, ai.transform.rotation);
		questClone.transform.SetParent(ai.transform);
		questClone.GetComponent<Quest>().questName = questToAdd;
		ai.quest = questClone.GetComponent<Quest>();
	}


}
