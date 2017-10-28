using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collection : MonoBehaviour {

	public static Collection collection;
	public Transform player;

	//GameObject
	public FactionControl factioncontrol;
	public SettlementActions settlementActions;
	public MovementActions movementActions;
	public TimeControl timeControl;
	public MouseControl mouseControl;
	public SceneControl sceneControl;

	//Prefab
	public GameObject unitPrefab;

	//Faction
	public GameObject[] factions;

	//Force
	public List<Force> forces;

	//Location
	public GameObject[] locations;
	public Settlement selectedLocation;

	//Color
	public Color neutralColor;
	public Color quColor;
	public Color rongColor;
	public Color yiColor;

	//Sprite	
	public Sprite citySprite;
	public Sprite passSprite;
	public Sprite hamletSprite;

	// Use this for initialization
	void Awake () {
		collection = this;
		SetFactionColor();
		SetFactions();
		SetLocations();
	}

	void SetFactions(){
		factions = GameObject.FindGameObjectsWithTag("Faction");
	}

	void SetLocations(){
		locations = GameObject.FindGameObjectsWithTag("Settlement");
	}

	void SetFactionColor(){
		// neutralColor = new Color32(75, 75, 75, 255);
		// quColor = new Color32(255, 159, 27, 255);
		// juColor = new Color32(39, 159, 44, 255);
		// puColor = new Color32(13, 180, 145, 255);
		// xingColor = new Color32(35, 162, 204, 255);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}



