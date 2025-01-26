using UnityEngine;

public class ShootWeapon : MonoBehaviour
{
    // The crosshair texture that will be displayed at the center of the screen
    public Texture2D crosshairTexture;

    // The prefab of the object that will be thrown
    public GameObject throwableObjectPrefab;
//Transform point for the origin of the shot weapon
    public Transform weaponOrigin;
    // The speed at which the object will be thrown
    public float throwForce = 70f;

    // Reference for the player's camera (used for determining throw direction)
    private Camera playerCamera;

    // The size of the crosshair on the screen
    public Vector2 crosshairSize = new Vector2(50, 50);

    private void Start()
    {
        // Get the main camera in the scene
        playerCamera = Camera.main;

        // Ensure we have a crosshair texture set
        if (crosshairTexture == null)
        {
            Debug.LogError("Crosshair texture is not assigned. Please assign it in the inspector.");
        }

        // Ensure the throwable object prefab is set
        if (throwableObjectPrefab == null)
        {
            Debug.LogError("Throwable object prefab is not assigned. Please assign it in the inspector.");
        }
    }

    private void Update()
    {
        // Check for user input (left mouse button or another key, e.g., "E")
        if (Input.GetKeyDown(KeyCode.E))
        {
            ThrowObject();
        }
    }

    private void OnGUI()
    {
        // Draw the crosshair texture at the center of the screen
        if (crosshairTexture != null)
        {
            float xPos = (Screen.width - crosshairSize.x) / 2;
            float yPos = (Screen.height - crosshairSize.y) / 2;
            GUI.DrawTexture(new Rect(xPos, yPos, crosshairSize.x, crosshairSize.y), crosshairTexture);
        }
    }

    private void ThrowObject()
    {
        // Ensure the throwable object prefab is assigned
        if (throwableObjectPrefab == null || playerCamera == null)
        {
            Debug.LogWarning("Cannot throw object: Missing references.");
            return;
        }

        // Instantiate the object at the camera's position
        GameObject thrownObject = Instantiate(throwableObjectPrefab,weaponOrigin.position, Quaternion.identity);

        // Get the Rigidbody component from the instantiated object
        Rigidbody rb = thrownObject.GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("The throwable object prefab must have a Rigidbody component.");
            Destroy(thrownObject);
            return;
        }

        // Apply a force in the forward direction of the camera
        rb.AddForce(playerCamera.transform.forward * throwForce, ForceMode.Impulse);
    }
}
