using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextUpdater : MonoBehaviour {

	[SerializeField] private Text[] textToSet = null;

	/// <summary>
	/// Sets text at index to value, if index negative, sets all text
	/// </summary>
	/// <param name="_index">_index.</param>
	public void SetText(string _value, int _index = -1) {

		if (_index < 0) {
			foreach (Text t in textToSet) {
				t.text = _value;
			}
		} else {
			textToSet[_index].text = _value;
		}
	}
}
