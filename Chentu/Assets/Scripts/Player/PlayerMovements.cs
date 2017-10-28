using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour {

	//Vector3 movement;
	public bool moveable = true;

	public float speed;

	public float moveSpeed;
	public float horizontalMoveSpeed;
	public float maxSpeed;
	
	public float acceleration;
	public float drag;

	public float turnSpeed;

	public float verticalSpeed;
	float turnAngle;

	float cornorSpeed;
	float chargeSpeed;
	float maxChargeSpeed;
	float storedMaxSpeed;
	float cornorChargeSpeed;

	public Weapon weapon;
	Rigidbody2D rb;
	Animator animator;
	PlayerInteract playerInteract;

	// Use this for initialization
	void Start () {
		maxSpeed = 0.2f;
		rb = transform.parent.GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		playerInteract = GetComponent<PlayerInteract>();
	}

	public bool onVehicle;
	// Update is called once per frame
	void FixedUpdate () {
		if(moveable ){
			if(!onVehicle & !playerInteract.operating & !playerInteract.taking){
				Moving();
			} else {

				animator.SetBool("Running", false);
			}
			if(!playerInteract.taking)
			Turning();
		}
	}

	public Transform battleField;

	float slowDownTime;
	public Vector3 movement;

	void Moving(){




	 	float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");

        movement = new Vector3 (moveHorizontal, moveVertical, 0.0f);
		movement = movement.normalized;
        rb.AddForce (movement * speed * 500 * Time.deltaTime);

		if(movement != Vector3.zero  ){
			//slowDownTime = Time.time;
			animator.SetBool("Running", true);
		} else {

			//if(Time.time - slowDownTime > 1)
			animator.SetBool("Running", false);
		}




	}
	
	public PlayerFight playerFight;
	public Transform player;

	void Turning(){

		Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90;
		Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		player.transform.rotation = Quaternion.Slerp(player.transform.rotation, rotation, turnSpeed * Time.deltaTime);

	}


	public void Look(Collider2D attentionPoint){
		Vector2 direction = attentionPoint.transform.position - transform.position;
		float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90;
		Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		player.transform.rotation = Quaternion.Slerp(player.transform.rotation, rotation, 1 );
	}



}
