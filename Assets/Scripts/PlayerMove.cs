using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private int speed;
    private Rigidbody playerRigidBody;
    // speed*Time.deltaTime
    
    // Start is called before the first frame update
    private void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Movement
        float horizontalInput = Input.GetAxisRaw("Horizontal"), verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalMovement = horizontalInput * speed, verticalMovement = verticalInput * speed;
        Vector3 movement = new Vector3(horizontalMovement, 0, verticalMovement);
        playerRigidBody.velocity = movement;
    }

}
