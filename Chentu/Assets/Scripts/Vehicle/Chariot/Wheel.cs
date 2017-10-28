using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour {

	public ChariotMovement chariotMovement;
	public float wheelType;
	public float hp;
	public float impact;
	public bool intact;

	public GameObject trackPrefab;



	// Use this for initialization
	void Start () {
		chariotMovement = transform.parent.GetComponent<ChariotMovement>();
		if(wheelType == 0){
			hp = 100f;
			impact = 2f;
		} else if(wheelType == 1){
			hp = 70f;
			impact = 5f;
		}
		fadeTime = 200f;
		intact = true;
	}
	
	// Update is called once per frame
	void Update () {
		LeaveTrack();
		LossDurability();
		Loose();
		Break();


	}

	void LossDurability(){
		if(hp>0){
			hp -= 5 * Time.deltaTime * chariotMovement.moveSpeed;
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		Impact(other);
	}


	


	void Impact(Collision2D other) {
		float impactDamge = 0;
		if(gameObject.layer == LayerMask.NameToLayer( "FriendlyChariot" ) &  other.transform.CompareTag("Enemy")  
		| gameObject.layer == LayerMask.NameToLayer( "EnemyChariot" ) &  other.transform.CompareTag("Ally") ){
			impactDamge = chariotMovement.moveSpeed * impact * 20;
			other.collider.GetComponent<AIFight>().hp -= impactDamge;
			chariotMovement.moveSpeed *= 0.8f;
		} else if(gameObject.layer == LayerMask.NameToLayer( "EnemyChariot" ) &  other.transform.CompareTag("Player")) {
			impactDamge = chariotMovement.moveSpeed * impact * 30;
			other.collider.GetComponent<PlayerFight>().hp -= impactDamge;
			chariotMovement.moveSpeed *= 0.8f;
		}
		//Impact horse
		 else if(gameObject.layer == LayerMask.NameToLayer( "FriendlyChariot" ) &  other.transform.CompareTag("Horse")  
		| gameObject.layer == LayerMask.NameToLayer( "EnemyChariot" ) &  other.transform.CompareTag("Horse") ){
			impactDamge = chariotMovement.moveSpeed * impact * 20;
			other.collider.GetComponent<Horse>().hp -= impactDamge;
			chariotMovement.moveSpeed *= 0.8f;
		}
		//Impact Wheel
		 else if(gameObject.layer == LayerMask.NameToLayer( "FriendlyChariot" ) &  other.transform.CompareTag("Wheel")  
		| gameObject.layer == LayerMask.NameToLayer( "EnemyChariot" ) &  other.transform.CompareTag("Wheel") ){
			impactDamge = chariotMovement.moveSpeed * impact * 20;
			other.collider.GetComponent<Wheel>().hp -= impactDamge;
			chariotMovement.moveSpeed *= 0.8f;
		}

	}

	GameObject trackClone;
	float nextTrack;
	float trackRate = 0.2f;

	void LeaveTrack(){
		if( Time.time > nextTrack & chariotMovement.moveSpeed > 0){

			nextTrack = Time.time + trackRate;
			trackClone = Instantiate(trackPrefab, transform.position, transform.rotation);
		}
	}

	void Loose(){
		if(gameObject.name == "LeftWheel" & hp < 50){
			chariotMovement.animator.SetTrigger("LooseLeftWheel");
		}
		if(gameObject.name == "RightWheel" & hp < 50){
			chariotMovement.animator.SetTrigger("LooseRightWheel");
		}
	}

	void Lost(){
		if(gameObject.name == "LeftWheel" ){

			chariotMovement.animator.SetTrigger("LooseLeftWheel");
			chariotMovement.animator.SetTrigger("LostLeftWheel");
		}
		if(gameObject.name == "RightWheel" ){

			chariotMovement.animator.SetTrigger("LooseRightWheel");
			chariotMovement.animator.SetTrigger("LostRightWheel");
		}
		chariotMovement.noHorse = true;
	}

	void Break(){
		if(hp<=0){
			Vanish();
		}
		if(hp<=0 & intact){
			Lost();
			chariotMovement.turnSpeed /= 2;
			intact = false;
			Loss();
		}
	}
	
	public float fadeTime;


	void Loss(){
		transform.parent = null;
		gameObject.tag = "DeadBody";
		gameObject.layer = LayerMask.NameToLayer("DeadBody");
		GetComponent<SpriteRenderer>().sortingLayerName = "DeadBody";
	}

	void Vanish(){
		
		GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, fadeTime);
		fadeTime -= 0.5f * Time.deltaTime;
		if(fadeTime<=0){
			Destroy(this.gameObject);
			//print("Destoried");
		}
		//print(fadeTime);
	}
}
