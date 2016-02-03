using UnityEngine;
using System.Collections;

namespace GameWorthy {
	
	public class TextureOffseter : MonoBehaviour {

		[SerializeField] private Renderer bg = null;
		[SerializeField] private float speed = 0.5f;
		[SerializeField] private Vector2 direction = Vector2.one;
		
		void Update () {
			float off = Time.time * speed;
			bg.material.SetTextureOffset ("_MainTex", new Vector2(direction.x * off,direction.y * off));
		}

		public void SetDirection(Vector2 _dir) {
			direction = _dir;
		}

	}
	
}