using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1stPMove : MonoBehaviour
{
    public float currentSpeed, runMultiplier, moveSpeed, runSpeed;
    private CharacterController charController;
    private CapsuleCollider capCollider;
    private MeshFilter meshFilter;
    private Vector3 movement;
    private double timer;

    [SerializeField] private Transform cameraTarget;
    private Camera mainCamera;

    [SerializeField] private bool invertMouse = false;
    private float verticalRotationLimit;

    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float lookUpConstraint;
    [SerializeField] private float lookDownConstraint;

    private void Start()
    {
        // Initializing
        charController = this.GetComponent<CharacterController>();
        capCollider = this.GetComponent<CapsuleCollider>();
        meshFilter = this.GetComponent<MeshFilter>();
        // Grab main camera
        mainCamera = Camera.main;
        // Speed logic
        currentSpeed = moveSpeed;
        runSpeed = moveSpeed * runMultiplier;
    }

    private void Update()
    {
        // Camera panning logic
        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;

        float yRotation = transform.rotation.eulerAngles.y + mouseInput.x;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, yRotation, transform.rotation.eulerAngles.z);

        // If camera inverting is selected, invert direction of panning
        float invert = invertMouse ? 1f : -1f;
        verticalRotationLimit += (mouseInput.y * invert);

        // Limit the amount of looking up and down
        verticalRotationLimit = Mathf.Clamp(verticalRotationLimit, lookDownConstraint, lookUpConstraint);
        cameraTarget.rotation = Quaternion.Euler(verticalRotationLimit, cameraTarget.eulerAngles.y, cameraTarget.eulerAngles.z);

        // Fetch buttonpress
        Vector3 moveForward = transform.forward * Input.GetAxisRaw("Vertical");
        Vector3 moveRight = transform.right * Input.GetAxisRaw("Horizontal");

        // Speed logic
        currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : moveSpeed;

        movement = (moveForward + moveRight).normalized * currentSpeed;
        charController.Move(movement * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftControl))
        {
            if(meshFilter.sharedMesh.name == "Capsule" || meshFilter.sharedMesh.name == "Capsule(Clone)")
            {
                if(timer == 0)
                {
                    this.transform.localScale = new Vector3(1, 0.75f, 1);
                    this.transform.position = new Vector3(this.transform.position.x, 0.75f, this.transform.position.z);
                    timer = Time.timeAsDouble;
                }
                else if (Time.timeAsDouble - timer > 0.1)
                {
                    // Change MeshFilter to Sphere
                    GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    MeshFilter mf = go.GetComponent<MeshFilter>();
                    Mesh mesh1 = Instantiate(mf.sharedMesh) as Mesh;
                    meshFilter.sharedMesh = mesh1;
                    Destroy(go);

                    // Morph CapCollider and CharController rigid body
                    capCollider.height = 1;
                    charController.height = 1;

                    // Rescale and reposition
                    this.transform.localScale = new Vector3(1, 1, 1);
                    this.transform.position = new Vector3(this.transform.position.x, 0.5f, this.transform.position.z);

                    timer = 0;
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            // Change MeshFilter to Capsule
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            MeshFilter mf = go.GetComponent<MeshFilter>();
            Mesh mesh1 = Instantiate(mf.sharedMesh) as Mesh;
            meshFilter.sharedMesh = mesh1;
            Destroy(go);

            // Morph CapCollider and CharController rigid body
            capCollider.height = 2;
            charController.height = 2;

            // Rescale and reposition
            this.transform.localScale = new Vector3(1, 1, 1);
            this.transform.position = new Vector3(this.transform.position.x, 1, this.transform.position.z);
        }
    }

    private void LateUpdate()
    {
        mainCamera.transform.position = cameraTarget.position;
        mainCamera.transform.rotation = cameraTarget.rotation;
    }
}
