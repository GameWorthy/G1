using UnityEngine;
using System.Collections;

public class FloorBuilder : MonoBehaviour {

	private const int MAX_COLUMNS = 5;

	[SerializeField] private float startingTick = 0.5f;
	[SerializeField] private float tick = 0.5f;
	[SerializeField] private BuildingBrush buildingBrush = null;

	private float currentTick = 0f;

	private int dir = 1;
	private int currentIndex = 0;

	void Start() {
		tick = startingTick;
	}

	void Update () {
		currentTick += Time.deltaTime;
		if (currentTick > tick) {
			currentTick = 0;

			if(currentIndex >= MAX_COLUMNS) {
				dir = -1;
			}
			else if(currentIndex <= 1){
				dir = 1;
			}

			currentIndex += dir;
			buildingBrush.transform.position = new Vector3(currentIndex,0,0);
		}
	}
}
