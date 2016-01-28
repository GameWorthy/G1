using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerTag : NetworkLobbyPlayer {
	
	[SerializeField] private Text playerNameText;
	[SerializeField] private Image background;
	[SerializeField] private Image ready;

	public int ID {
		get;
		set;
	}

	[SyncVar(hook = "OnMyName")]
	public string playerName = "";

	void SetAsLocalClient() {
		background.color = Color.green;
	}

	public override void OnClientReady(bool _ready) {
		base.OnClientReady (_ready);
		SetReady (_ready);
	}

	public void SetReady(bool _ready) {
		ready.gameObject.SetActive (_ready);
		if (_ready) {
			background.color = Color.green;
		} else {
			background.color = Color.red;
		}
	}

	public override void OnClientEnterLobby() {

		base.OnClientEnterLobby ();

		if (LobbyListScene.Instance == null)
			return;
		
		LobbyListScene.Instance.SetPlayerTagParent (this);

		playerNameText.text = "Player " + netId;

		SetReady (false);
	}

	public override void OnStartLocalPlayer() {
		base.OnStartLocalPlayer();
		LobbyListScene.Instance.SetLocalPlayer (this);
		OnMyName(PlayerPrefs.GetString ("player_name","Player " + netId));
	}

	//----------
	public void OnMyName(string _name) {
		playerName = _name;
		playerNameText.text = _name;
	}
}