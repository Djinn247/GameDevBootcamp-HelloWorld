using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithAddForce : MonoBehaviour
{
    public Rigidbody playerRigidbody;
    public float force;
    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.UpArrow))
        {
            playerRigidbody.AddForce(new Vector3(0, 0, force), ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            playerRigidbody.AddForce(new Vector3(0, 0, -force), ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            playerRigidbody.AddForce(new Vector3(-force, 0, 0), ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            playerRigidbody.AddForce(new Vector3(force, 0, 0), ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.Space) && this.transform.position.y == 1)
        {
            playerRigidbody.AddForce(new Vector3(0, force*3, 0), ForceMode.VelocityChange);
        }
    }
}

