using UnityEngine;
using System.Collections;
using DG.Tweening;

public class FloorBuilder : MonoBehaviour {

	[SerializeField] private GameObject buildingBlock = null;
	[SerializeField] private Transform buildingOrigin = null;
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
		audioSource = GetComponent<AudioSource> ();
	}

	public void SetColor(Color _color) {
		buildingColor = _color;
	}

	public byte BuildFloor(byte _newFloor) {
		//keep in mind that the newFloor parameter will be updated as it is a reference
		_newFloor = building.AddFloor (_newFloor);
	
		//Adding the visuals
		for (int i = 0; i < Game.MAX_COLUMNS; i++) {
			if(GameHelper.IsBitOn(_newFloor,i)) {

				//a byte is left to right, we read right to left.
				//so we have to invert i by doing this: Game.MAX_COLUMNS - 1 - i
				Vector2 fallPosition = GameHelper.BuildingToWorldSpace(Game.MAX_COLUMNS - 1 - i,building.TotalFloors - 1,-Game.MAX_COLUMNS * 0.5f);
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

		CurrentHeight = Building.TotalFloors + transform.position.y;
			
		return _newFloor;
	}

	IEnumerator IPlayLandSound() {
		yield return new WaitForSeconds (0.15f);
		audioSource.clip = landSound;
		audioSource.Play ();
	}

}