using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour {

	static public SceneSwitch sceneSwitch;

	// Use this for initialization
	void Awake () {
		sceneSwitch = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void LoadBattleScene(){
        SceneManager.LoadScene("Main");
	}


	public void ToScene(string sceneNmae){
        SceneManager.LoadScene(sceneNmae);
		

	}

	public void PlayerInfo(){

	}


}
