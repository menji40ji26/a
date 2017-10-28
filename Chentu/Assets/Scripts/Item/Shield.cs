using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

	Collider2D shieldRange;
	public float shieldArmor;
	Transform user;
	public AIFight aiFight;

	// Use this for initialization
	void Start () {
		aiFight = transform.parent.parent.GetComponent<AIFight>();
		shieldArmor = 5;

		shieldRange = GetComponent<Collider2D>();
		user = transform.parent.parent;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	float storedMaxDamage;

	void OnTriggerEnter2D(Collider2D other){
		/*
		if(other.name=="MainHand"){
			//Bug
			if(user.CompareTag("Ally") & other.transform.parent.parent.CompareTag("Enemy")){
				storedMinDamage = other.GetComponent<Weapon>().minDamage;
				other.GetComponent<Weapon>().maxDamage -= shieldArmor;
				if(other.GetComponent<Weapon>().maxDamage<=other.GetComponent<Weapon>().minDamage)
				other.GetComponent<Weapon>().maxDamage = other.GetComponent<Weapon>().minDamage;
				other.GetComponent<Weapon>().maxDamage = storedMaxDamage;
			}
		}
		 */
	}




}
