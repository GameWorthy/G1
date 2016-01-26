using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerTag : MonoBehaviour {
	[SerializeField] private Text playerName;
	[SerializeField] private Text players;
	[SerializeField] private Image background;
	[SerializeField] private Image host ;


	public int ID {
		get;
		set;
	}

	public string PlayerName {
		get { return playerName.text; }
		set { playerName.text = value; }
	}

	public void ToggleIsHost(bool _val) {
		host.gameObject.SetActive (_val);
	}

	public void SetAsLocalClient() {
		background.color = Color.green;
	}

	public void SetAsServerClient() {
		background.color = Color.blue;
	}

	public void SetInnactive() {
		background.color = Color.grey;
	}
}