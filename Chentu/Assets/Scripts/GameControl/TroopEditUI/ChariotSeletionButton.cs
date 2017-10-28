using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChariotSeletionButton : MonoBehaviour {

	TroopInfo troopInfo;
	public int chariotType;
	public float cost;
	public Text costText;
	public Text previewCostText;
	GameObject chariotPrefab;
	public GameObject chariotClone;
	public Transform preview;
	GameObject chariotPreviewClone;


	// Use this for initialization
	void Start () {
		costText = GetComponentInChildren<Text>();
		troopInfo = GameObject.FindWithTag("TroopInfo").GetComponent<TroopInfo>();
	}
	
	// Update is called once per frame
	void Update () {
		if(ChariotSelection.chariotSelection.showing){
			CreatPreview();
		}
	}


	public void CreatPreview(){
		if(chariotType == 1){
			chariotPrefab = ItemCollection.itemCollection.chariot01;
			cost = 270;
		} else if(chariotType == 2){
			chariotPrefab = ItemCollection.itemCollection.chariot02;
			cost = 650;
		} else if(chariotType == 3){
			chariotPrefab = ItemCollection.itemCollection.chariot03;
			cost = 390;
		}


		costText.text = cost + " ";

		if(!chariotClone){
			if(chariotType==1){
				chariotClone = Instantiate(chariotPrefab, transform.position, transform.rotation);
				chariotClone.transform.SetParent(this.transform);
				chariotClone.transform.localScale = new Vector3(7f, 7f, 1);
				chariotClone.transform.localPosition = new Vector3(0f, -10f, 0);
			} else if(chariotType==2){
				chariotClone = Instantiate(chariotPrefab, transform.position, transform.rotation);
				chariotClone.transform.SetParent(this.transform);
				chariotClone.transform.localScale = new Vector3(6f,6f, 1);
				chariotClone.transform.localPosition = new Vector3(0f, -10f, 0);
			} else if(chariotType==3){
				chariotClone = Instantiate(chariotPrefab, transform.position, transform.rotation);
				chariotClone.transform.SetParent(this.transform);
				chariotClone.transform.localScale = new Vector3(12f,12f, 1);
				chariotClone.transform.localPosition = new Vector3(0f, 10f, 0);
			}
		}
	}

	public void ChooseThisChariot(){
		if(!chariotPreviewClone){
			chariotPreviewClone = Instantiate(chariotPrefab, preview.transform.position, preview.transform.rotation);
			chariotPreviewClone.transform.SetParent(preview.transform);
			previewCostText.text = costText.text;
			chariotPreviewClone.transform.localScale = new Vector3(15, 15, 1);
			if(chariotType==3){
				chariotPreviewClone.transform.localPosition = new Vector3(0f, 40f, 0);
				chariotPreviewClone.transform.localScale = new Vector3(27, 27, 1);
			}
			if(ChariotSelection.chariotSelection.currentProview){
				Destroy(ChariotSelection.chariotSelection.currentProview.gameObject);
			}

			ChariotSelection.chariotSelection.currentProview = chariotPreviewClone;
		}
		troopInfo.chariotType = chariotType;
		ItemSeletion.itemSeletion.chariotCost = cost;
		ItemSeletion.itemSeletion.CountCost();
	}


}
