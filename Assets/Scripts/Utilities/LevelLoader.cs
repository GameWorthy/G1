using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

	public static LevelLoader Instance = null;

	[SerializeField] private Camera cam = null;
	[SerializeField] private SpriteRenderer sr = null;
	private float transitionTime = 0.3f;

	void Start() {
		DontDestroyOnLoad (gameObject);
		if (Instance == null) {
			Instance = this;
			LevelLoaderStart ();
		} else {
			Destroy (gameObject);
		}
	}
	
	public void LoadScene(string _name) {
		cam.enabled = true;
		StartCoroutine (ILoad (_name));
	}

	IEnumerator ILoad(string _name) {
		sr.DOColor (Color.black,transitionTime);
		yield return new WaitForSeconds(transitionTime);
		SceneManager.LoadScene (_name);
	}

	void OnLevelWasLoaded(int level) {
		LevelLoaderStart ();
	}

	void LevelLoaderStart() {
		sr.color = Color.black;
		sr.DOColor (new Color(0,0,0,0),transitionTime);
		StartCoroutine (DisableCam ());
	}

	IEnumerator DisableCam() {
		yield return new WaitForSeconds(transitionTime);
		cam.enabled = false;
	}

}
