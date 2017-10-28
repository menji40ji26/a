using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFight : MonoBehaviour {


	public AIClass aiClass;
	public AIMovement aiMovement;
	public AIInteract aiInteract;

	public float hp;
	public bool dead;
	public bool savable;

	public Transform body;
	public Transform head;
	public Transform leftHand;
	public Transform rightHand;
	public Transform mainHand;
	public Transform offHand;


	public Animator animator;

	// Use this for initialization
	void Start () {
		savable = true;
		dead = false;
		playerFight = null;
		hasTarget = false;

		animator = transform.GetComponent<Animator>();

		findTargetCD = 6f;
	}
	
	float findTargetCD;
	float nextFindTarget;


	// Update is called once per frame
	void Update () {



		if(!dead){
			if(target){
				animator.SetBool("NoTarget",false); 
				ClearTarget();
			}
			if(Time.time>nextFindTarget | !target | movingToSpot){

				nextFindTarget = findTargetCD + Time.time;
				FindTargetWithTag();
			}
			if(movingToSpot){
				CancelDraw();
				aimming = false;
			}

			if(hasTarget & !movingToSpot){

				if(!aiInteract.driving & aiClass.useBow & aiMovement.inRange ){

					HoldBow();
					ShootArrow();
					AimHorse();
				} else {
					CancelDraw();
				}
			} else if(!movingToSpot) {
				CancelDraw();
			} 
		}



		if(hp<=0){
			aiInteract.GetOff();
			down();
			CheckSavable();
			dead = true;
			//Vanish();
		}
	}

	void CheckSavable(){
		if(!dead & aiClass.leader == "Player"){
			if(aiClass.unitCard.GetComponent<Unit>().firstAid * 10 > Random.Range(0,100)){
				print("Savable");
			}
		}
	}

	
	void CancelDraw(){
		animator.ResetTrigger("Draw"); 
		animator.ResetTrigger("Aimming"); 
		animator.ResetTrigger("DrawCrossbow"); 
		animator.ResetTrigger("AimmingCrossbow"); 
		animator.ResetTrigger("Loss"); 
		animator.ResetTrigger("LossCrossbow"); 
	}

	//Find Target
	public Transform target;
	PlayerFight playerFight;
	public AIFight aiFight;
	public bool hasTarget = false;

	void ClearTarget(){

		if(!aiMovement.inRange){
			aimming = false;
		}

		if(playerFight){

			if( gameObject.CompareTag("Enemy") & target.CompareTag("Player")){
				if(playerFight.dead){

					target = null;
					aiMovement.target = null;
					hasTarget = false;
					aiFight = null;
				}

			}
		} else if(aiFight){
			if ( gameObject.CompareTag("Enemy") | target.CompareTag("Ally") ){
				if(aiFight.dead){

					target = null;
					aiMovement.target = null;
					hasTarget = false;
					playerFight = null;
				}
			} else if ( gameObject.CompareTag("Ally") | target.CompareTag("Enemy") ){
				if(aiFight.dead){

					target = null;
					aiMovement.target = null;
					hasTarget = false;
				}
			}
		}
		
		
	}

	
    public GameObject[] allTargets;
	public Transform nearestTarget;
	public float nearestTargetDistance;
	int nearestTargetNumber;
	float distance;
	float lastDistance;
	float playerDistance;

	void FindTargetWithTag(){
		if(gameObject.CompareTag("Ally")){
			allTargets =  GameObject.FindGameObjectsWithTag("Enemy");
		} else if (gameObject.CompareTag("Enemy")){
			allTargets =  GameObject.FindGameObjectsWithTag("Ally");
		} 

		for (int i = 0; i < allTargets.Length; i++){
			distance = Vector2.Distance(allTargets[i].transform.position, transform.position);
			if(i-1>=0){
				lastDistance = Vector2.Distance(allTargets[i-1].transform.position, transform.position);
				if(allTargets.Length-1 > nearestTargetNumber ){
					if(distance<lastDistance 
					& distance<Vector2.Distance(allTargets[nearestTargetNumber].transform.position, transform.position)
					& !allTargets[i].GetComponent<AIFight>().dead){
						nearestTarget = allTargets[i].transform;
						nearestTargetNumber = i;
					}
				} else {
					nearestTargetNumber -= 1;
				}
			}
		}
		if(allTargets.Length-1 >= nearestTargetNumber){
				nearestTarget = allTargets[nearestTargetNumber].transform;
		} 

		if(allTargets.Length == 1){
			nearestTarget = allTargets[0].transform;
		} else if(allTargets.Length == 0)  {
			nearestTarget = null;
			hasTarget = false;
		}

		if(gameObject.CompareTag("Enemy")){

			GameObject player;
			player = null;
			player = GameObject.FindGameObjectWithTag("Player");

			if(player){
				playerDistance = Vector2.Distance(player.transform.position, transform.position);
			}


			if(allTargets.Length == 1 & !player){
				nearestTarget = allTargets[0].transform;
				hasTarget = true;
			} else if(allTargets.Length == 0 & !player)  {
				nearestTarget = null;
				hasTarget = false;
			}
			
			if ( player & nearestTarget){
				
				if(playerDistance < Vector2.Distance(nearestTarget.transform.position, transform.position)){
					nearestTarget  = player.transform;
					hasTarget = true;
				}
				
			} else if ( player ){
				nearestTarget  = player.transform;
				hasTarget = true;
			}
		}
		





		if(!movingToSpot & nearestTarget){


			if(gameObject.CompareTag("Ally") & nearestTarget.CompareTag("Enemy")){
				if(!nearestTarget.GetComponent<AIFight>().dead){
					target = nearestTarget.GetComponent<Transform>();
					aiFight = target.GetComponent<AIFight>();
					aiMovement.target = target;
					hasTarget = true;
					playerFight = null;
				}
			} else if(gameObject.CompareTag("Enemy") & nearestTarget.CompareTag("Ally")){
				if(!nearestTarget.GetComponent<AIFight>().dead){
					target = nearestTarget.GetComponent<Transform>();
					aiFight = target.GetComponent<AIFight>();
					aiMovement.target = target;
					hasTarget = true;
					playerFight = null;
				}
			} else if(gameObject.CompareTag("Enemy") & nearestTarget.CompareTag("Player")){
				if(!nearestTarget.GetComponent<PlayerFight>().dead){
					target = nearestTarget.GetComponent<Transform>();
					playerFight = target.GetComponent<PlayerFight>();
					aiMovement.target = target;
					hasTarget = true;
					aiFight = null;
				}
			}

			//Spearman targets horse first
			nearestTargetDistance = Vector2.Distance(nearestTarget.transform.position, transform.position);

			if(aiClass.useSpear){
				if(nearestTargetDistance>aiClass.spear.nearestHorseDistance & aiClass.spear.horse){
					target = aiClass.spear.target;
					aiMovement.target = aiClass.spear.target;
				}
			}

		}



	}

	public bool movingToSpot = false;

	void FindEmptySpot(Transform other){

		if(gameObject.CompareTag("Ally") & other.CompareTag("Chariot")){
			if (other.gameObject.layer == LayerMask.NameToLayer("Chariot")
			| other.gameObject.layer == LayerMask.NameToLayer("FriendlyChariot")
			){


				aiMovement.target = other.GetComponent<Transform>();

			}
		} else if(gameObject.CompareTag("Enemy") & other.CompareTag("Chariot")){
			if (other.gameObject.layer == LayerMask.NameToLayer("Chariot")
			| other.gameObject.layer == LayerMask.NameToLayer("EnemyChariot")
			){


				aiMovement.target = other.GetComponent<Transform>();

			}
		}
	}

	void MoveToSpot(){
		if(aiMovement.target){

			if(aiMovement.target.CompareTag("Chariot")){

				if(aiMovement.target.GetComponent<ChariotPassengers>().hasSpot){
					movingToSpot = true;


				} else if(aiMovement.target.GetComponent<ChariotPassengers>().hasSpot == false){
					aiMovement.target = null;
					movingToSpot = false;

				}

				if(aiInteract.onSpot)
				movingToSpot = false;
			}
		}
		
	}


	void Idle(){
		aiMovement.idle = true;
	}
		


	public GameObject arrow;
	public Transform shotSpawn;

    public float fireRate;
    private float nextFire;

	

	public float drawTime = 0;
	float erroRanger = 65f;
	Quaternion angle;

	Quaternion error;

	float minDraw;
	float maxDraw;

	void ShootArrow(){

		minDraw = fireRate ;
		maxDraw = fireRate * 2;

		if(target!=null){

			if ( Time.time > nextFire & aiMovement.inRange)  {
				drawTime += 3 * Time.deltaTime;
				erroRanger -= 30 * Time.deltaTime;


				if(!aiClass.bow.crossbow){

					animator.SetTrigger("Draw");
					animator.SetTrigger("Aim");
				} else {

					animator.SetTrigger("DrawCrossbow"); 
					animator.SetTrigger("AimmingCrossbow"); 
				}




			} else {
				drawTime = 0;
			}

			if( drawTime  > minDraw + Random.Range(0,2 * maxDraw) && Time.time > nextFire){
				if(drawTime< 8){
					drawTime = 8;
				} else if (drawTime> 17 ){
					drawTime = 17;
				}

				if(erroRanger< 2){
					erroRanger = 2f;
				}

				arrow.GetComponent<Arrow>().speed = drawTime ;
				nextFire = Time.time + fireRate;
				angle = transform.rotation;
				ShootingError();
				GameObject newArrow = Instantiate(arrow, shotSpawn.position, shotSpawn.rotation);
				newArrow.GetComponent<Arrow>().shooter = aiClass;

				//Use Ammo
				aiClass.bow.UseAmmo();
				newArrow.GetComponent<Arrow>().basicDamage = Random.Range(aiClass.bow.minDamage,aiClass.bow.maxDamage);
				if(gameObject.CompareTag("Enemy")){
					newArrow.layer = LayerMask.NameToLayer( "EnemyFire" );
				} else if (gameObject.CompareTag("Ally")){
					newArrow.layer = LayerMask.NameToLayer( "FriendlyFire" );
				}
				
				drawTime = 0;
				erroRanger = 30f;
				if(!aiClass.bow.crossbow){

					animator.SetTrigger("Loss");
				} else {

					animator.SetTrigger("LossCrossbow");
				}


			}

		}

	}


	void ShootingError(){
		error.eulerAngles = new Vector3(0, 0, Random.Range(-erroRanger, erroRanger) + 15);
		angle.eulerAngles += error.eulerAngles; 
	}

	Quaternion aimmingDirection;
	Quaternion rightHandRotation;
	Quaternion bodyRotation;
	public bool aimming = false;

	void HoldBow(){

		if(drawTime>0 & target){

			if(!aiClass.bow.crossbow){

				animator.SetTrigger("Draw");
				animator.SetTrigger("Aim");
			} else {

				animator.SetTrigger("DrawCrossbow"); 
				animator.SetTrigger("AimmingCrossbow"); 
			}
			
			Vector2 direction = target.position - transform.position;

			if(aim){
				direction = aim.position - transform.position;

			}



			float turnAngle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90;
			bodyRotation = Quaternion.AngleAxis(turnAngle, Vector3.forward);

			
			


			leftHand.localPosition = new Vector2(-6.0f, 6.5f);
			rightHand.localPosition = new Vector2(1.0f, 2.0f);
			aimmingDirection.eulerAngles = new Vector3(0,0,0f);
			rightHandRotation.eulerAngles = new Vector3(0,0,-9.5f);

		 
			aimming = true;
		} else {
			
			leftHand.localPosition = new Vector2(-2.67f, 5.68f);
			rightHand.localPosition = new Vector2(3.6f, 3.6f);
			aimmingDirection.eulerAngles = new Vector3(0,0,0);
			rightHandRotation.eulerAngles = new Vector3(0,0,0);
			bodyRotation.eulerAngles = new Vector3(0,0,0f);
 		

			aimming = false;
		}

		
		leftHand.localRotation = aimmingDirection;
		rightHand.localRotation = rightHandRotation;
		if(aimming){
			animator.ResetTrigger("DrawCrossbow");
			animator.ResetTrigger("Draw");
			animator.ResetTrigger("AimmingCrossbow");
			animator.ResetTrigger("Aimming");
       		transform.rotation = Quaternion.Slerp(transform.rotation, bodyRotation, Time.deltaTime * 3 );
		}

		 
	}

	public Transform aim;

	void AimHorse(){
		if( transform.CompareTag("Enemy") & target.gameObject.layer == LayerMask.NameToLayer( "Passenger" )
		| transform.CompareTag("Ally") & target.gameObject.layer == LayerMask.NameToLayer( "EnemyPassenger" )){

			for (int i = 0; i < target.parent.parent.childCount; i++){	
				if(target.parent.parent.GetChild(i).CompareTag("Bar")){


					for (int n = 0; n < target.parent.parent.GetChild(i).childCount; n++){	
						if(target.parent.parent.GetChild(i).GetChild(n).CompareTag("Horse")){



							for (int x = 0; x < target.parent.parent.GetChild(i).GetChild(n).childCount; x++){	
								if(target.parent.parent.GetChild(i).GetChild(n).GetChild(x).name == "Aim"){
									aim = target.parent.parent.GetChild(i).GetChild(n).GetChild(x);
								}
							}
						}
					}

				}
			}
		} else {
			aim = null;
		}
	}






	Quaternion downAngle;

	bool HasDownDirection = false;
	SpriteRenderer bodySprite;
	SpriteRenderer headSprite;
	SpriteRenderer leftHandSprite;
	SpriteRenderer rightHandSprite;
	SpriteRenderer mianHandSprite;
	SpriteRenderer offHandSprite;
	SpriteRenderer bowLeftSprite;
	SpriteRenderer bowRightSprite;
	SpriteRenderer armorSprite;
	SpriteRenderer helmetSprite;
	SpriteRenderer backSprite;
	SpriteRenderer back2Sprite;




	void down(){


		transform.parent = null;
		aiInteract.spot = null;
		if(!animator.GetBool("Dead"))
		animator.SetTrigger("Dying"); 
		animator.SetBool("Dead",true);
		animator.SetBool("Running",false);




		gameObject.layer = LayerMask.NameToLayer( "DeadBody" );
		bodySprite = body.GetComponent<SpriteRenderer>();
		headSprite = head.GetComponent<SpriteRenderer>();
		leftHandSprite = leftHand.GetComponent<SpriteRenderer>();
		rightHandSprite = rightHand.GetComponent<SpriteRenderer>();
		mianHandSprite = mainHand.GetComponent<SpriteRenderer>();
		offHandSprite = offHand.GetComponent<SpriteRenderer>();
		bowLeftSprite = offHand.GetChild(0).GetComponent<SpriteRenderer>();
		bowRightSprite = offHand.GetChild(1).GetComponent<SpriteRenderer>();
		armorSprite = body.GetChild(0).GetComponent<SpriteRenderer>();
		helmetSprite = head.GetChild(0).GetComponent<SpriteRenderer>();
		backSprite = body.GetChild(1).GetComponent<SpriteRenderer>();
		back2Sprite = body.GetChild(1).GetComponent<SpriteRenderer>();

		bodySprite.sortingLayerName = "DeadBody";
		headSprite.sortingLayerName = "DeadBody";
		leftHandSprite.sortingLayerName = "DeadBody";
		rightHandSprite.sortingLayerName = "DeadBody";
		mianHandSprite.sortingLayerName = "DeadBody";
		offHandSprite.sortingLayerName = "DeadBody";
		bowLeftSprite.sortingLayerName = "DeadBody";
		bowRightSprite.sortingLayerName = "DeadBody";
		armorSprite.sortingLayerName = "DeadBody";
		helmetSprite.sortingLayerName = "DeadBody";
		backSprite.sortingLayerName = "DeadBody";
		back2Sprite.sortingLayerName = "DeadBody";
		
		gameObject.tag = "DeadBody";
		
	}



	float fadeTime = 100f;

	void Vanish(){
	
		bodySprite.color = new Color(bodySprite.color.r, bodySprite.color.g, bodySprite.color.b, fadeTime);
		headSprite.color = new Color(headSprite.color.r, headSprite.color.g, headSprite.color.b, fadeTime);
		leftHandSprite.color = new Color(bodySprite.color.r, bodySprite.color.g, bodySprite.color.b, fadeTime);
		rightHandSprite.color = new Color(headSprite.color.r, headSprite.color.g, headSprite.color.b, fadeTime);
		mianHandSprite.color = new Color(bodySprite.color.r, bodySprite.color.g, bodySprite.color.b, fadeTime);
		offHandSprite.color = new Color(headSprite.color.r, headSprite.color.g, headSprite.color.b, fadeTime);

		fadeTime -= 0.5f * Time.deltaTime;
		if(fadeTime<=0){
			Destroy(this.gameObject);
		}
	}
}
