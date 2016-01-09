using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GameWorthy;

namespace GameWorthy {
	
	public class Game : MonoBehaviour {
		
		private enum MenuState	{
			OFF,
			MAIN_MENU,
			IN_GAME,
			GAME_OVER,
			SETTINGS,
			MEDALS,
			UPGRADE_SHOP
		}
		
		private MenuState menuState = MenuState.OFF;
		
		[SerializeField] private Menu menu = null;
		[SerializeField] private AudioClip[] gameOverClips = null;
		
		private bool gameInProgress = false;
		private GameData gameData = null;
		private AudioSource audioSource = null;
		private float currentScore = 0;

		public bool GameInProgress {
			get {return gameInProgress;}
			set {gameInProgress = value;}
		}
		
		void Start() {
			
			Application.targetFrameRate = 60;
			
			MemoryCard.Initiate ();
			
			gameData = (GameData) MemoryCard.Load ("game");
			
			if (gameData == null) {
				gameData = new GameData();
			}
			
			Screen.orientation = ScreenOrientation.Portrait;
			
			MedalSystem.Initiate ();
			menu.UpdateMedalsText ();

			audioSource = GetComponent<AudioSource> ();

			menuState = MenuState.MAIN_MENU;
			
			ShowMenu ();
			
		}
		
		void Update() {
			
			menu.SetAnimationState ((int)menuState);

			if (Input.GetKeyDown (KeyCode.R)) {
				//Cheat for reseting game data
			}
		}
		
		public void StartGame() {

			if (gameInProgress)	return;
			
			gameInProgress = true;
			menuState = MenuState.IN_GAME;

			menu.DeactivateHighScore ();
		}

		public void SetGameOver() {

			audioSource.clip = gameOverClips[UnityEngine.Random.Range(0,gameOverClips.Length)];
			audioSource.Play();
			
			gameInProgress = false;

			menuState = MenuState.GAME_OVER;

			if (currentScore > gameData.highScore) {
				gameData.highScore = (int)currentScore;
				menu.ActivateHighScore();
			}

			
			AddMedals ((int)currentScore);
			
			menu.UpdateMedalsText ();
			
			menu.SetHighScore (gameData.highScore);
			menu.SetCurrentScore ((int)currentScore);
			gameData.totalScore += (int)currentScore;
			
			Save();
		}
		
		public void ShowMenu() {
			menuState = MenuState.MAIN_MENU;
		}
		
		public void ShowSettings() {
			menuState = MenuState.SETTINGS;
		}
		
		public void ShowMedals() {
			menuState = MenuState.MEDALS;
		}
		
		public void ShowUpgradeShop() {
			menuState = MenuState.UPGRADE_SHOP;
		}
		
		public void Save() {
			MemoryCard.Save (gameData, "game");
		}
		
		public void AddMedals(int _score, int _quantityToAdd = 1) {
			//give medals
			//TODO: Find a better way to initialise medals
			if (_score >= 600) {//Platinum
				MedalSystem.TotalPlatinum += _quantityToAdd;
				menu.SetMedalColor (new Color(203f/255f,251f/255f,1,1));
			} else if (_score >= 300) {//Gold
				MedalSystem.TotalGold += _quantityToAdd;
				menu.SetMedalColor (new Color(1,237f/255f,0,1));
			} else if (_score >= 150) {//Silver
				MedalSystem.TotalSilver += _quantityToAdd;
				menu.SetMedalColor (new Color(203f/255f,203f/255f,203f/255f,1));
			} else if (_score >= 50) {//Bronze
				MedalSystem.TotalBronze += _quantityToAdd;
				menu.SetMedalColor (new Color(218f/255f,103f/255f,0f,1));
			} else {//None, hide medal
				menu.SetMedalColor(new Color(0f,0f,0f,0f));
			}
		}
	}
	
	[Serializable]
	public class GameData {
		public int highScore = 0;
		public double totalScore = 0;
		public int coins = 0;
	}

}
