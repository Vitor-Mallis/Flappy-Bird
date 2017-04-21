using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fabric.Twitter;

public class TwitterController : MonoBehaviour {

    public static TwitterController instance;

    [SerializeField]
    Animator twitterPanel;

    [SerializeField]
    Text twitterText;

    void Awake() {
        CreateSingleton();
    }

    void CreateSingleton() {
        if (instance != null)
            Destroy(gameObject);
        else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void StartLogin() {
        TwitterSession session = Twitter.Session;
        if (session == null) {
            Twitter.LogIn(LoginComplete, LoginFailure);
        }
        else {
            Twitter.LogOut();
            twitterText.text = "Logged Out";
            StartCoroutine(SlideInOut());
        }
    }

    public void Share() {
        if (Twitter.Session != null)
            StartComposer(Twitter.Session);
        else {
            twitterText.text = "Not logged in";
            StartCoroutine(SlideInOut());
        }
    }

    void LoginComplete(TwitterSession session) {
        twitterText.text = "Logged In";
        StartCoroutine(SlideInOut());
    }

    void LoginFailure(ApiError error) {
		twitterText.text = error.message;
		StartCoroutine (SlideInOut ());
        UnityEngine.Debug.Log("code=" + error.code + " msg=" + error.message);
    }

    void StartComposer(TwitterSession session, string imageUri = "") {
		Card card = new AppCardBuilder ().ImageUri ("");

        Twitter.Compose(session, card);
    }

    IEnumerator SlideInOut() {
        twitterPanel.Play("SlideIn");
        yield return new WaitForSeconds(2f);
        twitterPanel.Play("SlideOut");
    }
}
