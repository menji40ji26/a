using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chat : MonoBehaviour {


	public Text leftName;
	public Text rightName;
	public AI left;
	public AI right;

	public Text text;

	public GameObject optionList;
	public GameObject buttonA;
	public GameObject buttonB;
	public GameObject buttonC;
	public Text a;
	public Text b;
	public Text c;


	public GameObject actionList;
	public GameObject actionA;
	public GameObject actionB;
	public GameObject actionC;
	public Text actionAText;
	public Text actionBText;
	public Text actionCText;

	//Portraits
	public GameObject leftPortrait;
	public GameObject rightPortrait;

	public Image leftBody;
	public Image leftCloth;
	public Image leftFace;
	public Image leftBeard;
	public Image leftHair;

	public Image rightBody;
	public Image rightCloth;
	public Image rightFace;
	public Image rightBeard;
	public Image rightHair;

	public string answer;

	bool talkEnd;

	// Use this for initialization
	void Start () {

	}
	
	void OnEnable(){
		ResetChat();
	}

	void ResetChat(){
		talkEnd = false;
		actionList.SetActive(false);
		optionList.SetActive(true);
		if(actionAText.text == null ){
			actionA.SetActive(false);
		}
		if(actionBText.text == null ){
			actionB.SetActive(false);
		}
		if(actionCText.text == null ){
			actionC.SetActive(false);
		}
		SetGreeting();
		SetPortrait();
		SetOption();
		CheckQuest();
	}

	void SetGreeting(){
		if(left.job == "Farmer"){
			text.text = "幸会";
		}

	}

	// Update is called once per frame
	void Update () {
	}


	public void Greet(){
		if(left.job == "Farmer" ){
			SetFarmerTalk();
		}
		text.text = answer;
	}

	public Quest quest;

	public void Quest(){
		CheckQuest();
		SetOption();
		if(left.quest){
			quest = left.quest;
			actionList.SetActive(true);
			optionList.SetActive(false);
			text.text = quest.nodeA;
			actionAText.text = quest.a;
			actionBText.text = quest.b;
			actionCText.text = quest.c;

			if(quest.took){
				text.text = quest.nodeB;
			}

		}

	}

	void CheckQuest(){
		
		actionA.SetActive(true);
		actionB.SetActive(false);
		actionC.SetActive(false);

		if(right.quest = quest){
			actionA.SetActive(false);
			if(quest.reached){
				actionB.SetActive(true);
			}
			actionC.SetActive(true);
			b.text = "之前说的事，";
		}
	}

	public void DoA(){
		right.quest = quest;
		b.text = "之前说的事，";
		quest.took = true;
		text.text = left.quest.nodeB;
		actionList.SetActive(false);
		optionList.SetActive(true);
	}

	public void DoB(){
		text.text = left.quest.nodeC;
		quest.Finish();
		actionList.SetActive(false);
		optionList.SetActive(true);
		SetOption();
	}

	public void DoC(){
		actionList.SetActive(false);
		optionList.SetActive(true);
		EndTalk();
	}

	public void EndTalk(){

		right.GetComponent<PlayerInteract>().EndTalk();
	}



	public void SetBothSide(AI leftToSet, AI rightToSet){
		left = leftToSet;
		right = rightToSet;
		leftName.text = left.name;
		rightName.text = right.name;


	}

	void SetPortrait(){
		leftBody.sprite = left.body;
		leftCloth.sprite = left.cloth;
		leftFace.sprite = left.face;
		leftBeard.sprite = left.beard;
		leftHair.sprite = left.hair;

		rightBody.sprite = right.body;
		rightCloth.sprite = right.cloth;
		rightFace.sprite = right.face;
		rightBeard.sprite = right.beard;
		rightHair.sprite = right.hair;
	}


	//Database
	void SetOption(){
		if(left.job=="Farmer"){
			a.text = "收成可好？";
			b.text = "需要帮忙吗？";
			c.text = "乡里有何传言？";
		} else if(left.job=="Merchant"){
			a.text = "生意可好？";
			b.text = "需要短工吗？";
			c.text = "市井有何流言？";
		} 


		if(!left.quest){
			text.text = "我们暂不需帮手。";
		}
	}


	void SetFarmerTalk(){

		if(GameController.gameController.region.fertility == 0)
		answer = "也许是春祭过于清淡使谷神愠怒，若不然怎会遭此荒年……这个冬天不知又将有多少人死于饥馑……";
		if(GameController.gameController.region.fertility == 1)
		answer = "幸亏有" + GameController.gameController.region.lordName + "，虽不算丰年但过冬足矣。";
		if(GameController.gameController.region.fertility == 2)
		answer = "多亏" + GameController.gameController.region.lordName + "，今年庄稼打得好，总算有些余粮备荒了。";

	}


}
