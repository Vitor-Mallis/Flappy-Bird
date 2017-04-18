using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour {

	public static GameplayController instance;

	[SerializeField]
	GameObject pausePanel, gameOverText, pauseButton, instructions;

	[SerializeField]
	Image medalImage;

	[SerializeField]
	Text scoreText, endScoreText, highScoreText;

	[SerializeField]
	Button resumeButton;

	[SerializeField]
	Sprite[] medals;

	[SerializeField]
	GameObject[] birds;

	void Awake() {
		CreateInstance ();
		Time.timeScale = 0f;
	}

	void CreateInstance() {
		if (instance == null)
			instance = this;
	}

	public void StartGame() {
		Time.timeScale = 1f;
		pauseButton.SetActive (true);
		instructions.SetActive (false);

		birds [GameController.instance.GetSelectedBird ()].SetActive (true);
	}

	public void Pause() {
		Time.timeScale = 0f;
		pausePanel.SetActive (true);
		gameOverText.SetActive (false);
		scoreText.gameObject.SetActive (false);
		pauseButton.SetActive (false);

		endScoreText.text = scoreText.text;
		highScoreText.text = GameController.instance.GetHighscore ().ToString ();

		resumeButton.onClick.RemoveAllListeners ();
		resumeButton.onClick.AddListener (() => Resume());
	}

	public void GameOver(int score) {
		Time.timeScale = 0f;
		pausePanel.SetActive (true);
		gameOverText.SetActive (true);
		scoreText.gameObject.SetActive (false);
		pauseButton.SetActive (false);

		endScoreText.text = score.ToString ();

		if (score <= 20) {
			medalImage.sprite = medals [0];
		} else if (score > 20 && score <= 40) {
			medalImage.sprite = medals [1];

			if (GameController.instance.IsGreenBirdUnlocked () == 0)
				GameController.instance.UnlockGreenBird ();
		} else {
			medalImage.sprite = medals [2];

			if (GameController.instance.IsGreenBirdUnlocked () == 0)
				GameController.instance.UnlockGreenBird ();

			if (GameController.instance.IsRedBirdUnlocked () == 0)
				GameController.instance.UnlockRedBird ();
		}

		if (score > GameController.instance.GetHighscore ())
			GameController.instance.SetHighscore (score);

		highScoreText.text = GameController.instance.GetHighscore ().ToString ();

		resumeButton.onClick.RemoveAllListeners ();
		resumeButton.onClick.AddListener (() => Restart());
	}

	public void Resume() {
		Time.timeScale = 1f;
		pausePanel.SetActive (false);
		pauseButton.SetActive (true);
	}

	public void Restart() {
		SceneFader.instance.ChangeScene ("Gameplay");
	}

	public void MainMenu() {
		Time.timeScale = 1f;
		SceneFader.instance.ChangeScene ("MainMenu");
	}

	public void SetScore(int score) {
		scoreText.text = score.ToString ();
	}
}
