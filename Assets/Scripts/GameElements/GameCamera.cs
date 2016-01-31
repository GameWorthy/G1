using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {

	[SerializeField] float followSpeed = 20;
	[SerializeField] int minHeightToFollow = 4;
	[SerializeField] private float offset = 4;
	[SerializeField] FloorBuilder floorBuilder;
	[SerializeField] private float shakeAmount = 0;

	void Update () {
		if (floorBuilder == null) {
			return;
		}

		float floorLevel = 0;

		if (floorBuilder.Building.TotalFloors >= minHeightToFollow) {
			floorLevel = floorBuilder.CurrentHeight + offset;
		}

		transform.position = Vector3.Lerp (
			transform.position,
			new Vector3 (0,floorLevel ,transform.position.z),
			Time.deltaTime * followSpeed);

		if (shakeAmount > 0) {
			transform.position = new Vector3 (
				transform.position.x + Random.Range(-shakeAmount,shakeAmount),
				transform.position.y + Random.Range(-shakeAmount,shakeAmount),
				transform.position.z
			);
			shakeAmount -= Time.deltaTime * 0.2f;//decay time
		}
	}

	public void Shake(float _amount, float _delay) {
		StartCoroutine (IShake(_amount,_delay));
	}

	IEnumerator IShake(float _amount, float _delay) {
		yield return new WaitForSeconds (_delay);
		shakeAmount = _amount;
	}
}
