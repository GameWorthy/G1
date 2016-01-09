using UnityEngine;
using System.Collections;
using DG.Tweening;

public class GameMusic : MonoBehaviour {

	public enum MusicType {
		MENU,
		GAME,
		NONE
	}

	[SerializeField] private AudioClip menuMusic = null;
	[SerializeField] private AudioClip gameMusic = null;
	[SerializeField] private AudioSource audioSource = null;
	private MusicType currentlyPlaying = MusicType.NONE;

	public void PlayMusic(MusicType _type) {
		if (currentlyPlaying == _type) {
			return;
		} else {
			currentlyPlaying = _type;
			switch(currentlyPlaying) {
				case MusicType.GAME:
					FadeToMusic(gameMusic);
					break;
				case MusicType.MENU:
					FadeToMusic(menuMusic);
					break;
			}
		}
	}

	void FadeToMusic(AudioClip _clipToPlay) {
		StartCoroutine (IFadeToMusic (_clipToPlay));
	}

	IEnumerator IFadeToMusic(AudioClip _clipToPlay) {
		audioSource.DOFade (0, 1);
		yield return new WaitForSeconds(1);
		audioSource.clip = _clipToPlay;
		audioSource.Play ();
		audioSource.DOFade (1, 1);
	}

}