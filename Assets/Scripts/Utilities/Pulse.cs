using UnityEngine;
using System.Collections;
using DG.Tweening;


public class Pulse : MonoBehaviour {

	public bool pulse = true;

	[SerializeField] private Vector3 size = Vector3.zero;
	[SerializeField] private float tweenTime = 0;
	[SerializeField] private float waitTime = 0;

	private Vector3 initialSize = Vector3.zero;

	void Start () {
		StartCoroutine (IPulse ());
		initialSize = transform.localScale;
	}

	IEnumerator IPulse() {
		while (pulse) {
			transform.DOScale (size, tweenTime);
			yield return new WaitForSeconds (tweenTime);
			transform.DOScale (initialSize, tweenTime);
			yield return new WaitForSeconds (tweenTime + waitTime);
		}

		yield return null;

	}
}
