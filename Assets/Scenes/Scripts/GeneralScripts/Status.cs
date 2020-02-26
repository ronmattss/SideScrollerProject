using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Base Status Script
public class Status : MonoBehaviour
{
   public int maxHealth = 100;
   int currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
    }

    
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        Debug.Log($"Damage:{damage}");
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            //Hurt
            Debug.Log(this.name + " " + currentHealth);
        }

    }

    public void Die(Animator animator)
    {

    }
}
