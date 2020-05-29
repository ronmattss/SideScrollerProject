using System;
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
        public Transform ground;
        public bool isGrounded;
        public LayerMask whatIsGround;
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
        public Transform pointA;
        public Transform pointB;
        public Vector2 nextPoint;
        private Vector2 pA;
        private Vector2 pB;
        public bool isPatrolling = false;
        [Header("Range Properties")]
        public bool isRange = false;

        public Transform raycastOrigin;
        public Transform targetLastPosition;
        public Transform targetInitialPosition;
        public LineRenderer laser;
        public GameObject line;
        public bool targetLock = false;

        public GameObject playerPosition;
        public Vector2 finalTargetPosition = Vector2.zero;
        void Start()
        {
            line = GameObject.Find("Line");
            laser = line.GetComponent<LineRenderer>();
            spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
            materialProperty = spriteRenderer.material;

            // if (transform.tag == "Enemy")

            animator = this.gameObject.GetComponent<Animator>();
            animator.gameObject.SetActive(false);
            animator.gameObject.SetActive(true);

            if (pointA && pointB != null)
            {
                pA = pointA.position;
                pB = pointB.position;
                nextPoint = pA;
            }

            // target = this.transform;
            currentHealth = maxHealth;
            slider.SetMaxValue(maxHealth);
        }
        void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(ground.position, 0.3f);
        }
        public Vector2 ChangeDirection()
        {
            return nextPoint = nextPoint != pA ? pA : pB;
        }
        private void CheckGround()
        {
            isGrounded = false;
            Collider2D[] groundCollider = Physics2D.OverlapCircleAll(ground.position, 0.3f, whatIsGround);
            foreach (Collider2D g in groundCollider)
            {
                isGrounded = true;
                if (!animator.GetBool("isMoving"))
                {

                }
            }
            if (!isGrounded)
            {
                // this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -10));
                //if (animator.GetBool("isMoving"))

                animator.SetBool("isIdle", true);

            }
        }
        public Vector2 LockTarget()
        {
            targetLock = true;
            return targetInitialPosition.position;


        }
        // will



        void Update()
        {   // if player is not on Sight
            CheckGround();
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
                //
                animator.SetBool("isIdle", true);
                animator.SetBool("isMoving", false);
                animator.SetBool("playerOnSight", false);
                animator.SetBool("playerInRange", false);
                //isPatrolling = true;
            }
            else
            {
                animator.SetBool("isPatrolling", false);
                isPatrolling = false;
            }
            if (isPatrolling && target == null)
            {
                animator.SetBool("isPatrolling", true);
                if (Vector2.Distance(this.transform.position, nextPoint) <= 1)
                {
                    // Debug.Log("Patrolneed to changeBefore: " + nextPoint);

                    //                    Debug.Log("Patrolneed to changed: " + nextPoint);
                }
            }
            if (isPatrolling)
                if (Vector2.Distance(this.transform.position, nextPoint) <= 1)
                {
                    //            Debug.Log("Patrolneed to changeBefore: " + nextPoint);
                    nextPoint = ChangeDirection();
                    //           Debug.Log("Patrolneed to changed: " + nextPoint);
                }
            // Debug.Log("Distance: " + Vector2.Distance(this.transform.position, nextPoint));
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
            if (isRange)
                Gizmos.DrawRay(raycastOrigin.position, Vector2.right);
        }
        public void Die(Animator animator)
        {

        }
        #region TakeDamage
        public void TakeDamage(int damage)
        {
            Debug.Log($"Damage:{damage}");
            if (!animator.GetBool("isAttacking"))
                animator.SetBool("isHurt", true);
            currentHealth -= damage;
            slider.SetValue(currentHealth);
            if (target != null)
                Knockback();
            if (currentHealth <= 0)
            {
                this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                Destroy(slider.slider);
                animator.SetBool("isDead", true);
                // moving to dead enemy state
                // Destroy(this.gameObject);
            }
            else
            {
                //Hurt
                Debug.Log(this.name + " " + currentHealth);
            }
        }
        public void TakeDamage(int damage, bool knockback)
        {
            Debug.Log($"Damage:{damage}");
            if (!animator.GetBool("isAttacking"))
                animator.SetBool("isHurt", true);
            currentHealth -= damage;
            slider.SetValue(currentHealth);
            if (target != null)
                if (knockback)
                    Knockback();
            if (currentHealth <= 0)
            {
                this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                Destroy(slider.slider);
                animator.SetBool("isDead", true);
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
            if (target == null)
            {
                return;
            }
            Transform transform = this.GetComponent<Transform>();
            // (250) needs to be substituted to a variable, also the 100
            // fucking FIX THIS
            float direction = target.localScale.x;//==1 ? 1:-1;
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
                    animator.SetBool("isPatrolling", false);
                    animator.SetBool("playerOnSight", true);
                    if (isRange)
                    {



                    }
                    animator.SetBool("isMoving", true);

                }
                else
                {
                    animator.SetBool("playerOnSight", false);
                }
            }
        }
        #endregion

        public void RangeAttack()
        {
            Vector2 raycastDirection = this.transform.localScale.x == -1 ? Vector2.left : Vector2.right;
            RaycastHit2D hit = Physics2D.Raycast(raycastOrigin.position, new Vector3(finalTargetPosition.x, finalTargetPosition.y, 0) - raycastOrigin.position, Mathf.Infinity, playerLayer);
           // Instantiate projectile
            laser.gameObject.SetActive(true);
            laser.SetPosition(0, Vector2.zero);
            laser.SetPosition(1, finalTargetPosition - new Vector2(raycastOrigin.position.x, raycastOrigin.position.y));
            Debug.Log("positions: " + raycastOrigin.localPosition + " " + raycastOrigin.position);
            if (hit.collider == null) Debug.Log("nothing hit");
            // Debug.Log(hit.collider.GetType());
            //  if (hit.GetType() == typeof(CapsuleCollider2D))
            // {
            Debug.Log("Player is in range");

            try
            {
                PlayerStatus playerStatus = hit.collider.gameObject.GetComponent<PlayerStatus>();
                playerStatus.TakeDamage(attackDamage, animator);
            }
            catch (NullReferenceException)
            {
                Debug.Log("enemy miss");
            }
            //Wait();
            //RegisterAttack(this.animator);
            // }

        }
        #region EnemyInRange
        private void EnemyInRange(LayerMask playerMask, Animator animator)
        {
            Collider2D[] playerCollider = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

            foreach (Collider2D player in playerCollider)
            {
                if (player.CompareTag("Player") && player.GetType() == typeof(CapsuleCollider2D))
                {
                    animator.SetBool("playerInRange", true);

                    animator.SetBool("isMoving", false);
                    if (targetLock == false)
                    {
                        targetInitialPosition = player.transform;
                        // position = player.transform.position;
                        playerPosition = player.gameObject;
                        targetLock = true;
                    }

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
        IEnumerator Wait()
        {
            yield return new WaitForSeconds(0.2f);
        }
        /// <summary>
        /// Callback to draw gizmos that are pickable and always drawn.
        /// </summary>


    }
}