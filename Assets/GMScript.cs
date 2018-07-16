using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMScript : MonoBehaviour {

	public List<GameObject> redPlayers;
	public List<GameObject> bluePlayers;
	public GameObject ball;
	public Vector3 lastBallPosition;

	private int turn;

	// Use this for initialization
	void Start () {
		ball = GameObject.Find ("Ball");
		lastBallPosition = ball.transform.position;



		redPlayers = new List<GameObject> ();
		for (int i = 0; i < 10; i++) {
			GameObject player = GameObject.Find ("RedPrefab (" + i.ToString () + ")");
			player.GetComponent<PlayerScript> ().SetFocus (false);
			redPlayers.Add (player);
		}

		bluePlayers= new List<GameObject> ();
		for (int i = 0; i < 10; i++) {
			GameObject player = GameObject.Find ("BluePrefab (" + i.ToString () + ")");
			player.GetComponent<PlayerScript> ().SetFocus (false);
			bluePlayers.Add (player);
		}



		turn = 0;

		//redPlayers [0].GetComponent<PlayerScript> ().SetFocus (true);
		FindNearestPlayer().GetComponent<PlayerScript>().SetFocus(true);

	}
	
	// Update is called once per frame
	void Update () {
		CheckIfGoal ();
		RegisterInput ();

	}

	void RegisterInput() {
		
		if(Input.GetKeyUp(KeyCode.Space)) {
			StartCoroutine(ChangeTurn ());
		}
	}


	IEnumerator ChangeTurn() {
		if (turn == 0) {
			for (int i = 0; i < 10; i++) {
				redPlayers [i].GetComponent<PlayerScript> ().SetFocus (false);
				bluePlayers [i].GetComponent<PlayerScript> ().SetFocus (false);
			}
			yield return new WaitForSeconds(1);
			ball.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
			turn = 1;
			GameObject player = FindNearestPlayer();
			player.GetComponent<PlayerScript> ().SetFocus (true);
		} 

		else if (turn == 1) {
			for (int i = 0; i < 10; i++) {
				bluePlayers[i].GetComponent<PlayerScript> ().SetFocus (false);
				redPlayers [i].GetComponent<PlayerScript> ().SetFocus (false);
			}
			yield return new WaitForSeconds (1);
			ball.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
			turn = 0;
			FindNearestPlayer ().GetComponent<PlayerScript> ().SetFocus (true);
		}
	}

	//Finds the player closest to the ball
	GameObject FindNearestPlayer() {

		float minDistance = 100;
		float currentDistance;
		Vector3 playerPosition;
		Vector3 ballPosition = ball.transform.position;
		int nearestPlayerNumber = 0;
		if (turn == 0) {
			for (int i = 0; i < 10; i++) {
				playerPosition = redPlayers [i].transform.position;
				currentDistance = (ballPosition - playerPosition).magnitude;
				if (currentDistance < minDistance) {
					nearestPlayerNumber = i;
					minDistance = currentDistance;
				}
			}
			return redPlayers [nearestPlayerNumber];
		}

		else if (turn == 1) {
			for (int i = 0; i < 10; i++) {
				playerPosition = bluePlayers [i].transform.position;
				currentDistance = (ballPosition - playerPosition).magnitude;
				if (currentDistance < minDistance) {
					nearestPlayerNumber = i;
					minDistance = currentDistance;
				}
			}
			return bluePlayers [nearestPlayerNumber];
		}

		return redPlayers [0];
		
	}

	void CheckIfGoal() {
		if (ball.transform.position.z >= 14) {
			Debug.Log ("Red Scores!");
			ball.transform.position = new Vector3 (0, 0.5f, 12);
			ball.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;

			//this changes the turn to blue's
			turn = 0;
			ChangeTurn ();
		}

		if (ball.transform.position.z <= -14) {
			Debug.Log ("Blue Scores!");
			ball.transform.position = new Vector3 (0, 0.5f, -12);
			ball.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
			//this changes the turn to red's
			turn = 1;
			ChangeTurn ();
		}
	}


}
