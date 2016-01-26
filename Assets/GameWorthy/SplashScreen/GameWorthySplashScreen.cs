using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace GameWorthy {

	public class GameWorthySplashScreen : MonoBehaviour {

		[SerializeField] private int sceneToLoad = 1;
		[SerializeField] private Renderer bg = null;

		public void LoadNextScene() {
			SceneManager.LoadScene (sceneToLoad);
			bg.gameObject.SetActive (false);
		}
	}

}