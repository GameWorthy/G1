using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SettingsScene : MonoBehaviour {

	[SerializeField] private Text soundText = null;

	void Start () {
		SetSoundText (Game.Instance.Muted);
	}

	public void ToggleSound() {
		Game.Instance.ToggleSound ();
		SetSoundText (Game.Instance.Muted);
		Game.Instance.Save ();
	}

	void SetSoundText(bool _on) {
		soundText.text = "SOUND: " + (_on ? "OFF" : "ON");
	}
}
