using UnityEditor;
using UnityEngine;

public class BubbleGiveOxygen : MonoBehaviour
{
    public string playerTag = "Player";
    [SerializeField]
    float giveAmount = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Give Oxygen - On Trigger Enter");
        if (other.tag == playerTag)
        {
            if (other.gameObject.tag == playerTag)
            {
                Debug.Log("Bubble Oxygen Given to Player");
                //Player Stats
                other.GetComponentInParent<PlayerStats>().UpdateOxygen(giveAmount);

            }
        }
    }


    
}
