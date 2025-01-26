//Teegan Tulk
//2025-01-24 
//Global Game Jam 2025
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    const float MAX_HEALTH = 100;
    const float MAX_OXYGEN = 100;

    [Header("Stats")]
    [SerializeField]
    float health;
    [SerializeField]
    float oxygen;


    [Header("Oxygen Rates")]
    [SerializeField]
    float oxygenDrainTimeInSec = 120;
    [SerializeField]
    float oxygenGainTimeInSec = 20;

    [Header("Health Rates")]
    [SerializeField]
    float healthDrainTimeInSec = 120;
    [SerializeField]
    float healthGainTimeInSec = 20;


    [Header("Sliders")]

    [SerializeField]
    Slider healthSlider;
    [SerializeField]
    Slider oxygenSlider;

    [Header("Reference")]
    [SerializeField]
    PlayerController playerController;

    bool dead = false;
    public bool knownUnderwater = false;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        playerController = GetComponent<PlayerController>();
        health = MAX_HEALTH;
        oxygen = MAX_OXYGEN;

        healthSlider.maxValue = MAX_HEALTH;
        oxygenSlider.maxValue = MAX_OXYGEN;


        UpdateHealthSlider();
        UpdateOxygenSlider();

        knownUnderwater = playerController.underWater;
    }


    void LateUpdate()
    {
        if (knownUnderwater != playerController.underWater)
        {
            if (playerController.underWater) //player has now gone underwater
            {
                StopAllCoroutines();
                StartCoroutine(KnockOxygen());

            }
            else  //player is now above water
            {
                StopAllCoroutines();

                StartCoroutine(GainHealth());
                StartCoroutine(GainOxygen());
            }
            knownUnderwater = playerController.underWater; //update last known
        }
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
        float decrement = health / healthDrainTimeInSec;
        while (oxygen <= 0 || !dead || playerController.underWater) //if the player dies in the middle of this, or gets more oxygen, or is no longer under water, stop.
        {
            //take ten seconds to die based on starting health
            UpdateHealth(-decrement);
            yield return new WaitForSeconds(1f);

        }
    }
    IEnumerator GainHealth()
    {
        //temp, increment at starting health
        float decrement = health / healthGainTimeInSec;
        while (oxygen <= 0 || !dead || !playerController.underWater || health != MAX_HEALTH) //if the player dies in the middle of this, or gets more oxygen, or is no longer above water, or is fully healed, stop.
        {
            //take ten seconds to full health based on starting health
            UpdateHealth(decrement);
            yield return new WaitForSeconds(1f);

        }
    }

    public void PlayerIntoWater()
    {

        StartCoroutine(KnockOxygen());
        
    }
    public void PlayerOutOfWater()
    {

        StopCoroutine(KnockOxygen());
    }
    IEnumerator KnockOxygen()
    {
        //temp, decrement at full health
        float decrement = MAX_HEALTH / oxygenDrainTimeInSec;
        while (oxygen >= 0 || !dead || playerController.underWater) //if the player dies or runs out of oxygen, or is no longer underwater, stop
        {
            //take oxygen drain time in sec to die based on starting health
            UpdateOxygen(-decrement);
            yield return new WaitForSeconds(1f);

        }
    }
    IEnumerator GainOxygen()
    {
        //temp, decrement at full health
        float decrement = health / oxygenGainTimeInSec;
        while (oxygen >= 0 || !dead || !playerController.underWater || oxygen != MAX_OXYGEN) //if the player dies or runs out of oxygen, or is no longer underwater, or is full oxygen, stop
        {
            //take 10 seconds to return to full oxygen
            UpdateOxygen(decrement);
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
