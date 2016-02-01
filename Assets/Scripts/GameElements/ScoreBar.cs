using UnityEngine;
using System.Collections;

public class ScoreBar : MonoBehaviour {

	[SerializeField] private TextMesh text = null;
	[SerializeField] private float yTarget = -4f;

	public void SetScore(int score) {

		if (score == 0) {
			text.text = "";
		} else {
			text.text = score.ToString ();
		}
	}

	public void SetTarget(float _target) {
		yTarget = _target;
	}

	void Update () {
		transform.position = Vector3.Lerp (
			transform.position,
			new Vector3 (0,yTarget ,transform.position.z),
			Time.deltaTime * 20);
	}
}
