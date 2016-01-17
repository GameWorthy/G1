using UnityEngine;
using System.Collections;

public class BuildingBrush : MonoBehaviour {


	private const byte MAX_COLUMNS = 5;
	
	[SerializeField] private float startingTick = 0.5f;
	[SerializeField] private float tick = 0.5f;
	[SerializeField] private GameObject brushBlock = null;
	[SerializeField] private GameObject buildingBlock = null;
	[SerializeField] Transform buildingOrigin = null;

	//no more than 255 lives...
	[SerializeField] private byte lives = 3;

	public byte Lives {
		get {return lives;}
		set {lives = value; UpdateLives();}
	}

	private Building building = new Building();

	private float currentTick = 999f;
	private int dir = 1;
	private int currentIndex = 0;
	
	void Start() {
		tick = startingTick;
		InputReader.onTap += AddFloor;
		UpdateLives ();
	}
	
	void Update () {
		currentTick += Time.deltaTime;
		if (currentTick > tick) {
			currentTick = 0;
			
			if(currentIndex + Lives >= MAX_COLUMNS) {
				dir = -1;
			}
			else if(currentIndex < 1){
				dir = 1;
			}
			
			currentIndex += dir;
			transform.position = GameHelper.BuildingToWorldSpace(currentIndex,building.TotalFloors,-MAX_COLUMNS * 0.5f);
		}
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
			brushGmo.transform.position = position;
		}
	}

	
	void AddFloor() {

		//translate our position into a floor
		byte[] newFloor = new byte[MAX_COLUMNS];//0,0,0,0,0
		int lifeDecrease = lives;
		int indexIncrease = currentIndex;
		while (lifeDecrease > 0) {
			lifeDecrease--;
			newFloor[indexIncrease] = 1;
			indexIncrease++;
		}

		//adding the floor to the building
		//keep in mind that the newFloor parameter will be updated as it is a reference
		building.AddFloor (newFloor);
		//building.LogFloors();


		//Adding the visuals
		for (int i = 0; i < newFloor.Length; i++) {
			if(newFloor[i] == 1) {
				Vector2 fallPosition = GameHelper.BuildingToWorldSpace(i,building.TotalFloors - 1,-MAX_COLUMNS * 0.5f);
				GameObject apartmentGmo = Instantiate(buildingBlock) as GameObject;
				apartmentGmo.transform.parent = buildingOrigin;
				apartmentGmo.transform.localPosition = fallPosition;
			}
		}
	}

}
