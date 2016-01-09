using UnityEngine;
using System.Collections;

public class UISound : MonoBehaviour {

	public enum SoundTypes{
		CLICK,
		HIGH_SCORE
	}

	[SerializeField] private AudioClip clickSound;
	[SerializeField] private AudioClip highScore;
	[SerializeField] private AudioSource audioSource = null;

	public static UISound Instance = null;


	void Awake () {
		Instance = this;
	}

	public void PlaySound() {
		PlaySound (SoundTypes.CLICK);
	}

	public void PlaySound(SoundTypes _audioClip, float _volume = 1f) {

		audioSource.volume = _volume;

		switch (_audioClip) {
			case SoundTypes.CLICK:
				audioSource.clip = clickSound;
				break;
			case SoundTypes.HIGH_SCORE:
			audioSource.clip = highScore;
				break;
		}

		audioSource.Play ();
	}
}
