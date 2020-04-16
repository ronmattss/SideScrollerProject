using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Base Status Script
namespace SideScrollerProject
{
    public class Status : MonoBehaviour
    {
        public int maxHealth = 100;
        public Transform attackPoint;
        public Transform searchPoint;
        public Transform target;
        public float searchRange = 0;
        public float attackRange = 0;
        public int attackDamage = 10;
        public LayerMask playerLayer;
        int currentHealth;
        Animator animator;
        void Start()
        {
            if (transform.tag == "Enemy")
            {
                animator = this.gameObject.GetComponent<Animator>();
                animator.gameObject.SetActive(false);
                animator.gameObject.SetActive(true);
            }

            // target = this.transform;
            currentHealth = maxHealth;
        }


        void Update()
        {
            if (!animator.GetBool("playerOnSight"))
            {
                ScanEnemy(playerLayer);
            }
            else
            {
                animator.SetBool("isMoving", true);
            }
            if( target == null)
            {
                 animator.SetBool("isMoving", false);
                  animator.SetBool("isIdle", true);
                   animator.SetBool("playerOnSight", false);
                   animator.SetBool("playerInRange", false);
                   
            }
            EnemyInRange(playerLayer, animator);
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


        private void ScanEnemy(LayerMask playerMask)
        {

            Collider2D[] playerCollider = Physics2D.OverlapCircleAll(searchPoint.position, searchRange, playerLayer);

            foreach (Collider2D player in playerCollider)
            {
                // Debug.Log("IS DIS WORKING " + animator.GetComponent<Transform>().name);

                if (player.CompareTag("Player"))
                {
                    //Debug.Log("Detected: " + player.tag + " " + c + " From " + animator.gameObject.transform.name);
                    target = player.transform;
                    if (!player.CompareTag("Player"))
                    { }
                    animator.SetBool("playerOnSight", true);
                    animator.SetBool("isMoving", true);

                }
                else
                {
                    animator.SetBool("playerOnSight", false);
                }
            }
        }
        private void EnemyInRange(LayerMask playerMask, Animator animator)
        {
            Collider2D[] playerCollider = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

            foreach (Collider2D player in playerCollider)
            {
                if (player.CompareTag("Player"))
                {
                    animator.SetBool("playerInRange", true);
                    animator.SetBool("isMoving", false);
                    return;
                }
                else if (player == null)
                {
                    animator.SetBool("isMoving", false);
                }
            }
        }

        public void RegisterAttack(Animator animator)
        {
            // Register enemies
            Collider2D[] playerCollider = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

            // Damage enemies
            foreach (Collider2D player in playerCollider)
            {
                Debug.Log($"Enemy hit: {player.name}");
                PlayerStatus playerStatus = player.gameObject.GetComponent<PlayerStatus>();

                if (playerStatus == null)
                    return;
                else
                {
                    playerStatus.TakeDamage(attackDamage, animator);
                }

            }


        }
    }
}