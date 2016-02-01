using UnityEngine;
using System.Collections;

public class SceneNavigator : MonoBehaviour {

	public void LoadMenu() {
		LevelLoader.Instance.LoadScene ("MainMenu");
	}

	public void LoadGame() {
		int isFirstTime = PlayerPrefs.GetInt ("firstTimeLoadingGameScene", 0);

		if (isFirstTime > 0) {
			LevelLoader.Instance.LoadScene ("GameSceneSingleplayer");
		} else {
			PlayerPrefs.SetInt("firstTimeLoadingGameScene",1);
			//LoadTutorial();
			LoadSkins();//So player can select a color
		}
	}

	public void LoadSkins() {
		PlayerPrefs.SetInt("firstTimeLoadingGameScene",1);//player already loaded skin
		LevelLoader.Instance.LoadScene ("SkinStore");
	}

	public void LoadGameOver() {
		LevelLoader.Instance.LoadScene ("GameEndScene");
	}

	public void LoadSettings() {
		LevelLoader.Instance.LoadScene ("SettingsScene");
	}

	public void LoadTutorial() {
		LevelLoader.Instance.LoadScene ("TutorialScene");
	}

	public void LoadLobbyList() {
		LevelLoader.Instance.LoadScene ("Lobby");
	}

	public void OpenSupport() {
		Application.OpenURL ("mailto:gameworthyfeedback@gmail.com");
	}
	
	public void LoadFacebook() {
		Game.Instance.Save ();
		Application.OpenURL ("https://www.facebook.com/GameWorthy");
	}

	public void LoadTwitter() {
		Game.Instance.Save ();
		Application.OpenURL ("https://twitter.com/game_worthy");
	}

	public void LoadGameWorthy() {
		Game.Instance.Save ();
		Application.OpenURL ("http://gameworthystudios.com/");
	}

	public void ResetGameData() {
		Game.Instance.ResetGameData ();
	}
}
