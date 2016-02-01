using UnityEngine;
using System.Collections;
using DG.Tweening;

public class FloorBuilder : MonoBehaviour {

	
	[SerializeField] private GameObject buildingBlock = null;
	[SerializeField] private Transform buildingOrigin = null;
	[SerializeField] private TextUpdater scoreText = null;
	[SerializeField] private AudioClip fallSound = null;
	[SerializeField] private AudioClip landSound = null;
	private AudioSource audioSource = null;
		
	private Building building = new Building();

	private Color buildingColor = Color.white;

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

		audioSource = GetComponent<AudioSource> ();
	}

	public void SetColor(Color _color) {
		buildingColor = _color;
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
				Apartment apartment = apartmentGmo.GetComponent<Apartment> ();
				apartment.PlaceApartment (fallPosition, true);
				apartment.UpdateColors (buildingColor);

				audioSource.clip = fallSound;
				audioSource.Play ();

				Camera.main.GetComponent<GameCamera> ().Shake (0.1f,0.15f);
				StartCoroutine (IPlayLandSound());
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

	IEnumerator IPlayLandSound() {
		yield return new WaitForSeconds (0.15f);
		audioSource.clip = landSound;
		audioSource.Play ();
	}



}
