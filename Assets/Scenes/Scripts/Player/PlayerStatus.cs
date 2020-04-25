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

        }



        void Update()
        {

        }
        private void OnDrawGizmosSelected()
        {
            if (searchPoint != null)
                Gizmos.DrawWireSphere(searchPoint.position, searchRange);
            if (attackPoint != null)
                Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }

        public void TakeDamage(int damage, Animator animator)
        {
            Debug.Log($"Damage:{damage}");
            currentHealth -= damage;
            healthSliderScript.SetValue(currentHealth);
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
        public void RegenerateResource()
        {
            // regenerate resource while attacking, not using an ability or not being hurt
        }

        public void Knockback(Animator animator)
        {
            Transform playerTransform = animator.GetComponentInParent<Transform>();
            Vector2 knockback = new Vector2(250 * playerTransform.localScale.x, 100);
            this.gameObject.GetComponent<Rigidbody2D>().AddForce((knockback), ForceMode2D.Force);

        }
        // Seperate slider functions unique to the player

    }
}