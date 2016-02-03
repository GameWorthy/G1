using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class SinglePlayerScene : MonoBehaviour {

	[SerializeField] private BuildingBrush buildingBrush = null;
	[SerializeField] private ScoreBar scoreBar = null;
	[SerializeField] private ScoreBar highScoreBar = null;
	[SerializeField] private Renderer[] renderers = null;
	[SerializeField] private Text highScoreText = null;
	[SerializeField] private Button playButton = null;
	[SerializeField] private ParticleSystem explosionParticle = null;
	[SerializeField] private AudioClip highScoreSound = null;

	private AudioSource audioSource = null;

	private float yOffset = -3.5f;
	private int height = 0;

	void Start () {

		buildingBrush.onLoseAllLifes += OnGameOver;
		buildingBrush.onPlaceFloor += OnPlaceFloor;

		scoreBar.SetScore (0);
		highScoreBar.SetScore (0);

		highScoreBar.SetTarget (GameHelper.BuildingToWorldSpace (0, Game.Instance.HighScore).y + yOffset);
		explosionParticle.transform.position = GameHelper.BuildingToWorldSpace (0, Game.Instance.HighScore) + Vector2.up * yOffset;

		highScoreText.text = "";
		playButton.gameObject.SetActive (false);

		foreach (Renderer r in renderers) {

			if (r is SpriteRenderer) {
				((SpriteRenderer)r).color = Game.Instance.PlayerColor;
			} else {
				r.material.color = Game.Instance.PlayerColor;
			}
		}

		highScoreText.color = Game.Instance.PlayerColor;

		audioSource = GetComponent<AudioSource> ();

		Game.Instance.PlayGameMusic (true);
	}

	void OnGameOver() {
		
		playButton.gameObject.SetActive (true);

		if (height > Game.Instance.HighScore) {
			Game.Instance.HighScore = height;
			Bounce (highScoreText.transform, 10);
		}

		highScoreText.text = "Highest Score:\n" + Game.Instance.HighScore;

		Game.Instance.Save ();

	}

	private bool bouncing = false;
	void OnPlaceFloor(float _height) {
		height = (int)_height;
		scoreBar.SetTarget (GameHelper.BuildingToWorldSpace (0, height).y + yOffset);
		scoreBar.SetScore (height);

		if (!bouncing && height > Game.Instance.HighScore) {
			bouncing = true;
			Bounce (scoreBar.transform.GetChild(0), 0.3f);

			explosionParticle.Simulate(0.0f,true,true);
			ParticleSystem.EmissionModule emission = explosionParticle.emission;
			emission.enabled = true;
			explosionParticle.Play ();
			highScoreBar.gameObject.SetActive (false);

			audioSource.clip = highScoreSound;
			audioSource.Play ();
		}
	}

	void OnDestroy() {
		buildingBrush.onLoseAllLifes += OnGameOver;
		buildingBrush.onPlaceFloor += OnPlaceFloor;
	}

	void Bounce(Transform _t, float _amount) {
		_t.DOLocalJump (transform.localPosition, _amount, 999, 999);
	}

}