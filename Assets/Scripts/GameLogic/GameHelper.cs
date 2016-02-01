using UnityEngine;
using System;

public class GameHelper {

	/// <summary>
	/// Buildings To World Space
	/// </summary>
	/// <returns>Convers a building coordinate into Unity game world space.</returns>
	/// <param name="_apartment">_apartment.</param>
	/// <param name="_floor">_floor.</param>
	/// <param name="_horizontalOffset">_horizontal offset.</param>
	/// <param name="_blockSize">_block size.</param>
	public static Vector2 BuildingToWorldSpace(int _apartment, int _floor, float _horizontalOffset = 0f, float _blockSize = 1f) {
		return new Vector2 (
			_apartment * _blockSize + _horizontalOffset + (_blockSize * 0.5f),
			_floor * _blockSize + (_blockSize * 0.5f)
		);
	}

	public static bool IsBitOn(byte b, int bitNumber) {
		return (b & (1 << bitNumber)) != 0;
	}
		
	public static void LogByte(byte _byte) {
		Debug.Log (Convert.ToString(_byte,2).PadLeft(8,'0'));
	}

	public static byte BitsInByte(byte _b) {
		byte _return = 0;
		for(byte i = 0; i < 8; i++) {
			if(IsBitOn(_b,i)) {
				_return++;
			}
		}
		return _return;
	}
}

