using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hint : MonoBehaviour {

	static public Hint hint;
	public RectTransform rectTransform;
	Text hintText;
	float alpha;
	float blue;

	// Use this for initialization
	void Start () {
		hint = this;
		hintText = GetComponent<Text>();
		rectTransform.localScale = new Vector3(0.0f,0.0f,0);
		alpha = 4;
		blue = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if(alpha>0)
		alpha -= Time.deltaTime / 2;
		blue -= Time.deltaTime * 2;
		if(rectTransform.localScale.x>0.2f)
		rectTransform.localScale -= new Vector3(1,1,0) * Time.deltaTime / 2;
		hintText.color = new Color(1,0.5f,blue,alpha); 
	}

	public void InsufficientFund(){
		alpha = 4;
		blue = 1;
		hintText.text = "Insufficient Fund";
		rectTransform.localScale = new Vector3(0.25f,0.25f,0);
	}
}
