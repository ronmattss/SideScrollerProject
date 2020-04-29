using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace SideScrollerProject
{
    public class PlayerStatus : MonoBehaviour
    {
        public int maxHealth = 100;
        public int maxResource = 100;
        public float knockbackForce = 250f;
        public Transform attackPoint;
        public Transform searchPoint;
        public Transform target;
        public float searchRange = 0;
        public float attackRange = 0;
        public Animator animator;
        public Slider healthSlider;
        public Slider resourceSlider;
        public SliderScript healthSliderScript;
        public SliderScript resourceSliderScript;
        public Actions actions;
        public int currentHealth;
        public int currentResource;
        public float timeBeforeRegenerating = 3f;
        public int regenerateRate = 1;
        public bool canRegenerateResource = true;
        float countDown;
        void Start()
        {
            healthSliderScript.slider = healthSlider;
            resourceSliderScript.slider = resourceSlider;
            animator = this.GetComponent<Animator>();
            animator.gameObject.SetActive(false);
            animator.gameObject.SetActive(true);
            // target = this.transform;
            healthSliderScript.SetMaxValue(maxHealth);
            resourceSliderScript.SetMaxValue(maxResource);

            currentHealth = maxHealth;
            currentResource = maxResource;
            countDown = timeBeforeRegenerating;

        }



        void Update()
        {
            RegenerateResource();
        }
        private void OnDrawGizmosSelected()
        {
            if (searchPoint != null)
                Gizmos.DrawWireSphere(searchPoint.position, searchRange);
            if (attackPoint != null)
                Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }

        public void TakeDamage(int damage, Animator enemyAnimator)
        {
            Debug.Log($"Damage:{damage}");
            currentHealth -= damage;
            healthSliderScript.SetValue(currentHealth);
            Knockback(enemyAnimator.GetComponentInParent<Transform>().localScale.x);
            animator.SetBool("IsHurt",true);
            ResetCountDown();
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
        public void RegenerateResource()
        {
            if (canRegenerateResource)
            {
                if (countDown > 0)
                {
                    countDown -= Time.deltaTime;
                }
                else
                {
                    if (currentResource != maxResource)
                    {
                        currentResource += regenerateRate;
                        resourceSliderScript.SetValue(currentResource);
                    }
                    else
                    {
                        ResetCountDown();
                    }

                }
            }
            else
            {
                ResetCountDown();
            }
        }
        public void ResetCountDown()
        {
            countDown = timeBeforeRegenerating;
        }

        public void Knockback(float direction)
        {
            Vector2 knockback = new Vector2(knockbackForce * direction, 100);
            this.gameObject.GetComponent<Rigidbody2D>().AddForce((knockback), ForceMode2D.Force);

        }
        public void DepleteResourceBar(int value)
        {
            currentResource -= value;
            resourceSliderScript.SetValue(currentResource);
        }
        public void ChangeHealthBar(int value)
        {
            if (value > 0)
            {
                currentHealth += value;
                healthSliderScript.SetValue(currentHealth);
            }
            else
            {
                currentHealth -= value;
                healthSliderScript.SetValue(currentHealth);
            }
        }

        // Seperate slider functions unique to the player

    }
}