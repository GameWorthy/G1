using UnityEngine;
using System.Collections;

public class SceneBackground : MonoBehaviour {

	[SerializeField] private Texture[] patterns;

	void Start () {

		Renderer rend = GetComponent<Renderer> ();

		rend.material.SetTexture ("_MainTex",patterns[Random.Range(0,patterns.Length)]);
		transform.rotation = Quaternion.Euler (0,0,Random.Range(0,361));

		GameWorthy.TextureOffseter offsetter = GetComponent<GameWorthy.TextureOffseter> ();
		offsetter.SetDirection (new Vector2(Random.Range(-1f,1f),Random.Range(-1f,1f)));
	}
}
