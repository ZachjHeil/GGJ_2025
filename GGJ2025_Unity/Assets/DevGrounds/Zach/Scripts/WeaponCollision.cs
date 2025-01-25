using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    
    // The tag that the collided object must have to trigger the event
    public string targetTag = "Enemy";

    // Reference to a GameObject, serialized for assignment in the Inspector (if needed)
    

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has the specified tag
        if (collision.gameObject.CompareTag(targetTag))
        {
            // Attempt to get the FishAI script from the collided object
            FishAI targetScript = collision.gameObject.GetComponent<FishAI>();

            if (targetScript != null)
            {
                // Call the function in the script of the tagged object
                targetScript.EndAttack();
            }
            else
            {
                Debug.LogWarning("The collided object does not have a FishAI script attached.");
            }

            // Destroy the thrown object (this object)
            Destroy(gameObject);
        }
    }
}
