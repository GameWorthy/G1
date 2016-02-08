using UnityEngine;
using System;
using System.Collections;
using DG.Tweening;
using GameWorthy;
using UnityEngine.SceneManagement;


public class Game : MonoBehaviour {
	
	public const byte MAX_COLUMNS = 8;

	public static Game Instance = null;
	
	private GameData gameData = null;

	[SerializeField] private GameMusic gameMusic = null;

	public bool Muted {
		get {return gameData.muted;}
		set {gameData.muted = value;}
	}

	public Color PlayerColor {
		get {return new Color(gameData.r, gameData.g, gameData.b);}
		set {
			gameData.r = value.r;
			gameData.g = value.g;
			gameData.b = value.b;
			gameData.a = value.a;
		}
	}

	public int HighScore {
		get { return gameData.highScore; }
		set { gameData.highScore = value; }
	}

	public int Tick {
		get;
		set;
	}

	public string LastConnectedHostIp {
		get;
		set;
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
		Application.targetFrameRate = 60;
		Screen.orientation = ScreenOrientation.Portrait;

		MemoryCard.Initiate ();
		gameData = (GameData) MemoryCard.Load ("game");

		if (gameData == null) {
			gameData = new GameData();
		}

		ApplySoundSettings ();
	}

	void OnLevelWasLoaded(int level) {
		if (!IsInstance()) return;
		ApplySoundSettings ();

		PlayGameMusic (0);

	}

	public void PlayGameMusic(int _level) {
		gameMusic.PlayMusic (_level);
	}
	
	void Update() {
		if (Input.GetKeyDown (KeyCode.R)) {
			ResetGameData();
			print ("---RESET----");
		}
	}

	public void Save() {
		MemoryCard.Save (gameData, "game");
	}

	public void ResetGameData() {
		PlayerPrefs.SetInt("firstTimeLoadingGameScene",0);
		MemoryCard.Save (new GameData(), "game");
		gameData = (GameData) MemoryCard.Load ("game");
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
	public float r = 0;
	public float g = 0;
	public float b = 0;
	public float a = 1;
	public int highScore = 0;
	public bool muted = false;
}

