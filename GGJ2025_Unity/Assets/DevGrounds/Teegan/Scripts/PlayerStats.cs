using JetBrains.Annotations;
using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    const float MAX_HEALTH = 100;
    const float MAX_OXYGEN = 100;

    [SerializeField]
    float health;
    [SerializeField]
    float oxygen;

    [SerializeField]
    Slider healthSlider;
    [SerializeField]
    Slider oxygenSlider;

    public bool OutOfOxygen = false;

    bool dead = false;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //If saving and loading system in place, then get the information from that, otherwise
        health = MAX_HEALTH;
        oxygen = MAX_OXYGEN;

        healthSlider.maxValue = MAX_HEALTH;
        oxygenSlider.maxValue = MAX_OXYGEN;

        UpdateHealthSlider();
        UpdateOxygenSlider();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Health
    public void UpdateHealth(float modifier)
    {
        health += modifier;
        if (health <= 0)
        {
            //dead
            health = 0;
            PlayerDead();
            
        }
        else if (health > MAX_HEALTH)
        {
            health = MAX_HEALTH;
        }
        UpdateHealthSlider();
        
    }

    void UpdateHealthSlider()
    {
        //To Do: If we have time, make it move smoothly with a coroutine
        if (healthSlider != null)
        {
            healthSlider.value = health;
        }
    }

    IEnumerator KnockHealth()
    {
        //temp, decrement at starting health
        float decrement = health / 10;
        while (oxygen <= 0 && !dead) //if the player dies in the middle of this, or gets more oxygen, stop.
        {
            //take ten seconds to die based on starting health
            UpdateHealth(-decrement);
            yield return new WaitForSeconds(1f);

        }
    }


    public void PlayerDead()
    {
        dead = true;
        StopCoroutine(KnockHealth());
        //respond
    }

    public void PlayerRevived()
    {
        dead = false;
        health = MAX_HEALTH;
        oxygen = MAX_OXYGEN;
        UpdateHealthSlider();
        UpdateOxygenSlider();
        

    }
    #endregion

    #region Oxygen

    public void UpdateOxygen(float modifier)
    {
        oxygen += modifier;
        if (oxygen <= 0)
        {
            oxygen = 0;
            //Start knocking health
            StartCoroutine(KnockHealth());
        }
        else if (oxygen > MAX_OXYGEN)
        {
            oxygen = MAX_OXYGEN;
        }
        UpdateOxygenSlider();
        

    }
    public void UpdateOxygenSlider()
    {
        if (oxygenSlider != null) { oxygenSlider.value = oxygen; }
    }


  
    #endregion
}
