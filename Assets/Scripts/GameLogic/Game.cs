using UnityEngine;
using System;
using System.Collections;
using DG.Tweening;
using GameWorthy;


public class Game : MonoBehaviour {
	
	public const byte MAX_COLUMNS = 8;

	public static Game Instance = null;
	
	private GameData gameData = null;

	[SerializeField] private GameMusic gameMusic = null;

	public bool Muted {
		get {return gameData.muted;}
		set {gameData.muted = value;}
	}

	public int SelectedBronto {
		get {return gameData.selectedSkin;}
		set {gameData.selectedSkin = value;}
	}

	public int LastUnlockedBrontoSkin {
		get {return gameData.lastUnlockedSkin;}
		private set {gameData.lastUnlockedSkin = value;}
	}

	void Awake() {
		DontDestroyOnLoad (gameObject);
		if (Instance == null) {
			Instance = this;
			GameStart();
		} else {
			Destroy (gameObject);
		}

	}
	
	void GameStart() {
		//Application.targetFrameRate = 60;
		Screen.orientation = ScreenOrientation.Portrait;

		MemoryCard.Initiate ();
		gameData = (GameData) MemoryCard.Load ("game");

		if (gameData == null) {
			gameData = new GameData();
		}

		ApplySoundSettings ();

		//HACK: find a better way to handle on game loading event
		PlayGameMusic (Application.loadedLevel == 2);
	}

	void OnLevelWasLoaded(int level) {
		if (!IsInstance()) return;
		ApplySoundSettings ();

		//HACK: find a better way to handle on game loading event
		PlayGameMusic (level == 2);

	}

	void PlayGameMusic(bool playGame) {
		if (playGame) {
			gameMusic.PlayMusic (GameMusic.MusicType.GAME);
		} else {
			gameMusic.PlayMusic (GameMusic.MusicType.MENU);
		}
	}
	
	void Update() {

		if (Input.GetKeyDown (KeyCode.R)) {
			ResetGameData();
		}
	}

	public void Save() {
		MemoryCard.Save (gameData, "game");
	}

	public bool OwnSkin(int _id) {
		foreach (int i in gameData.ownedSkins) {
			if(i == _id) {
				return true;
			}
		}
		return false;
	}

	public void ResetGameData() {
		PlayerPrefs.SetInt("firstTimeLoadingGameScene",0);
		MemoryCard.Save (new GameData(), "game");
		gameData = (GameData) MemoryCard.Load ("game");
	}

	/// <summary>
	/// Unlocks a skin by a given ID. DOES NOT SAVE AFTER UNLOCKING!
	/// </summary>
	/// <param name="_id">_id.</param>
	public void UnlockSkin (int _id) {
		int currentOwnedTotal = gameData.ownedSkins.Length;
		int[] newOwned = new int[currentOwnedTotal + 1];
		for (int i=0; i < currentOwnedTotal; i++) {
			newOwned[i] = gameData.ownedSkins[i];
		}
		newOwned [currentOwnedTotal] = _id;
		gameData.ownedSkins = newOwned;
	}

	public void OnGameOver(int _achievedHeight, int _playedBrontoID) {

		//Highscore
		//bool isHighScore = false;
		if (_achievedHeight > gameData.highScore) {
			gameData.highScore = _achievedHeight;
			//isHighScore = true;
		}

		Save ();
	}

	public void ToggleSound () {
		Muted = !Muted;
		ApplySoundSettings ();
	}

	void ApplySoundSettings() {
		AudioListener.volume = Muted ? 0 : 1;
	}

	bool IsInstance() {
		return Instance != null && Instance.Equals(this);
	}
}

[Serializable]
public class GameData {
	public int[] ownedSkins = new int[]{0};
	public int selectedSkin = 0;
	public int highScore = 0;
	public int lastUnlockedSkin = 0;
	public bool muted = false;
}

