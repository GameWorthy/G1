using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Apartment : MonoBehaviour {

	[SerializeField] private float fallTime = 0.15f;
	[SerializeField] private ParticleSystem landParticle = null;

	void Awake() {
		landParticle.Stop ();
	}

	public void PlaceApartment(Vector2 _pos, bool _animateFall) {
		if (_animateFall) {
			transform.localPosition = _pos + Vector2.up * 10;
			StartCoroutine (AnimateFall(_pos));
		} else {
			transform.localPosition = _pos;
		}
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.A)) {
			landParticle.Play ();
		}
	}
	
	IEnumerator AnimateFall(Vector2 _fallPos) {
		transform.localScale = new Vector3(0.2f,1,1);
		transform.DOLocalMove(_fallPos,fallTime).SetEase(Ease.OutSine);
		yield return new WaitForSeconds(fallTime);
		landParticle.Play ();
		transform.localScale = new Vector3 (0.2f,0.2f,1f);
		transform.DOScale (1,0.1f);
		yield return new WaitForSeconds(fallTime);
	}
}
