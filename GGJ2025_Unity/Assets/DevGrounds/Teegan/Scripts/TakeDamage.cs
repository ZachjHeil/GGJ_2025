//Teegan Tulk
//2025-01-24 
//Global Game Jam 2025
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public PlayerStats stats;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HandleDamage(float takeDamage)
    {
        Debug.Log("Handling Damage in " + this.name);
        if (takeDamage > 0) { takeDamage = -takeDamage; }
        if (stats != null) { stats.UpdateHealth(takeDamage); }
       // else if {  }
       // else
       // {

    }

}


