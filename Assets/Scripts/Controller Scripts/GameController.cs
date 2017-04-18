using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public static GameController instance;

	const string HIGHSCORE = "Highscore";
	const string SELECTED_BIRD = "Selected Bird";
	const string GREEN_BIRD = "Green Bird";
	const string RED_BIRD = "Red Bird";

	void Awake() {
		CreateSingleton ();
		GameStartedFirstTime ();
	}

	void CreateSingleton() {
		if (instance != null) {
			Destroy (gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad (gameObject);
		}
	}

	void GameStartedFirstTime() {
		if (!PlayerPrefs.HasKey ("GameStartedFirstTime")) {
			PlayerPrefs.SetInt (HIGHSCORE, 0);
			PlayerPrefs.SetInt (SELECTED_BIRD, 0);
			PlayerPrefs.SetInt (GREEN_BIRD, 0);
			PlayerPrefs.SetInt (RED_BIRD, 0);
			PlayerPrefs.SetInt ("GameStartedFirstTime", 0);
		}
	}

	public void SetHighscore(int value) {
		PlayerPrefs.SetInt (HIGHSCORE, value);
	}

	public int GetHighscore() {
		return PlayerPrefs.GetInt (HIGHSCORE);
	}

	public void SetSelectedBird(int value) {
		PlayerPrefs.SetInt (SELECTED_BIRD, value);
	}

	public int GetSelectedBird() {
		return PlayerPrefs.GetInt (SELECTED_BIRD);
	}

	public void UnlockGreenBird() {
		PlayerPrefs.SetInt (GREEN_BIRD, 1);
	}

	public int IsGreenBirdUnlocked() {
		return PlayerPrefs.GetInt (GREEN_BIRD);
	}

	public void UnlockRedBird() {
		PlayerPrefs.SetInt (RED_BIRD, 1);
	}

	public int IsRedBirdUnlocked() {
		return PlayerPrefs.GetInt (RED_BIRD);
	}
}
