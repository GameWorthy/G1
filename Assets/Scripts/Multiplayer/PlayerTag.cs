using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;

public class PlayerTag : NetworkLobbyPlayer {
	
	[SerializeField] private Text playerName;
	[SerializeField] private Image background;
	[SerializeField] private Image ready;

	public int ID {
		get;
		set;
	}

	public string PlayerName {
		get { return playerName.text; }
		set { playerName.text = value; }
	}

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
		playerName.text = "Player " + this.netId.ToString();

		SetReady (false);
	}

	public override void OnStartLocalPlayer() {
		base.OnStartLocalPlayer();
		LobbyListScene.Instance.SetLocalPlayer (this);
	}
}