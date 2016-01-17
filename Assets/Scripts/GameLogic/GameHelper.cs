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
}

