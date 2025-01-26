//Teegan Tulk
//2025-01-26
using UnityEngine;
using UnityEngine.Events;

public class Checkpoint : MonoBehaviour
{
    public string playerTag = "Player";
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == playerTag)
        {
            Debug.Log("Saving Trigger Entered");
            if (SavingLoading.Instance != null) { SavingLoading.Instance.CheckpointSave(); }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == playerTag)
        {
            Debug.Log("Saving Trigger Exit");
            if (SavingLoading.Instance != null) { SavingLoading.Instance.CheckpointSave(); }
        }
    }
}
