using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1stPCollectible : MonoBehaviour
{
    public int score, maxScore, upgradeDuration;

    private Player1stPMove playerMoveScript;
    private float oldMoveSpeed, oldRunMultiplier;
    private double upgradeTimer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Collectible"))
        {
            print("trigger entered");
            
            // Increase player score
            score += 1;

            // TODO make the player faster for 10 seconds
            playerMoveScript = gameObject.GetComponent<Player1stPMove>();

            // Fetch and store old speed, then apply powerup
            playerMoveScript.moveSpeed *= 2.0f;
            print("New Playerspeed is: " + playerMoveScript.moveSpeed);

            // Fetch and store old run multiplier, then apply powerup
            playerMoveScript.runMultiplier *= 2.0f;
            print("New Playerspeed is: " + playerMoveScript.moveSpeed);
            playerMoveScript.runSpeed = oldMoveSpeed * playerMoveScript.runMultiplier;

            // Mark time of pickup for duration logic
            upgradeTimer = Time.timeAsDouble;

            Destroy(other.gameObject);
        }
    }

    private void Start()
    {
        score = 0;
        maxScore = 7;
        playerMoveScript = gameObject.GetComponent<Player1stPMove>();
        print("Playerspeed is: " + playerMoveScript.moveSpeed);
        // Fetch and store old speed
        oldMoveSpeed = playerMoveScript.moveSpeed;
        oldRunMultiplier = playerMoveScript.runMultiplier;
        upgradeDuration = 10;
        upgradeTimer = 0.0;
    }

    private void Update()
    {
        if (upgradeTimer != 0)
        {
            // Calc time in seconds since pickup and countdown the duration
            double countdown = upgradeDuration - (Time.timeAsDouble - upgradeTimer);
            print("Upgrade duration: " + (int)countdown);

            // If duration has passed completely effects should be reverted
            if (countdown <= 0)
            {
                // Revert to old speed and run multiplier
                playerMoveScript.moveSpeed = oldMoveSpeed;
                print("New Playerspeed is: " + playerMoveScript.moveSpeed);
                playerMoveScript.runMultiplier = oldRunMultiplier;
                playerMoveScript.runSpeed = oldMoveSpeed * playerMoveScript.runMultiplier;

                // Clear pickup timer
                upgradeTimer = 0;
            }
        }
    }
}
