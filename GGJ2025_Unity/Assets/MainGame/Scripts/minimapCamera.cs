using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    public Transform player; // Reference to the player object

    void LateUpdate()
    {
        // Update the camera's position to match the player's position
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y; // Maintain the camera's height
        transform.position = newPosition;
    }
}
