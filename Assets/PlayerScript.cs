using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	bool allowMovement;
	GameObject ball;
	float shotPower;
	bool isCurrentTurn;

	// Use this for initialization
	void Start () {
		this.allowMovement = false;
		ball = GameObject.Find ("Ball");
		shotPower = 0f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		this.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
		if (this.allowMovement == true) {
			MovePlayer ();
			if (Input.GetKeyUp(KeyCode.Space)) {
				Shoot (shotPower);
				shotPower = 0;
			}
			if (Input.GetKey (KeyCode.Space)) {
				PowerUp (); 
			}
		}

		
	}

	public void SetFocus(bool focus) {
		//this.GetComponentInChildren<Camera> ().enabled = focus;
		this.allowMovement = focus;
		this.isCurrentTurn = focus;

		if (focus == false) {
			this.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
			gameObject.GetComponent<Light> ().enabled = false;
		}
		else if (focus == true) {
			this.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeRotation;
			gameObject.GetComponent<Light> ().enabled = true;
		}



	}

	void MovePlayer() {
		//this.transform.Translate (new Vector3 (3*Input.GetAxis ("Horizontal")*Time.deltaTime, 0, 0));
		this.GetComponent<Rigidbody>().AddForce(new Vector3(-3*Input.GetAxis("Vertical")*Time.deltaTime, 0, 
			-3*Input.GetAxis("Horizontal")*Time.deltaTime), ForceMode.Impulse);
	}

	void Shoot(float power) {		
		if (!isCurrentTurn) {
			return;
		}

		if ((ball.transform.position - this.transform.position).magnitude <= 5) {
			Debug.Log ("Ball is close enough");
			ball.GetComponent<Rigidbody>().AddForce (new Vector3 ((ball.transform.position.x - this.transform.position.x), 0,
				(ball.transform.position.z - this.transform.position.z)).normalized*shotPower*50);
		}
		isCurrentTurn = false;
	}

	void PowerUp () {
		if (shotPower >= 40) {
			Shoot (40);
			shotPower = 0;
		}

		else {
			shotPower++;
			Debug.Log (shotPower);
		}
			
	}
}
