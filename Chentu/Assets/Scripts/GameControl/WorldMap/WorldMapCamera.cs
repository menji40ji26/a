using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapCamera : MonoBehaviour {

	public Camera camera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		CameraMove();
		CameraToPlayer();
	}

	void CameraMove(){
		if(Input.GetKey(KeyCode.W) & camera.transform.position.y < 400f){
			camera.transform.position += Vector3.up * Time.deltaTime * 100;
		}if(Input.GetKey(KeyCode.S) & camera.transform.position.y > -400f){
			camera.transform.position -= Vector3.up * Time.deltaTime * 100;
		}if(Input.GetKey(KeyCode.D) & camera.transform.position.x < 300f){
			camera.transform.position -= Vector3.left * Time.deltaTime * 100;
		}if(Input.GetKey(KeyCode.A) & camera.transform.position.x > -300f){
			camera.transform.position += Vector3.left * Time.deltaTime * 100;
		}

		if (Input.GetAxis("Mouse ScrollWheel") < 0f & camera.transform.position.z > -350f) {
			camera.transform.position -= Vector3.forward * Time.deltaTime * 800 ;
		}
		if (Input.GetAxis("Mouse ScrollWheel") > 0f & camera.transform.position.z < -100f){
			camera.transform.position += Vector3.forward * Time.deltaTime * 800 ;
		}
	}

	void CameraToPlayer(){
		if(Input.GetKey("space")){
			transform.position = new Vector3(Collection.collection.player.position.x, Collection.collection.player.position.y, transform.position.z);
		}
	}
}
