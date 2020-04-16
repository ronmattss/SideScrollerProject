using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SideScrollerProject
{
    public class PlayerStatus : MonoBehaviour
    {
        public int maxHealth = 100;
        public Transform attackPoint;
        public Transform searchPoint;
        public Transform target;
        public float searchRange = 0;
        public float attackRange = 0;
        public Animator animator;
        int currentHealth;
        void Start()
        {
            animator = this.GetComponent<Animator>();
            animator.gameObject.SetActive(false);
            animator.gameObject.SetActive(true);
            // target = this.transform;
            currentHealth = maxHealth;
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
}