
using System.Collections.Generic;
using UnityEngine;

public class GiveDamage : MonoBehaviour
{
    public string test = "";
    public List<string> giveDamageTags = new List<string>();
    public float damageAmount = 1f;

    
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("On Trigger Enter running in" + this.name);
        TakeDamage takeDamage = other.GetComponent<TakeDamage>();
        if (takeDamage != null)
        {
            Debug.Log("Found Take Damage in Other");
            for (int i = 0; i < giveDamageTags.Count; i++)
            {
                if (other.tag == giveDamageTags[i])
                {
                    Debug.Log("Found give damage tag");
                    takeDamage.HandleDamage(damageAmount);
                    break;
                }
            }
        }
        else
        {
            Debug.Log("Did Not Find Take Damage in Other");
        }
    }
}
