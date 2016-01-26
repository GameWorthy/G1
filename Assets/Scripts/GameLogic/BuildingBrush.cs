using UnityEngine;
using System.Collections;

public class BuildingBrush : MonoBehaviour {
	

	[SerializeField] private float startingTick = 0.5f;
	[SerializeField] private float tick = 0.5f;
	[SerializeField] private GameObject brushBlock = null;
	[SerializeField] private FloorBuilder floorBuilder = null;

	//no more than 255 lives...
	[SerializeField] private byte lives = 3;

	public byte Lives {
		get {return lives;}
		set {lives = value; UpdateLives();}
	}
	
	private float currentTick = 999f;
	private int dir = 1;
	private int currentIndex = 0;
	private Vector2 startingOffset = Vector2.zero;
	
	void Start() {
		tick = startingTick;
		InputReader.onTap += AddFloor;
		startingOffset = transform.position;
		UpdateLives ();
	}
	
	void Update () {
		currentTick += Time.deltaTime;
		if (currentTick > tick) {
			currentTick = 0;
			
			if(currentIndex + Lives >= Game.MAX_COLUMNS) {
				dir = -1;
			}
			else if(currentIndex < 1){
				dir = 1;
			}
			
			currentIndex += dir;
			SetBrushPosition();
		}
	}

	void SetBrushPosition() {
		transform.position = GameHelper.BuildingToWorldSpace(currentIndex,floorBuilder.Building.TotalFloors,-Game.MAX_COLUMNS * 0.5f) + startingOffset;
	}

	//TODO: Refactor this so it doens't kill and instantiate everything
	void UpdateLives() {

		for (int i = 0; i < gameObject.transform.childCount; i++) {
			Destroy(transform.GetChild(i).gameObject);
		}

		for(byte i = 0; i < lives; i++) {
			Vector2 position = GameHelper.BuildingToWorldSpace(i,0,-0.5f);
			GameObject brushGmo = Instantiate(brushBlock) as GameObject;
			brushGmo.transform.parent = transform;
			brushGmo.transform.localPosition = position;
		}
	}

	
	void AddFloor() {

		if (Lives <= 0) {
			return;
		}

		tick *= 0.85f;

		//translate our position into a floor
		byte[] newFloor = new byte[Game.MAX_COLUMNS];//0,0,0,0,0
		int lifeDecrease = lives;
		int indexIncrease = currentIndex;
		while (lifeDecrease > 0) {
			lifeDecrease--;
			newFloor[indexIncrease] = 1;
			indexIncrease++;
		}

		//adding the floor to the building trough the floor builder
		Lives = (byte)floorBuilder.BuildFloor (newFloor);

		currentIndex = Random.value > 0.5f ? 0 : (int)Game.MAX_COLUMNS - Lives;
		currentTick = 0;
		SetBrushPosition ();

		if (lives <= 0) {
			StartCoroutine(PLACEHOLDERRESTART());
		}
	}

	void OnDestroy() {
		InputReader.onTap -= AddFloor;
	}

	IEnumerator PLACEHOLDERRESTART() {
		yield return new WaitForSeconds (2);
		LevelLoader.Instance.LoadScene ("GameSceneSingleplayer");
	}
	

	void SetBrushEnable(bool _value) {
		Debug.Log (transform.childCount);
		for (int i = 0; i < transform.childCount; i++) {
			transform.GetChild(i).gameObject.SetActive(_value);
		}
	}

}
