using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using UnityEngine.UI;

public class LobbyListScene : NetworkLobbyManager {

	public static LobbyListScene Instance = null;

	[Header("CLIENT UI")]
	[SerializeField] private GameObject lobbyMenu;
	[SerializeField] private GameObject playerLobbyMenu;
	[SerializeField] private RectTransform playerListTransform;
	[SerializeField] private Text errorText;

	private bool isHosting = false;
	private PlayerTag localPlayer = null;

	void OnEnable() {
		Instance = this;
	}

	void Start () {
		//this.networkAddress = "127.0.0.1";
		//this.networkPort = 7755;
		DisplayErrorMessage ("");
		Debug.Log (NetworkManager.singleton.networkAddress);
	}

	public void ClickedHost() {
		NetworkClient nc = this.StartHost ();

		if (nc == null) {
			DisplayErrorMessage ("A Host is already running on port " + this.networkPort + ". Click join to join the match.");
		}
	}

	public void SetLocalPlayer(PlayerTag _tag) {
		localPlayer = _tag;
	}

	public void ClickedReady() {
		if(localPlayer == null) return;
		localPlayer.SendReadyToBeginMessage ();
	}

	public void ClickedNotReady() {
		if(localPlayer == null) return;
		localPlayer.SendNotReadyToBeginMessage ();
	}

	public void ClickedHome() {
		if (isHosting) {
			this.StopHost ();
			isHosting = false;
		}
		Destroy (this.gameObject);
	}

	public void ClickedJoin() {
		StartClient ();
		DisplayErrorMessage ("...Looking for a match...");
	}

	void DisplayErrorMessage(string _m) {
		errorText.text = _m;
	}

	void SetOnPlayerLobbyVisual(bool _b) {
		playerLobbyMenu .SetActive(_b);
		lobbyMenu.SetActive (!_b);
	}



//---------------- SERVER MESSAGES------------------

	public override void OnStartHost()
	{
		base.OnStartHost();
		isHosting = true;
	}

	public override void OnClientConnect(NetworkConnection conn)
	{
		base.OnClientConnect(conn);
		SetOnPlayerLobbyVisual (true);
	}

	public override void OnMatchCreate(UnityEngine.Networking.Match.CreateMatchResponse matchInfo)
	{
		base.OnMatchCreate(matchInfo);
		print ("Match created: " + matchInfo.networkId);
	}

	public void SetPlayerTagParent(PlayerTag _tag) {
		_tag.transform.SetParent(playerListTransform);
		_tag.transform.localScale = Vector3.one;
	}

}
