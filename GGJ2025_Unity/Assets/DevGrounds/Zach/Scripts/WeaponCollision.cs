using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    
    // The tag that the collided object must have to trigger the event
    public string targetTag = "Enemy";
    public string surfaceTag = "Water";
    float lifeTime = 10f;
    float startScale = 0.25f;
    float maxScale = 2f;
    float gravity = 3.5f;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(lifeTime <= 0) { Destroy(gameObject); return; }
        lifeTime -= Time.deltaTime;

        float curScale = Mathf.Clamp(10f - lifeTime, startScale, maxScale);
        transform.localScale = new Vector3(curScale, curScale, curScale);

        rb.angularVelocity.Set(rb.angularVelocity.x, gravity * Time.deltaTime, rb.angularVelocity.z);
    }


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

        else if (collision.gameObject.CompareTag(surfaceTag))
        {
            Destroy(gameObject);
        }
    }
}
