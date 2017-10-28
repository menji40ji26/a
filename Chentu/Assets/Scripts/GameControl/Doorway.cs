using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doorway : MonoBehaviour {

	public Transform otherSide;
	public string toScene;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnTriggerStay2D(Collider2D other){
		if(other.gameObject.CompareTag("Player")){
			if(Input.GetKeyDown(KeyCode.E)){
				other.transform.parent.transform.position = otherSide.position;
				if(toScene!="Null"){
					SceneSwitch.sceneSwitch.ToScene(toScene);
				}

			}
		}
	}


}
