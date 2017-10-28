using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
	public Transform goal;
	UnityEngine.AI.NavMeshAgent agent;
	void Start () {
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
	}

	void FixedUpdate(){
		SetGoal();
	}

	void SetGoal(){      	
		if (Input.GetMouseButtonDown(0)) {
			RaycastHit hit;
			
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 10000)) {
				agent.destination = hit.point;
			}
		}
	}
}
