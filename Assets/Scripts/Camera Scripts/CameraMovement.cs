using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public static float offsetX;

	void Update() {
        if(Bird.instance != null) {
            if (Bird.instance.isAlive)
                MoveCamera();
        }
    }

    void MoveCamera() {
        Vector3 tempPos = transform.position;
        tempPos.x = Bird.instance.GetPositionX() + offsetX;
        transform.position = tempPos;
    }
}
