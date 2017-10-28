using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	static public GameController gameController;

	//Region
	public Region region;

	//Battle
	public Report report;

	//Chat
	public GameObject chatUI;
	public GameObject chatList;
	public GameObject chatWindow;
	public Chat chat;

	//Quest
	public GameObject questUI;
	public QuestControl questControl;

	//Inventory
	public Inventory inventory;

	//Player
	public GameObject player;

	void Awake (){
		gameController = this; 
		CloseChatWindow();
		GetData();
	}


	// Use this for initialization
	void Start () {
		FindPlayer();
	}

	void FindPlayer(){
		player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShowChatWindow(){

		chatList.SetActive(false);
		chatWindow.SetActive(true);
	}

	public void CloseChatWindow(){

		chatList.SetActive(false);
		chatWindow.SetActive(false);
	}


	void OnDestroy(){
		SaveDate();
	}

	public void ResetGame(){
		PlayerPrefs.SetString("item1", "Null");
		print("Reset");
	}

	void SaveDate(){
		if(inventory.item1){
			PlayerPrefs.SetString("item1", inventory.item1.GetComponent<Item>().name);
			print("Saved Item1");
		} else {
			PlayerPrefs.SetString("item1", "Null");
		}
	}

	void GetData(){
		//PlayerPrefs.GetString("Item1", "Null");
		GameObject playerClone = Instantiate(player, transform.position, transform.rotation);
	}
}
