using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StoreScene : MonoBehaviour {

	[SerializeField] RectTransform colorsParent;
	[SerializeField] RectTransform checkMark;

	void Start() {
		if (Game.Instance.PlayerColor.Equals (Color.black)) {
			OnUpdateColor (colorsParent.GetChild (0).GetComponent<Button> ());
		} else {
			//find the starting color
			foreach (RectTransform child in colorsParent) {
				Image img = child.GetComponent<Image> ();
				if (img.color.Equals (Game.Instance.PlayerColor)) {
					OnUpdateColor (child.GetComponent<Button>());
				}
			}
		}
	}

	public void OnUpdateColor(Button _b) {
		Game.Instance.PlayerColor = _b.GetComponent<Image> ().color;
		checkMark.SetParent (_b.transform);
		checkMark.localPosition = Vector2.zero;
		Game.Instance.Save ();
	}
}
