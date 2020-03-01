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

    public void TakeDamage(int damage,Animator animator)
    {
        Debug.Log($"Damage:{damage}");
        currentHealth -= damage;
        Knockback(animator);
        if (currentHealth <= 0)
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

    public void Knockback(Animator animator)
    {
        Transform playerTransform = animator.GetComponentInParent<Transform>();
        Vector2 knockback = new Vector2(250 * playerTransform.localScale.x, 100);
        this.gameObject.GetComponent<Rigidbody2D>().AddForce((knockback), ForceMode2D.Force);

    }
}
