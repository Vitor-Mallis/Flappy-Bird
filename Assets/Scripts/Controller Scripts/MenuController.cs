using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

	public static MenuController instance;

	[SerializeField]
	GameObject[] birds;
	bool isGreenBirdUnlocked, isRedBirdUnlocked;

	void Awake() {
		CreateInstance ();
	}

	void Start() {
		CheckUnlockedBirds ();

		birds [GameController.instance.GetSelectedBird ()].SetActive (true);
	}

	void CreateInstance() {
		if (instance == null)
			instance = this;
	}

	void CheckUnlockedBirds() {
		if (GameController.instance.IsGreenBirdUnlocked () == 1)
			isGreenBirdUnlocked = true;
		if (GameController.instance.IsRedBirdUnlocked () == 1)
			isRedBirdUnlocked = true;
	}

	public void ChangeBird() {
		if (GameController.instance.GetSelectedBird () == 0) {
			if (isGreenBirdUnlocked) {
				GameController.instance.SetSelectedBird (1);
				birds [0].SetActive (false);
				birds [GameController.instance.GetSelectedBird ()].SetActive (true);
			}
		} else if (GameController.instance.GetSelectedBird () == 1) {
			if (isRedBirdUnlocked) {
				GameController.instance.SetSelectedBird (2);
				birds [1].SetActive (false);
				birds [GameController.instance.GetSelectedBird ()].SetActive (true);
			} else {
				GameController.instance.SetSelectedBird (0);
				birds [1].SetActive (false);
				birds [GameController.instance.GetSelectedBird ()].SetActive (true);
			}
		} else if (GameController.instance.GetSelectedBird () == 2) {
			GameController.instance.SetSelectedBird (0);
			birds [2].SetActive (false);
			birds [GameController.instance.GetSelectedBird ()].SetActive (true);
		}
	}

	public void StartGame() {
		SceneFader.instance.ChangeScene ("Gameplay");
	}

    public void Share() {
        TwitterController.instance.Share();
    }

    public void LogOnTwitter() {
        TwitterController.instance.StartLogin();
    }
}
