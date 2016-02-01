using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Building {
	
	private List<byte> floors = new List<byte>();

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
	public byte AddFloor(byte _newFloor) {
		if (floors.Count > 1) {//if we have a previous floor, subtract it
			_newFloor = (byte)(_newFloor & floors [floors.Count - 1]);
		}
		if (_newFloor > 0) {
			floors.Add (_newFloor);
		}
		return _newFloor;
	}
}
