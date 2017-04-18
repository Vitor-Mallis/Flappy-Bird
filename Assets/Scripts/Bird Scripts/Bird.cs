using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bird : MonoBehaviour {

    public static Bird instance;
    
    [SerializeField]
    private Rigidbody2D rigidBody;
    [SerializeField]
    private Animator animator;

    private float forwardSpeed = 3f;
    private float bounceSpeed = 4f;

    private bool didFlap;
    [HideInInspector]
    public bool isAlive = true;

    private Button flapButton;

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
}
