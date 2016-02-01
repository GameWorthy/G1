using UnityEngine;
using System.Collections;

public class SinglePlayerScene : MonoBehaviour {

	[SerializeField] private BuildingBrush buildingBrush = null;
	[SerializeField] private ScoreBar scoreBar = null;

	private float yOffset = -3.5f;

	void Start () {
		buildingBrush.onLoseAllLifes += OnGameOver;
		buildingBrush.onPlaceFloor += OnPlaceFloor;
		scoreBar.SetScore (0);
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
