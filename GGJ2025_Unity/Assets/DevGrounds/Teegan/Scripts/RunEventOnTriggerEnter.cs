//Teegan Tulk
//2025-01-25
//Global Game Jam 2025
using UnityEngine;
using UnityEngine.Events;

public class RunEventOnTriggerEnter : MonoBehaviour
{
    public string playerTag = "Player";
    public UnityEvent unityEvent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        unityEvent = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == playerTag)
        {
            Debug.Log("Event Trigger Entered");
            unityEvent.Invoke();
        }
    }
}
