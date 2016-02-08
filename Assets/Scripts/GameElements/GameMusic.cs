using UnityEngine;
using System.Collections;
using DG.Tweening;

public class GameMusic : MonoBehaviour {

	//[SerializeField] private AudioSource chords = null;
	[SerializeField] private AudioSource bass = null;
	[SerializeField] private AudioSource drum = null;
	[SerializeField] private AudioSource hats = null;

	public void PlayMusic(int _level) {

		byte bassVolume = 0;
		byte drumVolume = 0;
		byte hatsVolume = 0;

		if (_level > 3) {
			bassVolume = 1;
		}
		if (_level > 8) {
			drumVolume = 1;
		}
		if (_level > 13) {
			hatsVolume = 1;
		}

		bass.DOFade (bassVolume,1);
		drum.DOFade (drumVolume,1);
		hats.DOFade (hatsVolume,1);

	}
}