using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	bool allowMovement;
	GameObject ball;

	// Use this for initialization
	void Start () {
		this.allowMovement = false;
		ball = GameObject.Find ("Ball");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		this.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
		if (this.allowMovement == true) {
			MovePlayer ();
			if (Input.GetKeyDown (KeyCode.Space)) {
				Shoot ();
			}
		}

		
	}

	public void SetFocus(bool focus) {
		this.GetComponentInChildren<Camera> ().enabled = focus;
		this.allowMovement = focus;

		if (focus == false) {
			this.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
		}
		else if (focus == true) {
			this.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
		}

	}

	void MovePlayer() {
		//this.transform.Translate (new Vector3 (3*Input.GetAxis ("Horizontal")*Time.deltaTime, 0, 0));
		this.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(3*Input.GetAxis("Horizontal")*Time.deltaTime, 0, 0), ForceMode.Impulse);
	}

	void Shoot() {
		/*RaycastHit hit;
		Debug.DrawRay(new Vector3(transform.position.x,0.2f,transform.position.z), transform.TransformDirection(Vector3.forward)*5, Color.blue, duration:10f);
		if (Physics.Raycast (new Vector3(transform.position.x,0.2f,transform.position.z), transform.TransformDirection (Vector3.forward), out hit, 5)) {
			Debug.Log ("raycast!");
			Debug.Log (hit.collider);
			if(hit.collider == ball.GetComponent<Collider>()) {
				Debug.Log("Hit the ball");
				ball.GetComponent<BallScript> ().MoveBall (transform.position);
			}
		}*/

		if ((ball.transform.position - this.transform.position).magnitude <= 5) {
			Debug.Log ("Ball is close enough");
			ball.GetComponent<Rigidbody>().AddForce (new Vector3 ((ball.transform.position.x - this.transform.position.x), 0,
				(ball.transform.position.z - this.transform.position.z)).normalized*700);
		}
	}


}
