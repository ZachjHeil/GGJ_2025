using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    public Transform player; // Reference to the player object

 void LateUpdate()
{
    Vector3 newPosition = player.position;
    newPosition.y = transform.position.y; // Maintain the camera's height
    transform.position = newPosition;

    // Rotate the camera to match the player's Y-axis rotation
    transform.rotation = Quaternion.Euler(90, player.eulerAngles.y, 0);
}

}
