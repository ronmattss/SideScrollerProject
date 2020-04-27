using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Status Script
// Status Script for Enemies
// all enemies should have this script
// this script contains all statuses(states) enemies can do
// along side with its animations
// 
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
        public int currentHealth;
        Animator animator;
        public SpriteRenderer spriteRenderer;
        public Material materialProperty;
        public float dissolveValue = 1f;
        public SliderScript slider;
        void Start()
        {
            spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
            materialProperty = spriteRenderer.material;
            if (transform.tag == "Enemy")
            {
                animator = this.gameObject.GetComponent<Animator>();
                animator.gameObject.SetActive(false);
                animator.gameObject.SetActive(true);
            }

            // target = this.transform;
            currentHealth = maxHealth;
            slider.SetMaxValue(maxHealth);
        }


        void Update()
        {   // if player is not on Sight
            if (!animator.GetBool("playerOnSight"))
            {
                ScanEnemy(playerLayer);
            }
            else
            {
                animator.SetBool("isMoving", true);
            }
            if (target == null)
            {
                animator.SetBool("isMoving", false);
                animator.SetBool("isIdle", true);
                animator.SetBool("playerOnSight", false);
                animator.SetBool("playerInRange", false);
            }

            if (currentHealth <= 0)
            {
                materialProperty.SetFloat("_Fade", dissolveValue);
            }

            animator.SetInteger("health", currentHealth);
            EnemyInRange(playerLayer, animator);
        }
        private void OnDrawGizmosSelected()
        {
            if (searchPoint != null)
                Gizmos.DrawWireSphere(searchPoint.position, searchRange);
            if (attackPoint != null)
                Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
        public void Die(Animator animator)
        {

        }
        #region TakeDamage
        public void TakeDamage(int damage)
        {
            Debug.Log($"Damage:{damage}");
            animator.SetBool("isHurt", true);
            currentHealth -= damage;
            slider.SetValue(currentHealth);
            Knockback();
            if (currentHealth <= 0)
            {
                this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                Destroy(slider.slider);
                // animator.SetBool("isDead", true);
                // moving to dead enemy state
                // Destroy(this.gameObject);
            }
            else
            {
                //Hurt
                Debug.Log(this.name + " " + currentHealth);
            }
        }
        #endregion

        public void Die()
        {
            Destroy(this.gameObject);
        }
        #region Knockback
        public void Knockback()
        {
            Transform transform = this.GetComponent<Transform>();
            // (250) needs to be substituted to a variable, also the 100
            // fucking FIX THIS
            float direction = target.localScale.x ;//==1 ? 1:-1;
            Vector2 knockback = new Vector2(250 * direction, 100);
            this.gameObject.GetComponent<Rigidbody2D>().AddForce((knockback), ForceMode2D.Force);

        }
        #endregion
        #region ScanEnemy
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
        #endregion
        #region EnemyInRange
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
        #endregion
        #region EnemyAttack 
        public void RegisterAttack(Animator animator)
        {
            // Register enemies
            Collider2D[] playerCollider = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

            // Damage enemies
            foreach (Collider2D player in playerCollider)
            {
                if (!player.isTrigger)
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
        #endregion
    }
}