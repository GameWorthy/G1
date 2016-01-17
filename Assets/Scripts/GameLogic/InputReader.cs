using UnityEngine;
using System.Collections;

public class InputReader : MonoBehaviour {
	
	void Update () {

		if (Input.GetMouseButtonDown (0)) {
			onTap.Invoke();
		}

		if (Input.touchCount > 0) {
			if (Input.GetTouch (0).phase == TouchPhase.Began) {
				onTap.Invoke();
			}
		}
	}
	

	//EVENTS

	/// <summary>
	/// When swipe started
	/// </summary>
	public static d_onTap onTap = () => {};
	public delegate void d_onTap();
}