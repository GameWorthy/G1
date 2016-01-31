using UnityEngine;
using System.Collections;
using DG.Tweening;

public class FloorBuilder : MonoBehaviour {

	
	[SerializeField] private GameObject buildingBlock = null;
	[SerializeField] private Transform buildingOrigin = null;
	[SerializeField] private TextUpdater scoreText = null;
	private Building building = new Building();

	public Building Building {
		get {return building;}
		private set {building = value;}
	}

	public float CurrentHeight {
		get;
		set;
	}

	void Start() {
		if (scoreText != null) {
			scoreText.SetText(building.TotalFloors.ToString());
		}
	}

	public int BuildFloor(byte[] _newFloor) {
		//keep in mind that the newFloor parameter will be updated as it is a reference
		building.AddFloor (_newFloor);
		
		//Adding the visuals
		for (int i = 0; i < _newFloor.Length; i++) {
			if(_newFloor[i] == 1) {
				Vector2 fallPosition = GameHelper.BuildingToWorldSpace(i,building.TotalFloors - 1,-Game.MAX_COLUMNS * 0.5f);
				GameObject apartmentGmo = Instantiate(buildingBlock) as GameObject;
				apartmentGmo.transform.parent = buildingOrigin;

				//TODO: True to be replaced with animation
				//Once networking is in, this will be toggled
				apartmentGmo.GetComponent<Apartment> ().PlaceApartment (fallPosition, true);
				Camera.main.GetComponent<GameCamera> ().Shake (0.1f,0.15f);
			}
		}

		int placedFloors = 0;
		foreach (byte b in _newFloor) {
			placedFloors += b;
		}

		CurrentHeight = Building.TotalFloors + transform.position.y;

		if (scoreText != null) {
			scoreText.SetText(building.TotalFloors.ToString());
		}

		return placedFloors;
	}



}
