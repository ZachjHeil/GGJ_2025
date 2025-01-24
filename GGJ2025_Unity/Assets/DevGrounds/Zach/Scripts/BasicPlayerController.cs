using UnityEngine;

public class BasicPlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed
    public float lookSpeed = 3f; // Look sensitivity

    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            Debug.LogError("No CharacterController found! Add one to the Player GameObject.");
        }
    }

    void Update()
    {
        // Handle movement
        float horizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow
        float vertical = Input.GetAxis("Vertical");     // W/S or Up/Down Arrow
        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        characterController.Move(move * moveSpeed * Time.deltaTime);

        // Handle looking around
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        // Rotate the player horizontally
        transform.Rotate(Vector3.up * mouseX);

        // Clamp vertical rotation (optional for first-person style cameras)
        Camera.main.transform.Rotate(Vector3.left * mouseY);
    }
}
