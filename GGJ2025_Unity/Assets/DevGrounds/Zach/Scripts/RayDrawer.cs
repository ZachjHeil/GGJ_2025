using UnityEngine;

public class RayDrawer : MonoBehaviour
{
    public float rayLength = 10f;  // Length of the ray
    public Color rayColor = Color.red;  // Color of the ray

    void Update()
    {
        // Draw the ray starting from the object's position and going forward in its local space (forward direction)
        Debug.DrawRay(transform.position, transform.forward * rayLength, rayColor);
    }
}
