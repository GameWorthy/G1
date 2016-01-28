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
	[SerializeField] private InputField nameInput;
	[SerializeField] private InputField ipInput;
	[SerializeField] private Text lobbyName;


	private bool isHosting = false;
	private PlayerTag localPlayer = null;

	void OnEnable() {
		Instance = this;
	}

	void Start () {

		DisplayErrorMessage (Network.player.ipAddress + ":" + networkPort);

		nameInput.text = PlayerPrefs.GetString ("player_name","Player1");
		nameInput.onEndEdit.AddListener(delegate{SaveName();});

		if (Game.Instance.LastConnectedHostIp == null) {
			Game.Instance.LastConnectedHostIp = Network.player.ipAddress;
		}
		ipInput.text = Game.Instance.LastConnectedHostIp;
		ipInput.onEndEdit.AddListener (delegate{SaveIp();});
	}

	void SaveName() {
		PlayerPrefs.SetString ("player_name", nameInput.text);
	}

	void SaveIp() {
		this.networkAddress = ipInput.text;
		Game.Instance.LastConnectedHostIp = ipInput.text;
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

	public void ClickedHost() {
		this.networkAddress = ipInput.text;
		NetworkClient nc = this.StartHost ();
		
		if (nc == null) {
			DisplayErrorMessage ("A Host is already running on port " + this.networkPort + ". Click join to join the match.");
		}
	}

	public void ClickedJoin() {
		StartClient ();
		DisplayErrorMessage ("...Looking for a match... at: " + this.networkAddress + ":" + this.networkPort);
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

		lobbyName.text = Game.Instance.LastConnectedHostIp;

		StartCoroutine (DelayedSetingName ());
	}

	IEnumerator DelayedSetingName() {
		yield return new WaitForSeconds (1);
		foreach (NetworkLobbyPlayer nlp in lobbySlots) {
			PlayerTag pTag = (PlayerTag)nlp;
			if (nlp != null) {
				if (pTag.playerName != "") {
					pTag.OnMyName (pTag.playerName);
				}
			}
		}
	}

	public void SetPlayerTagParent(PlayerTag _tag) {
		_tag.transform.SetParent(playerListTransform);
		_tag.transform.localScale = Vector3.one;
	}

}
