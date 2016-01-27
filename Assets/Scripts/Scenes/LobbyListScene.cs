using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using UnityEngine.UI;

public class LobbyListScene : NetworkLobbyManager {

	[Header("CLIENT UI")]
	[SerializeField] private GameObject lobbyMenu;
	[SerializeField] private GameObject playerLobbyMenu;
	[SerializeField] private RectTransform playerListTransform;
	[SerializeField] private Text errorText;

	void Start () {
		this.networkAddress = "127.0.0.1";
		this.networkPort = 7755;
		DisplayErrorMessage ("");
	}

	public void ClickedHost() {
		NetworkClient nc = this.StartHost ();
		if (nc == null) {
			DisplayErrorMessage ("A Host is already running on port " + this.networkPort + ". Click join to join the match.");
		}
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

		print ("Host started");
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

	public override GameObject OnLobbyServerCreateLobbyPlayer(NetworkConnection conn, short playerControllerId)
	{
		GameObject obj = Instantiate(lobbyPlayerPrefab.gameObject) as GameObject;

		PlayerTag tag = obj.GetComponent<PlayerTag>();

		tag.transform.SetParent(playerListTransform);
		tag.transform.localScale = Vector3.one;

		return obj;
	}

}
