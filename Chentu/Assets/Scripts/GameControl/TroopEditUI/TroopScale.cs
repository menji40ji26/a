using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TroopScale : MonoBehaviour {

	static public TroopScale troopScale;
	public float rookieNumber;
	public Text rookieNumberText;
	public float veteranNumber;
	public Text veteranNumberText;
	public float eliteNumber;
	public Text eliteNumberText;

	//public List<GameObject> rookieNumberIcons;
	public List<GameObject> unitAIcons;
	public List<GameObject> unitBIcons;
	public List<GameObject> unitCIcons;
	GameObject unitIconClone;
	public GameObject rookieIconPrefab;
	public GameObject veteranIconPrefab;
	public GameObject eliteIconPrefab;

	//public Transform rookieNumberScale;
	public Transform unitAScale;
	public Transform unitBScale;
	public Transform unitCScale;

	TroopInfo troopInfo;

	// Use this for initialization
	void Start () {
		troopInfo = GameObject.FindWithTag("TroopInfo").GetComponent<TroopInfo>();
		troopScale = this;
	}
	
	// Update is called once per frame
	void Update () {
		troopInfo.a = unitAIcons.Count;
		troopInfo.b = unitBIcons.Count;
		troopInfo.c = unitCIcons.Count;
		rookieNumberText.text = rookieNumber + " ";
		veteranNumberText.text = veteranNumber + " ";
		eliteNumberText.text = eliteNumber + " ";
	}


	int replaceNumber;
	public void PromoteListA(){
		replaceNumber = unitAIcons.Count;

		if(troopInfo.aLevel==2){
			rookieNumber += replaceNumber;
		} else if(troopInfo.aLevel==3){
			veteranNumber += replaceNumber;
		}
		for (int i = unitAIcons.Count -1; i > -1; i--){
			Destroy(unitAIcons[i].gameObject);
			unitAIcons.RemoveAt(i);
		}
		for (int i = 0; i < replaceNumber; i++){
			AddUnitA();
		}
	}

	public void DemoteListA(){
		replaceNumber = unitAIcons.Count;

		if(troopInfo.aLevel==2){
			eliteNumber += replaceNumber;
		} else if(troopInfo.aLevel==1){
			veteranNumber += replaceNumber;
		}
		for (int i = unitAIcons.Count -1; i > -1; i--){
			Destroy(unitAIcons[i].gameObject);
			unitAIcons.RemoveAt(i);
		}
		for (int i = 0; i < replaceNumber; i++){
			AddUnitA();
		}
	}

	public void PromoteListB(){
		replaceNumber = unitBIcons.Count;

		if(troopInfo.bLevel==2){
			rookieNumber += replaceNumber;
		} else if(troopInfo.bLevel==3){
			veteranNumber += replaceNumber;
		}
		for (int i = unitBIcons.Count -1; i > -1; i--){
			Destroy(unitBIcons[i].gameObject);
			unitBIcons.RemoveAt(i);
		}
		for (int i = 0; i < replaceNumber; i++){
			AddUnitB();
		}
	}

	public void DemoteListB(){
		replaceNumber = unitBIcons.Count;

		if(troopInfo.bLevel==2){
			eliteNumber += replaceNumber;
		} else if(troopInfo.bLevel==1){
			veteranNumber += replaceNumber;
		}
		for (int i = unitBIcons.Count -1; i > -1; i--){
			Destroy(unitBIcons[i].gameObject);
			unitBIcons.RemoveAt(i);
		}
		for (int i = 0; i < replaceNumber; i++){
			AddUnitB();
		}
	}
	public void PromoteListC(){
		replaceNumber = unitCIcons.Count;

		if(troopInfo.cLevel==2){
			rookieNumber += replaceNumber;
		} else if(troopInfo.cLevel==3){
			veteranNumber += replaceNumber;
		}
		for (int i = unitCIcons.Count -1; i > -1; i--){
			Destroy(unitCIcons[i].gameObject);
			unitCIcons.RemoveAt(i);
		}
		for (int i = 0; i < replaceNumber; i++){
			AddUnitC();
		}
	}

	public void DemoteListC(){
		replaceNumber = unitCIcons.Count;

		if(troopInfo.cLevel==2){
			eliteNumber += replaceNumber;
		} else if(troopInfo.cLevel==1){
			veteranNumber += replaceNumber;
		}
		for (int i = unitCIcons.Count -1; i > -1; i--){
			Destroy(unitCIcons[i].gameObject);
			unitCIcons.RemoveAt(i);
		}
		for (int i = 0; i < replaceNumber; i++){
			AddUnitC();
		}
	}

	public void AddUnitA(){

		if(rookieNumber>0 & troopInfo.aLevel ==1){
			unitIconClone = Instantiate(rookieIconPrefab, unitAScale.position, transform.rotation);
			unitIconClone.transform.SetParent(unitAScale);
			unitIconClone.transform.localScale = new Vector3(1, 1, 1);
			unitAIcons.Add(unitIconClone);


			rookieNumber --;
		} else if(veteranNumber>0 & troopInfo.aLevel ==2){
			unitIconClone = Instantiate(veteranIconPrefab, unitAScale.position, transform.rotation);
			unitIconClone.transform.SetParent(unitAScale);
			unitIconClone.transform.localScale = new Vector3(1, 1, 1);
			unitAIcons.Add(unitIconClone);


			veteranNumber --;
		} else if(eliteNumber>0 & troopInfo.aLevel ==3){
			unitIconClone = Instantiate(eliteIconPrefab, unitAScale.position, transform.rotation);
			unitIconClone.transform.SetParent(unitAScale);
			unitIconClone.transform.localScale = new Vector3(1, 1, 1);
			unitAIcons.Add(unitIconClone);


			eliteNumber --;
		}
	}	
	
	public void AddUnitB(){
		if(rookieNumber>0 & troopInfo.bLevel ==1){
			unitIconClone = Instantiate(rookieIconPrefab, unitBScale.position, transform.rotation);
			unitIconClone.transform.SetParent(unitBScale);
			unitIconClone.transform.localScale = new Vector3(1, 1, 1);
			unitBIcons.Add(unitIconClone);


			rookieNumber --;
		} else if(veteranNumber>0 & troopInfo.bLevel ==2){
			unitIconClone = Instantiate(veteranIconPrefab, unitBScale.position, transform.rotation);
			unitIconClone.transform.SetParent(unitBScale);
			unitIconClone.transform.localScale = new Vector3(1, 1, 1);
			unitBIcons.Add(unitIconClone);


			veteranNumber --;
		} else if(eliteNumber>0 & troopInfo.bLevel ==3){
			unitIconClone = Instantiate(eliteIconPrefab, unitBScale.position, transform.rotation);
			unitIconClone.transform.SetParent(unitBScale);
			unitIconClone.transform.localScale = new Vector3(1, 1, 1);
			unitBIcons.Add(unitIconClone);


			eliteNumber --;
		}
	}
	
	public void AddUnitC(){
		if(rookieNumber>0 & troopInfo.cLevel ==1){
			unitIconClone = Instantiate(rookieIconPrefab, unitCScale.position, transform.rotation);
			unitIconClone.transform.SetParent(unitCScale);
			unitIconClone.transform.localScale = new Vector3(1, 1, 1);
			unitCIcons.Add(unitIconClone);


			rookieNumber --;
		} else if(veteranNumber>0 & troopInfo.cLevel ==2){
			unitIconClone = Instantiate(veteranIconPrefab, unitCScale.position, transform.rotation);
			unitIconClone.transform.SetParent(unitCScale);
			unitIconClone.transform.localScale = new Vector3(1, 1, 1);
			unitCIcons.Add(unitIconClone);


			veteranNumber --;
		} else if(eliteNumber>0 & troopInfo.cLevel ==3){
			unitIconClone = Instantiate(eliteIconPrefab, unitCScale.position, transform.rotation);
			unitIconClone.transform.SetParent(unitCScale);
			unitIconClone.transform.localScale = new Vector3(1, 1, 1);
			unitCIcons.Add(unitIconClone);


			eliteNumber --;
		}
	}

	public void DeleteUnitA(){
		if(unitAIcons.Count>0){

			if(unitAIcons[0].name=="RookieIcon(Clone)"){
				rookieNumber ++;
			} else if(unitAIcons[0].name=="VeteranIcon(Clone)"){
				veteranNumber ++;
			} else if(unitAIcons[0].name=="EliteIcon(Clone)"){
				eliteNumber ++;
			}
			Destroy(unitAIcons[0].gameObject);
			unitAIcons.RemoveAt(0);
		}
	}

	public void DeleteUnitB(){
		if(unitBIcons.Count>0){

			if(unitBIcons[0].name=="RookieIcon(Clone)"){
				rookieNumber ++;
			} else if(unitBIcons[0].name=="VeteranIcon(Clone)"){
				veteranNumber ++;
			} else if(unitBIcons[0].name=="EliteIcon(Clone)"){
				eliteNumber ++;
			}
			Destroy(unitBIcons[0].gameObject);
			unitBIcons.RemoveAt(0);

		}
	}	
	
	public void DeleteUnitC(){
		if(unitCIcons.Count>0){
			if(unitCIcons[0].name=="RookieIcon(Clone)"){
				rookieNumber ++;
			} else if(unitCIcons[0].name=="VeteranIcon(Clone)"){
				veteranNumber ++;
			} else if(unitCIcons[0].name=="EliteIcon(Clone)"){
				eliteNumber ++;
			}
			Destroy(unitCIcons[0].gameObject);
			unitCIcons.RemoveAt(0);
		}
	}
}
