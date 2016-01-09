using UnityEngine;
using System.Collections;

namespace GameWorthy {

	public class GameWorthySplashScreen : MonoBehaviour {

		[SerializeField] private int sceneToLoad = 1;
		[SerializeField] private Renderer bg = null;

		public void LoadNextScene() {
			Application.LoadLevel (sceneToLoad);
			bg.gameObject.SetActive (false);
		}
	}

}