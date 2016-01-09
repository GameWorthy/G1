using UnityEngine;
using System.Collections;

public class RenderLayer : MonoBehaviour {

	[SerializeField] private string layerName = "";
	[SerializeField] private int orderInLayer = 0;
	void Start () {
		Renderer rend = GetComponent<Renderer> ();
		if (rend != null) {
			rend.sortingLayerName = layerName;
			rend.sortingOrder = orderInLayer;
		}
	}
}
