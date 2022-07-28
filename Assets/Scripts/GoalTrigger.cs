using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    private GameObject goalCamera;
    private void Start()
    {
        goalCamera = GameObject.Find("Goal Camera");
        goalCamera.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish")
        {
            print("Goal Reached!");

            // TODO showing score and win prompt
            GameObject.Find("Main Camera (1stP)").SetActive(false);
            goalCamera.SetActive(true);

        }
    }
}
