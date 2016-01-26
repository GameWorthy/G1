using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class LobbyListScene : NetworkLobbyManager {

	void Start () {
		
	}

	public void CreateLobby() {
		this.CreateLobby ();
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.Space)) {
			Debug.Log (this.matches.Count);
		}
	}


}
