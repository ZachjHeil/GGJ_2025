using UnityEngine;
using System.Collections;

public class SpawnFish : MonoBehaviour
{
        [SerializeField]
    private float speed = 5f; // Speed of movement towards the player

    private Transform playerTransform;

[SerializeField]
private string playerTag = "Player"; // Assign this in the Inspector or leave it as "Player"
  private void Start()
    {
        // Find the player using the "Player" tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
            StartCoroutine(DelayedMoveTowardsPlayer(.1f)); // Example: Start moving after 3 seconds
        }
        else
        {
            Debug.LogError("Player not found! Make sure the player has the correct tag.");
        }
    }

    private IEnumerator DelayedMoveTowardsPlayer(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        if (playerTransform != null)
        {
            Debug.Log("Moving towards the player...");
            
            // Move towards the player continuously
            while (Vector3.Distance(transform.position, playerTransform.position) > 0.9f)
            {
                // Move step-by-step towards the player's position
                transform.position = Vector3.MoveTowards(
                    transform.position, 
                    playerTransform.position, 
                    speed * Time.deltaTime
                );
            
                // Wait for the next frame
                yield return null;
            }
      
            Debug.Log("Reached the player!");
        }
    }
public void Awake()
{
    GameObject player = GameObject.FindGameObjectWithTag(playerTag);

    if (player != null)
    {
        transform.LookAt(player.transform.position);
    }
    else
    {
        Debug.LogError("You forgot your player, fuck head.");
    }
}
}
