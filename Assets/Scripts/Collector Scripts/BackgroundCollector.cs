using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCollector : MonoBehaviour {

    GameObject[] backgrounds;
    GameObject[] grounds;

    float lastBGX, lastGroundX;

    void Awake() {
        backgrounds = GameObject.FindGameObjectsWithTag("Background");
        grounds = GameObject.FindGameObjectsWithTag("Ground");

        lastBGX = backgrounds[0].transform.position.x;
        lastGroundX = grounds[0].transform.position.x;

        for(int i = 1; i < backgrounds.Length; i++) {
            if (backgrounds[i].transform.position.x > lastBGX)
                lastBGX = backgrounds[i].transform.position.x;
        }

        for (int i = 1; i < grounds.Length; i++) {
            if (grounds[i].transform.position.x > lastGroundX)
                lastGroundX = grounds[i].transform.position.x;
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Background") {
            float width = ((BoxCollider2D)collision).size.x;
            Vector3 tempBGPos = collision.transform.position;
            tempBGPos.x = lastBGX + width;
            collision.transform.position = tempBGPos;
            lastBGX = tempBGPos.x;
        }
        else if (collision.tag == "Ground") {
            float width = ((BoxCollider2D)collision).size.x;
            Vector3 tempGroundPos = collision.transform.position;
            tempGroundPos.x = lastGroundX + width;
            collision.transform.position = tempGroundPos;
            lastGroundX = tempGroundPos.x;
        }
    }
}
