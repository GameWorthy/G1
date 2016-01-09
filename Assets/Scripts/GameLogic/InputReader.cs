using UnityEngine;
using System.Collections;

public class InputReader : MonoBehaviour {
	private float fingerStartTime  = 0.0f;
	private Vector2 fingerStartPos = Vector2.zero;
	
	private bool isSwipe = false;
	private float minSwipeDist  = 100f;
	private float maxSwipeDist = 300;
	private float maxSwipeTime = 0.5f;

	void Start() {
		maxSwipeDist = Screen.width * 0.15f;//15% of the width
	}

	void Update () {

		if (Input.touchCount > 0) {

			Touch touch = Input.GetTouch (0);
			float gestureTime = isSwipe ? Time.time - fingerStartTime : 0;
			float gestureDist = (touch.position - fingerStartPos).magnitude;

			if(isSwipe) {
				//check for swiping timing out
				if(gestureTime > maxSwipeTime) CanceledSwipe(touch.position);
				//check for reaching max swiping distance
				if(gestureDist >= maxSwipeDist) CompleteSwipe(fingerStartPos, touch.position);
			}
			
			switch (touch.phase) {

				case TouchPhase.Began :
					isSwipe = true;
					fingerStartTime = Time.time;
					fingerStartPos = touch.position;
					onSwipeStart.Invoke(fingerStartPos);
					break;

				case TouchPhase.Moved:
					if(isSwipe) onSwipeMove.Invoke(touch.position);
					break;

				case TouchPhase.Canceled :
					CanceledSwipe(touch.position);
					break;

				case TouchPhase.Ended :
					if(!isSwipe) break;

					if (gestureDist > minSwipeDist) {
						CompleteSwipe(fingerStartPos, touch.position);
					}
					else {
						CanceledSwipe(touch.position);
					}
					break;
			}
		}
	}

	private void CanceledSwipe(Vector2 position) {
		isSwipe = false;
		onSwipeCancel.Invoke(position);
	}

	private void CompleteSwipe(Vector2 _startPos, Vector2 _finalPosition) {
		isSwipe = false;
		onSwipeComplete.Invoke(_finalPosition - _startPos);
	}

	//EVENTS

	/// <summary>
	/// When swipe started
	/// </summary>
	public static d_OnSwipeStart onSwipeStart = (Vector2 a) => {};
	public delegate void d_OnSwipeStart(Vector2 _position);

	/// <summary>
	/// When swipe moves
	/// </summary>
	public static d_OnSwiped onSwipeMove = (Vector2 a) => {};
	public delegate void d_OnSwipeMove(Vector2 _position);

	/// <summary>
	/// After a sucessfull swipe
	/// </summary>
	public static d_OnSwiped onSwipeComplete = (Vector2 a) => {};
	public delegate void d_OnSwiped(Vector2 _direction);

	/// <summary>
	/// When swipe finished or was cancelled
	/// </summary>
	public static d_OnSwipeCancelled onSwipeCancel = (Vector2 a) => {};
	public delegate void d_OnSwipeCancelled(Vector2 _position);
}