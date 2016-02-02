using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class SinglePlayerScene : MonoBehaviour {

	[SerializeField] private BuildingBrush buildingBrush = null;
	[SerializeField] private ScoreBar scoreBar = null;
	[SerializeField] private Renderer[] renderers = null;
	[SerializeField] private Text highScoreText = null;
	[SerializeField] private Button playButton = null;

	private float yOffset = -3.5f;
	private int height = 0;

	void Start () {
		buildingBrush.onLoseAllLifes += OnGameOver;
		buildingBrush.onPlaceFloor += OnPlaceFloor;
		scoreBar.SetScore (0);

		highScoreText.text = "";
		playButton.gameObject.SetActive (false);

		foreach (Renderer r in renderers) {
			if (r is SpriteRenderer) {
				((SpriteRenderer)r).color = Game.Instance.PlayerColor;
			} else {
				r.material.color = Game.Instance.PlayerColor;
			}
		}

		Game.Instance.PlayGameMusic (true);
	}

	void OnGameOver() {
		
		playButton.gameObject.SetActive (true);

		if (height > Game.Instance.HighScore) {
			Game.Instance.HighScore = height;
			highScoreText.transform.DOShakePosition (999,2,3);
			highScoreText.transform.DOJump (transform.position, 0.3f, 999, 999);
		}

		highScoreText.text = "Highest Score:\n" + Game.Instance.HighScore;

		Game.Instance.Save ();

	}
		
	void OnPlaceFloor(float _height) {
		height = (int)_height;
		scoreBar.SetTarget (GameHelper.BuildingToWorldSpace (0, height).y + yOffset);
		scoreBar.SetScore (height);
	}

	void OnDestroy() {
		buildingBrush.onLoseAllLifes += OnGameOver;
		buildingBrush.onPlaceFloor += OnPlaceFloor;
	}
}
