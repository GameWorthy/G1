using UnityEngine;
using System.Collections;

public class Temporary : MonoBehaviour {

	[SerializeField] private float lifespan = 5f;
	
	void Start () {
		Destroy (gameObject, lifespan);	
	}
}
