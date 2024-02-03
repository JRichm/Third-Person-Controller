using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float rotationSpeed = 600f;
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] public GameObject cameraHolder;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject weaponHolder;
    [SerializeField] private GameObject weaponHolderAnchor;
    [SerializeField, Range(0f, 90f)] private float maxLookUpAngle;
    [SerializeField, Range(0f, 90f)] private float maxLookDownAngle;

    private List<GameObject> weaponInventory = new List<GameObject>();

    private Rigidbody rb;
    private bool isGrounded;

    private float pitch, yaw = 0f;
   
    private void Start () {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() {
        HandleMouseLook();
        HandleMovement();
        HandleJump();
        weaponHolderAnchor.transform.rotation = cameraHolder.transform.rotation;
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            HandleMouseClick();
        }
    }

    private void HandleMouseLook() {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        pitch -= mouseY * rotationSpeed * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, -maxLookDownAngle, maxLookUpAngle);

        yaw += mouseX * rotationSpeed * Time.deltaTime;

        cameraHolder.transform.localRotation = Quaternion.Euler(pitch, yaw, 0f);
        playerObject.transform.localRotation = Quaternion.Euler(0, yaw, 0f);
    }

    private void HandleMovement() {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Get the forward direction of the camera without the vertical component
        Vector3 cameraForward = cameraHolder.transform.forward;
        cameraForward.y = 0f;
        cameraForward.Normalize();

        // Get the right direction of the camera without the vertical component
        Vector3 cameraRight = cameraHolder.transform.right;
        cameraRight.y = 0f;
        cameraRight.Normalize();

        // Calculate the movement direction based on input and camera orientation
        Vector3 movementDirection = (cameraForward * vertical) + (cameraRight * horizontal);

        // Apply movement to the player
        Vector3 movement = movementDirection * movementSpeed * Time.deltaTime;
        transform.Translate(movement);
    }

    private void HandleJump() {
        isGrounded = Physics.Raycast(transform.position + Vector3.up, Vector3.down, 1.2f, groundLayer);
        
        if (Input.GetButtonDown("Jump") && isGrounded) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void HandleMouseClick() {
        if (weaponInventory.Count > 0) {
            weaponInventory[0].GetComponent<GunScript>().HandleShoot();
        }
    }

    public void EquipWeapon(GameObject weaponObj) {
        if (weaponInventory.Count < 5) {
            Debug.Log("Adding weapon to inventory");
            weaponInventory.Add(weaponObj);
            weaponObj.GetComponent<GunScript>().isEquipped = true;
            weaponObj.transform.parent = weaponHolder.transform;
            weaponObj.transform.position = weaponHolder.transform.position;
            weaponObj.transform.rotation = weaponHolder.transform.rotation;
        } else {
            Debug.Log("Not enough space in inventory");
        }
    }
}