using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {

	[SerializeField] float followSpeed = 20;
	[SerializeField] int minHeightToFollow = 4;
	[SerializeField] private float offset = 4;
	[SerializeField] FloorBuilder floorBuilder;

	void Update () {
		if (floorBuilder == null || floorBuilder.Building.TotalFloors < minHeightToFollow) {
			return;
		}

		transform.position = Vector3.Lerp (
			transform.position,
			new Vector3 (transform.position.x,floorBuilder.CurrentHeight + offset,transform.position.z),
			Time.deltaTime * followSpeed);
	}
}
