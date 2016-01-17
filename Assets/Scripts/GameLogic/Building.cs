using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Building {
	
	private List<byte[]> floors = new List<byte[]>();

	public int TotalFloors {
		get {
			return floors.Count;
		}
	}

	/// <summary>
	/// Returns added floor
	/// </summary>
	/// <returns>The floor.</returns>
	/// <param name="_newFloor">_new floor.</param>
	public byte[] AddFloor(byte[] _newFloor) {
		floors.Add (_newFloor);
		if (floors.Count > 1) {//if we have a previous floor
			SubtractFromFloor(_newFloor, floors[floors.Count - 2]);
		}

		return _newFloor;
	}

	/// <summary>
	/// Subtracts from floor. Subtracts a floor apartments if an apartment is placed on top of an empty apartment.
	/// </summary>
	/// <param name="_floorToSubtract">Updates the reference _floorToSubtract</param>
	/// <param name="_floorToSubtractFrom">_floor to subtract from.</param>
	private void SubtractFromFloor(byte[] _floorToSubtract, byte[] _floorToSubtractFrom) {
		for(int i = 0; i < _floorToSubtract.Length && i < _floorToSubtractFrom.Length; i++) {
			byte a = _floorToSubtract[i];
			byte b = _floorToSubtractFrom[i];
			_floorToSubtract[i] = (byte)(a & b);
		}
	}

	public void LogFloors() {

		for (int floor = 0; floor < floors.Count; floor++) {
			string line = string.Empty;
			line += "Floor " + floor + ": ";
			byte[] floorApartments = floors[floor];
			for(int apartment = 0; apartment < floorApartments.Length; apartment++) {
				line += floorApartments[apartment] + "-";
			}
			Debug.Log (line);
		}
	}
}
