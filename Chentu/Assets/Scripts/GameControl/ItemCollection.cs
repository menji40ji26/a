using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollection : MonoBehaviour {

	static public ItemCollection itemCollection;

	public Sprite itemIcon;

	public Sprite ringHeadedSwordSprite;
	public Sprite bronzeSwordSprite;
	public Sprite axeSprite;
	public Sprite sword01;
	public Sprite sword02;
	public Sprite shield01;

	public Sprite bowLeft01;
	public Sprite bowRight01;
	public Sprite arrow01;
	public Sprite quiver01;


	public Sprite crossbow01;
	public Sprite crossbowBody01;
	public Sprite crossbowLeft01;
	public Sprite crossbowRight01;
	public Sprite crossbowArrow01;
	public Sprite crossbowQuiver01;
	
	public Sprite spear01;
	public Sprite spear02;
	public Sprite spear03;

	public Sprite twohand01;
	public Sprite twohand02;

	public Sprite axe01;
	public Sprite axe02;

	//Armor

	public Sprite armor01;
	public Sprite armor02;
	public Sprite armor03;
	public Sprite armor04;
	public Sprite armor05;
	public Sprite helmet01;
	public Sprite helmet02;
	public Sprite helmet03;
	public Sprite helmet04;
	public Sprite helmet05;

	//Mount
	public Sprite horse01;
	public Sprite boar01;
	public Sprite deadBoarSprite;

	//Item Sprite

	public Sprite meatSprite;

	//Item GameObject
	
	//Weapon
	public GameObject bowArrow;
	public GameObject crossbowArrow;
	public GameObject ballistaArrow;

	public GameObject crossbow;
	public GameObject ringHeadedSword;
	public GameObject bronzeSword;
	public GameObject axe;

	//Animal
	public GameObject deadBoar;
	public GameObject deadHorse;
	public GameObject meat;

	public GameObject itemPrefab;

	//Chariot
	public GameObject chariot01;
	public GameObject chariot02;
	public GameObject chariot03;

	// Use this for initialization
	void Awake () {
		itemCollection = this;
	}
	
}
