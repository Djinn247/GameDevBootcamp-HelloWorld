using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Goal"))
        {
            print("Goal Reached!");

            // TODO showing score and win prompt
        }
    }
}
