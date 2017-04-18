using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeCollector : MonoBehaviour {

    GameObject[] pipeHolders;
    float distance = 2.5f;
    float minY = -1.62f;
    float maxY = 1.65f;
    float lastPipeX;

    void Awake() {
        pipeHolders = GameObject.FindGameObjectsWithTag("PipeHolder");

        for(int i = 0; i < pipeHolders.Length; i++) {
            Vector3 tempPos = pipeHolders[i].transform.position;
            tempPos.y = Random.Range(minY, maxY);
            pipeHolders[i].transform.position = tempPos;
        }

        lastPipeX = pipeHolders[0].transform.position.x;

        for(int i = 1; i < pipeHolders.Length; i++) {
            if (pipeHolders[i].transform.position.x > lastPipeX)
                lastPipeX = pipeHolders[i].transform.position.x;
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "PipeHolder") {
            Vector3 tempPos = collision.transform.position;
            tempPos.x = lastPipeX + distance;
            tempPos.y = Random.Range(minY, maxY);
            lastPipeX = tempPos.x;
            collision.transform.position = tempPos;
        }
    }
}
