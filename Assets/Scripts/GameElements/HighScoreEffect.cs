using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class HighScoreEffect : MonoBehaviour {
	
	[SerializeField] private ParticleSystem ps = null;
	[SerializeField] private Outline outline = null;
	private bool active = false;
	private float timer = 0;


	void Start() {
		if (outline != null) {
			outline.effectColor = new Color (0, 0, 0, 0);
		}
	}

	public void Activate() {
		active = true;
		if (ps != null) {
			ps.Play ();
		}
		transform.DOShakePosition (999,2,3);
		transform.DOJump (transform.position, 0.3f, 999, 999);
	}

	void Update() {
		if (active) {
			if(ps != null) {
				ps.startColor = new Color(Random.Range(0.5f,1f),Random.Range(0.5f,1f),Random.Range(0.5f,1f),1.0f);
			}

			timer += Time.deltaTime;
			if (timer >= 0.1f){
				if(outline != null) {
					outline.effectColor = new Color(Random.Range(0.5f,1f),Random.Range(0.5f,1f),Random.Range(0.5f,1f),1.0f);
				}
			}
		}
	}

}
