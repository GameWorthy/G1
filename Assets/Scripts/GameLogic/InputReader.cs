using UnityEngine;
using System.Collections;

public class InputReader : MonoBehaviour {

	private bool firstTouch = true;

	void Update () {

		if (Input.GetKeyDown (KeyCode.Space)) {
			onTap.Invoke();
		}

		if (Input.touchCount > 0) {
			if(firstTouch) {
				firstTouch = false;
				onTap.Invoke ();
			}
		} else {
			firstTouch = true;
		}
	}
	

	//EVENTS

	/// <summary>
	/// When swipe started
	/// </summary>
	public static d_onTap onTap = () => {};
	public delegate void d_onTap();
}