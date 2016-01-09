using UnityEngine;
using System.Collections;

namespace GameWorthy {
	
	public class TextureOffseter : MonoBehaviour {

		[SerializeField] private Renderer bg = null;
		[SerializeField] private float speed = 0.5f;
		
		void Update () {
			float off = Time.time * speed;
			bg.material.SetTextureOffset ("_MainTex", new Vector2(off,off));
		}

	}
	
}