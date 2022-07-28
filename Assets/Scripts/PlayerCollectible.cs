using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollectible : MonoBehaviour
{
    private PlayerMove playerMoveScript;
    private float playerSpeed;
    [SerializeField]
    private int score, upgradeDuration;
    private double upgradeTimer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Collectible"))
        {
            score += 1;

            // TODO make the player faster for 10 seconds
            playerMoveScript.speed = (playerSpeed * 2.0f);
            print("New Playerspeed is: " + playerMoveScript.speed);
            upgradeTimer = Time.timeAsDouble;

            Destroy(other.gameObject);
        }
    }
    
    private void Start()
    {
        score = 0;
        playerMoveScript = this.gameObject.GetComponent<PlayerMove>();
        playerSpeed = playerMoveScript.speed;
        print("Playerspeed is: "+playerSpeed);
        upgradeTimer = 0.0;
    }

    private void Update()
    {
        if (upgradeTimer != 0)
        {
            double countdown = upgradeDuration - (Time.timeAsDouble - upgradeTimer);
            print("Upgrade duration: " +(int)countdown );
            if (countdown <= 0)
            {
                playerMoveScript.speed = playerSpeed;
                print("New Playerspeed is: " + playerMoveScript.speed);
                upgradeTimer = 0;
            }
        }
    }
}
