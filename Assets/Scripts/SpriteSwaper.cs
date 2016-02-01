using UnityEngine;
using System.Collections;

public class SpriteSwaper : MonoBehaviour {

	[SerializeField] private Sprite[] sprites;
	[SerializeField] private SpriteRenderer sr;

	void Update () {
		sr.sprite = sprites[(int)(Game.Instance.Tick % sprites.Length)];
	}
}
