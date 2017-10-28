using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Report : MonoBehaviour {

	public Transform reportPanel;
	public GameObject reportPrefab;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}


	public void GenerateMessage(string killer, string target, string faction){
		GameObject messageClone = Instantiate(reportPrefab, reportPanel.position, reportPanel.rotation);
		SetMessage(messageClone,killer,target);
		messageClone.transform.SetParent(reportPanel);
		SetColor(messageClone, faction);
		CheckMessages();
	}

	public Color red;
	public Color yellow;

	void SetColor(GameObject messageClone, string faction){
		if(faction == "Ally"){
			messageClone.transform.GetChild(1).GetComponent<Text>().color = red;
		} else {
			messageClone.transform.GetChild(0).GetComponent<Text>().color = red;
		}
	}

	void SetMessage(GameObject messageClone, string killer, string target){
		messageClone.transform.GetChild(0).GetComponent<Text>().text = killer;
		messageClone.transform.GetChild(1).GetComponent<Text>().text = target;
	}

	void CheckMessages(){
		if(reportPanel.childCount>=5){
			for (int i = 0; i < reportPanel.childCount-4; i++){
				Destroy(reportPanel.GetChild(i).gameObject);
			}
		}
	}
}
