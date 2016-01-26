using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JoinLobbyButton : MonoBehaviour {
	[SerializeField] private Text lobbyName;
	[SerializeField] private Text players;

	public int ID {
		get;
		set;
	}

	public string LobbyName {
		get { return lobbyName.text; }
		set { lobbyName.text = value; }
	}


}
