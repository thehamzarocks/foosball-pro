using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void MoveBall(Vector3 playerPosition) {
		Debug.Log (playerPosition);
		Vector3 direction = new Vector3 (transform.position.x - playerPosition.x, 0, transform.position.z - playerPosition.z);
		direction.Normalize ();
		this.GetComponent<Rigidbody> ().AddForce (direction*10, ForceMode.Impulse);
	}
}
