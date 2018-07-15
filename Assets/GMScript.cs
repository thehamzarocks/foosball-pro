using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMScript : MonoBehaviour {

	public List<GameObject> redPlayers;
	public List<GameObject> bluePlayers;

	private int turn;

	// Use this for initialization
	void Start () {
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

		redPlayers [0].GetComponent<PlayerScript> ().SetFocus (true);
	}
	
	// Update is called once per frame
	void Update () {		
		RegisterInput ();

	}

	void RegisterInput() {
		for (int i = 0; i < 10; i++) {
			if(Input.GetKeyDown(i.ToString())) {
				ChangePlayerFocus (i);
			}
		}

		if(Input.GetKeyDown(KeyCode.Space)) {
			ChangeTurn ();
		}
	}

	void ChangePlayerFocus(int playerNumber) {
		if (turn == 0) {
			for (int i = 0; i < 10; i++) {
				redPlayers [i].GetComponent<PlayerScript> ().SetFocus (i == playerNumber);
			}
		} else if (turn == 1) {
			for (int i = 0; i < 10; i++) {
				bluePlayers[i].GetComponent<PlayerScript> ().SetFocus (i == playerNumber);
			}
		}
	}

	void ChangeTurn() {
		if (turn == 0) {
			for (int i = 0; i < 10; i++) {
				redPlayers [i].GetComponent<PlayerScript> ().SetFocus (false);
			}
			bluePlayers [0].GetComponent<PlayerScript> ().SetFocus (true);

			turn = 1;
		} 

		else if (turn == 1) {
			for (int i = 0; i < 10; i++) {
				bluePlayers[i].GetComponent<PlayerScript> ().SetFocus (false);
			}
			redPlayers [0].GetComponent<PlayerScript> ().SetFocus (true);

			turn = 0;
		}
	}
}
