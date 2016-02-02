using UnityEngine;
using System.Collections;

public class SinglePlayerScene : MonoBehaviour {

	[SerializeField] private BuildingBrush buildingBrush = null;
	[SerializeField] private ScoreBar scoreBar = null;
	[SerializeField] private Renderer[] renderers = null;

	private float yOffset = -3.5f;

	void Start () {
		buildingBrush.onLoseAllLifes += OnGameOver;
		buildingBrush.onPlaceFloor += OnPlaceFloor;
		scoreBar.SetScore (0);

		foreach (Renderer r in renderers) {
			if (r is SpriteRenderer) {
				((SpriteRenderer)r).color = Game.Instance.PlayerColor;
			} else {
				r.material.color = Game.Instance.PlayerColor;
			}
		}
	}

	void OnGameOver() {
		StartCoroutine (IReStart());
	}
		
	void OnPlaceFloor(float _height) {
		scoreBar.SetTarget (GameHelper.BuildingToWorldSpace (0, (int)_height).y + yOffset);
		scoreBar.SetScore ((int)_height);
	}

	IEnumerator IReStart() {
		yield return new WaitForSeconds (2);
		LevelLoader.Instance.LoadScene ("GameSceneSingleplayer");
	}

	void OnDestroy() {
		buildingBrush.onLoseAllLifes += OnGameOver;
		buildingBrush.onPlaceFloor += OnPlaceFloor;
	}
}
