using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerNetworkBehaviour : NetworkBehaviour {

	void Start () {
		Debug.Log ("Me!");
	}

	void OnDestroy() {
		Debug.LogError("Was destroyed");
	}
}
