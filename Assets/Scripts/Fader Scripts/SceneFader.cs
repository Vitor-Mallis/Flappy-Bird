using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour {

	public static SceneFader instance;

	[SerializeField]
	GameObject canvas;
	[SerializeField]
	Animator animator;

	void Awake() {
		CreateSingleton ();
	}

	void CreateSingleton() {
		if (instance != null) {
			Destroy (gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad (gameObject);
		}
	}

	public void ChangeScene(string scene) {
		StartCoroutine (FadeInOut (scene));
	}

	IEnumerator FadeInOut(string scene) {
		canvas.SetActive (true);
		animator.Play ("FadeOut");
		yield return StartCoroutine(MyCoroutine.WaitForRealSeconds (1f));
		SceneManager.LoadScene (scene);
		animator.Play ("FadeIn");
		yield return StartCoroutine(MyCoroutine.WaitForRealSeconds (.7f));
		canvas.SetActive (false);
	}
}
