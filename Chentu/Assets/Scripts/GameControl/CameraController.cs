using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	Transform player;
	float xOffset;
	float yOffset;
	//Vector3 offset;
	Vector3 playerPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {

		CameraFollowPlayer();
		CameraToMouse();
		GodView();
		ChatView();
	}

	void CameraFollowPlayer(){

		//float viewdistance = 20f;
		if(GameObject.FindWithTag("Player")){

			player = GameObject.FindWithTag("Player").transform.parent;
		} else {
			player = null;
		}
		if(player){

			playerPosition = new Vector3(player.position.x + xOffset / 2 ,player.position.y + yOffset / 2,-10f);
			//offset = new Vector3(xOffset, yOffset, 0.0f);

			transform.position = playerPosition;
		}

	}

	public float mouseDistance;
	public float mouseAngle;

	void CameraToMouse(){

		if(player)
		if(!player.GetComponentInChildren<PlayerInteract>().taking){

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


			if (Input.GetAxis("Mouse ScrollWheel") < 0f & Camera.main.orthographicSize < 5) {
				Camera.main.orthographicSize +=  10 * Time.deltaTime;
			}
			if (Input.GetAxis("Mouse ScrollWheel") > 0f & Camera.main.orthographicSize > 2){
				Camera.main.orthographicSize -=  10 * Time.deltaTime;
			}
		}

	}


	void GodView(){
		if(!player & Camera.main.orthographicSize < 5){
			Camera.main.orthographicSize += 1 * Time.deltaTime;
		}
	}

	void ChatView(){
		if(player & Camera.main.orthographicSize > 2){
			if(player.GetComponentInChildren<PlayerInteract>().taking)
			Camera.main.orthographicSize -= 2.5f * Time.deltaTime;
		}
	}


}
