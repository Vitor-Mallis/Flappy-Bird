using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bird : MonoBehaviour {

    public static Bird instance;
    
    [SerializeField]
    Rigidbody2D rigidBody;
    [SerializeField]
    Animator animator;

    float forwardSpeed = 3f;
    float bounceSpeed = 4f;

    bool didFlap;
    [HideInInspector]
    public bool isAlive = true;

    Button flapButton;

    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    AudioClip flap, die, point;

    int score;

    void Awake() {
        CreateInstance();

        flapButton = GameObject.FindGameObjectWithTag("FlapButton").GetComponent<Button>();
        flapButton.onClick.AddListener(() => Flap());

        SetCameraOffsetX();
    }

    void FixedUpdate() {
        if(isAlive) {
            Vector3 tempPos = transform.position;
            tempPos.x += forwardSpeed * Time.deltaTime;
            transform.position = tempPos;

            if (didFlap) {
                didFlap = false;
                rigidBody.velocity = new Vector2(0, bounceSpeed);
                animator.SetTrigger("Flap");
                audioSource.PlayOneShot(flap);
            }

            if (rigidBody.velocity.y > 0) {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else {
                float rotation = Mathf.Lerp(0, -90, -rigidBody.velocity.y / 7);
                transform.rotation = Quaternion.Euler(0, 0, rotation);
            }
        }
    }

    void CreateInstance() {
        if (instance == null)
            instance = this;
    }

    void SetCameraOffsetX() {
        CameraMovement.offsetX = (Camera.main.transform.position.x - transform.position.x) - 1f;
    }

    public void Flap() {
        didFlap = true;
    }

    public float GetPositionX() {
        return transform.position.x;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "PipeHolder") {
            score++;
            audioSource.PlayOneShot(point);
			GameplayController.instance.SetScore (score);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (isAlive) {
            if (collision.gameObject.tag == "Pipe" || collision.gameObject.tag == "Ground") {
                isAlive = false;
                animator.SetTrigger("Died");
                audioSource.PlayOneShot(die);
				GameplayController.instance.GameOver (score);
            }
        }
    }
}
