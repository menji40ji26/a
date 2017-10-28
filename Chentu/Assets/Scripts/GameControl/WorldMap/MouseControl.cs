using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MouseControl : MonoBehaviour {

 	public Image cursor;
 	public Sprite normal;
 	public Sprite attack;

	// Use this for initialization
	void Start () {
        Cursor.visible = false;
		cursor.sprite = normal;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		FollowMouse();
		SetNormal();
	}

	void ResetMouse(){
		SetNormal();
	}

	void FollowMouse(){
		cursor.transform.position = Input.mousePosition;
	}

	public void SetNormal(){
		cursor.sprite = normal;
	}

	//Go to Target's location
	public void SetAttack(){
		cursor.sprite = attack;
		FollowTarget();
	}

	public Force targetForce;

	void FollowTarget(){
		if(targetForce)
		Collection.collection.player.GetComponent<Force>().finalGoal = targetForce.localSettlement.name;
		//Collection.collection.sceneControl.LoadBattlePrepareScene();
	}
}
