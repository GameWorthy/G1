using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DebugBob : MonoBehaviour {

	public Text t = null;
	
	void Update () {
		t.text = Screen.width + "x" + Screen.height + "\n" + Screen.currentResolution;	
	}
}
