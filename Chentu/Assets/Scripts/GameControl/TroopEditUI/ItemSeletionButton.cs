using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSeletionButton : MonoBehaviour{

	public string equitmentType;
	public int equitmentTypeNumber;
	public float price;

	public bool isMainHand;
	public bool isOffHand;
	public bool isTwohanded;

	public bool isArmor;
	public bool isHelmet;
	public bool isShield;

	public bool isMount;


	//Attribute
	Weapon weapon;
	Armor armor;

	public float minDamage;
	public float maxDamage;

	public float defense;

	public Text nameText;
	public Text costText;
	public ItemSeletion itemSeletion;
	public TroopInfo troopInfo;
	public TroopScale troopScale;

	// Use this for initialization
	void Start () {
		weapon = GetComponent<Weapon>();
		armor = GetComponent<Armor>();

		if(isMainHand|isOffHand|isTwohanded){
			weapon.enabled = true;
			armor.enabled = false;
			GetAttributeFromWeaponDatabase();
		} else if (isArmor|isHelmet|isShield){
			armor.enabled = true;
			weapon.enabled = false;
			GetAttributeFromArmorDatabase();
		} else if (isMount){
			GetAttributeFromMountDatabase();
		}


		//itemSeletion = transform.parent.parent.parent.GetComponent<ItemSeletion>();
		nameText = GetComponentInChildren<Text>();
		troopInfo = GameObject.FindWithTag("TroopInfo").GetComponent<TroopInfo>();

		SetPreviewImage();
		SetPrice();
	}
	
	
	

	public void GetAttributeFromWeaponDatabase(){
		if(equitmentType=="Sword"){
			//weapon.sword = true;

		} else if(equitmentType=="Crossbow"){
			weapon.crossbow = true;
		} else if(equitmentType=="Spear"){
			weapon.spear = true;
		} else if(equitmentType=="Axe"){
			weapon.axe = true;
		} 
		weapon.tagName = equitmentType;
		weapon.type = equitmentTypeNumber;
		weapon.CheckType();
		minDamage = weapon.minDamage;
		maxDamage = weapon.maxDamage;
		price = weapon.price;
	}


	public void GetAttributeFromArmorDatabase(){
		if(equitmentType=="Armor"){
			armor.armorType = equitmentTypeNumber;
			armor.CheckType();
			defense = armor.bodyArmor;
		} else if(equitmentType=="Helmet"){
			armor.helmetType = equitmentTypeNumber;
			armor.CheckType();
			defense = armor.helmetArmor;
		} else if(equitmentType=="Shield"){
			armor.shieldType = equitmentTypeNumber;
			armor.CheckType();
			defense = armor.shieldArmor;
		}

		
		price = armor.price;
	}


	public void GetAttributeFromMountDatabase(){
		if(equitmentType=="Mount"){
			price = 100;
		}
	}

	public void SeletItem(){

		GetPreviewObject();



		
		if(itemSeletion.currentGold>=price*itemSeletion.currentEditingUnitNumber){
			SetAttribute();
			
			if(itemSeletion.currentEditingUnit == "a"){

				if(isMainHand){
					troopInfo.tagAm = equitmentType;
					troopInfo.mainHandA = equitmentTypeNumber;
					itemSeletion.unitAMainHandCost = price;
				} else if(isOffHand){
					troopInfo.tagAo = equitmentType;
					troopInfo.offHandA = equitmentTypeNumber;
					itemSeletion.unitAOffHandCost = price;
				} else if (isTwohanded){
					troopInfo.tagAo = "Untagged";
					troopInfo.tagAm = equitmentType;
					troopInfo.mainHandA = equitmentTypeNumber;
					itemSeletion.unitAMainHandCost = price;
					itemSeletion.unitAOffHandCost = 0f;
				}

				//Armor
				else if (isArmor){
					troopInfo.armorA = equitmentTypeNumber;
					itemSeletion.unitAArmorCost = price;
				}
				else if (isHelmet){
					troopInfo.helmetA = equitmentTypeNumber;
					itemSeletion.unitAHelmetCost = price;
				}
				else if (isShield){
					troopInfo.tagAo = "Shield";
					itemSeletion.unitAOffHandCost = price;
				}

				//Mount
				else if (isMount){
					troopInfo.aMounted = true;
					troopInfo.aMountType = nameText.transform.parent.name;
					itemSeletion.unitAMountCost = price;
				}

			} else if(itemSeletion.currentEditingUnit == "b"){

				if(isMainHand){
					troopInfo.tagBm = equitmentType;
					troopInfo.mainHandB = equitmentTypeNumber;
					itemSeletion.unitBMainHandCost = price;
				} else if(isOffHand){
					troopInfo.tagBo = equitmentType;
					troopInfo.offHandB = equitmentTypeNumber;
					itemSeletion.unitBOffHandCost = price;
				} else if (isTwohanded){
					//troopInfo.tagBo = "Untagged";
					troopInfo.tagBm = equitmentType;
					troopInfo.mainHandB = equitmentTypeNumber;
					itemSeletion.unitBMainHandCost = price;
					//itemSeletion.unitBOffHandCost = 0f;
				}

				//Armor
				else if (isArmor){
					troopInfo.armorB = equitmentTypeNumber;
					itemSeletion.unitBArmorCost = price;
				}
				else if (isHelmet){
					troopInfo.helmetB = equitmentTypeNumber;
					itemSeletion.unitBHelmetCost = price;
				}
				else if (isShield){
					troopInfo.tagBo = "Shield";
					itemSeletion.unitBOffHandCost = price;
				}

				//Mount
				else if (isMount){
					troopInfo.bMounted = true;
					troopInfo.bMountType = nameText.transform.parent.name;
					itemSeletion.unitBMountCost = price;
				}

			} else if(itemSeletion.currentEditingUnit == "c"){

				if(isMainHand){
					troopInfo.tagCm = equitmentType;
					troopInfo.mainHandC = equitmentTypeNumber;
					itemSeletion.unitCMainHandCost = price;
				} else if(isOffHand){
					troopInfo.tagCo = equitmentType;
					troopInfo.offHandC = equitmentTypeNumber;
					itemSeletion.unitCOffHandCost = price;
				} else if (isTwohanded){
					troopInfo.tagCo = "Untagged";
					troopInfo.tagCm = equitmentType;
					troopInfo.mainHandC = equitmentTypeNumber;
					itemSeletion.unitCMainHandCost = price;
					itemSeletion.unitCOffHandCost = 0f;
				}

				//Armor
				else if (isArmor){
					troopInfo.armorC = equitmentTypeNumber;
					itemSeletion.unitCArmorCost = price;
				}
				else if (isHelmet){
					troopInfo.helmetC = equitmentTypeNumber;
					itemSeletion.unitCHelmetCost = price;
				}
				else if (isShield){
					troopInfo.tagCo = "Shield";
					itemSeletion.unitCOffHandCost = price;
				}

				//Mount
				else if (isMount){
					troopInfo.cMounted = true;
					troopInfo.cMountType = nameText.transform.parent.name;
					itemSeletion.unitCMountCost = price;
				}

			}

			itemSeletion.CountCost();
			itemSeletion.CountAttribute();
			SetPreview();
		} else {
			print("Insufficient Fund");
			Hint.hint.InsufficientFund();
		}


	}


	//Preview

	public Transform currentPreview;

	public SpriteRenderer helmet;
	public SpriteRenderer armorSprite;
	public SpriteRenderer back;
	public SpriteRenderer back2;
	public SpriteRenderer offHand;
	public SpriteRenderer mainHand;
	public SpriteRenderer mount;
	
	Sprite previewImage;

	void SetPrice(){
		costText.text = price + " " ;
	}

	void SetPreviewImage(){
		if(nameText.transform.parent.name == "Crossbow"){
			previewImage = ItemCollection.itemCollection.crossbow01;
		} else if(nameText.transform.parent.name == "Spear"){
			previewImage = ItemCollection.itemCollection.spear01;
		} else if(nameText.transform.parent.name == "BronzeSword"){
			previewImage = ItemCollection.itemCollection.sword02;
		} else if(nameText.transform.parent.name == "RingHeadedSword"){
			previewImage = ItemCollection.itemCollection.sword01;
		} else if(nameText.transform.parent.name == "Club"){
			previewImage = ItemCollection.itemCollection.axe01;
		} else if(nameText.transform.parent.name == "Axe"){
			previewImage = ItemCollection.itemCollection.axe02;
		} 

		//Armor
		else if(nameText.transform.parent.name == "LinenRobe"){
			previewImage = ItemCollection.itemCollection.armor01;
		} else if(nameText.transform.parent.name == "LeatherScaleArmor"){
			previewImage = ItemCollection.itemCollection.armor02;
		} else if(nameText.transform.parent.name == "Fur"){
			previewImage = ItemCollection.itemCollection.armor03;
		} 

		//Helmet 
		else if(nameText.transform.parent.name == "LeatherScaleHelmet"){
			previewImage = ItemCollection.itemCollection.helmet02;
		} 

		//Shield
		else if(nameText.transform.parent.name == "Shield"){
			previewImage = ItemCollection.itemCollection.shield01;
		} 

		//Mount
		else if(nameText.transform.parent.name == "Horse"){
			previewImage = ItemCollection.itemCollection.horse01;
		} else if(nameText.transform.parent.name == "Boar"){
			previewImage = ItemCollection.itemCollection.boar01;
		} 


	
		if(previewImage)
		transform.GetChild(0).GetComponent<Image>().sprite = previewImage;

	}


	public void SetPreview(){
		if(isMainHand){
			mainHand.sprite = previewImage;
			currentPreview.GetComponent<ManPreview>().previewingTwoHanded = false;
		} else if(isOffHand){
			offHand.sprite = null;
			offHand.sprite = previewImage;

			if(currentPreview.GetComponent<ManPreview>().previewingTwoHanded){
				mainHand.sprite = null;

				//Clear TwoHanded
				troopInfo.tagAm = "Untagged";
				troopInfo.mainHandA = 0;
			}



		} else if(isTwohanded){
			offHand.sprite = null;
			mainHand.sprite = previewImage;
			currentPreview.GetComponent<ManPreview>().previewingTwoHanded = true;
		}

		//Armor
 		else if(isArmor){
			 armorSprite.sprite = previewImage;
		}

 		else if(isHelmet){
			 helmet.sprite = previewImage;
		}

 		else if(isShield){
			 offHand.sprite = previewImage;
		}

		//Mount
		else if(isMount){
			mount.sprite = previewImage;
		}
			

	}


	public void SetAttribute(){
		if(isMainHand | isOffHand | isTwohanded){
			if(itemSeletion.currentEditingUnit == "a"){
				itemSeletion.unitAMinDamage = minDamage;
				itemSeletion.unitAMaxDamage = maxDamage;
			} else if(itemSeletion.currentEditingUnit == "b"){
				itemSeletion.unitBMinDamage = minDamage;
				itemSeletion.unitBMaxDamage = maxDamage;
			} else if(itemSeletion.currentEditingUnit == "c"){
				itemSeletion.unitCMinDamage = minDamage;
				itemSeletion.unitCMaxDamage = maxDamage;
			}
		} else if (isArmor){
			if(itemSeletion.currentEditingUnit == "a")
				itemSeletion.unitABodyDefense = defense;
			else if(itemSeletion.currentEditingUnit == "b")
				itemSeletion.unitBBodyDefense = defense;
			else if(itemSeletion.currentEditingUnit == "c")
				itemSeletion.unitCBodyDefense = defense;
		} else if (isHelmet){
			if(itemSeletion.currentEditingUnit == "a")
				itemSeletion.unitAHeadDefense = defense;
			else if(itemSeletion.currentEditingUnit == "b")
				itemSeletion.unitBHeadDefense = defense;
			else if(itemSeletion.currentEditingUnit == "c")
				itemSeletion.unitCHeadDefense = defense;
		} else if (isShield){
			if(itemSeletion.currentEditingUnit == "a")
				itemSeletion.unitAShieldDefense = defense;
			else if(itemSeletion.currentEditingUnit == "b")
				itemSeletion.unitBShieldDefense = defense;
			else if(itemSeletion.currentEditingUnit == "c")
				itemSeletion.unitCShieldDefense = defense;
		}
	}


	void GetPreviewObject(){

		itemSeletion = ItemSeletion.itemSeletion;

		if(itemSeletion.currentEditingUnit == "a")
			currentPreview = itemSeletion.manPreviewA;
		else if(itemSeletion.currentEditingUnit == "b")
			currentPreview = itemSeletion.manPreviewB;
		else if(itemSeletion.currentEditingUnit == "c")
			currentPreview = itemSeletion.manPreviewC;
			
		if(currentPreview){

			offHand = currentPreview.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
			mainHand = currentPreview.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>();
			helmet = currentPreview.GetChild(2).GetChild(0).GetComponent<SpriteRenderer>();
			armorSprite = currentPreview.GetChild(3).GetChild(0).GetComponent<SpriteRenderer>();
			back = currentPreview.GetChild(3).GetChild(1).GetComponent<SpriteRenderer>();
			back2 = currentPreview.GetChild(3).GetChild(2).GetComponent<SpriteRenderer>();
			mount = currentPreview.GetChild(4).GetComponent<SpriteRenderer>();
		}
	}

}

