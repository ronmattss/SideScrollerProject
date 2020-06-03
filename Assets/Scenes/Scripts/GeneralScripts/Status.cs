﻿using System;
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

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CapsuleCollider2D))]
    [RequireComponent(typeof(SliderScript))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(AudioSource))]
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
        private bool changeColor = false;
        public GameObject playerPosition;
        public Vector2 finalTargetPosition = Vector2.zero;
        public GameObject arrowPrefab;
        public AudioSource bowDraw;
        public float range = 7;
        void Start()
        {

            line = GameObject.Find("Line");
            if (line != null)
                laser = line.GetComponent<LineRenderer>();
            spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
            materialProperty = spriteRenderer.material;
            animator = gameObject.GetComponent<Animator>();

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
            spriteRenderer.material.SetColor("_Color", Color.black);
        }
        void LateUpdate()
        {
            if (changeColor)
                StartCoroutine(ChangeColor());
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
        IEnumerator ChangeColor()
        {
            spriteRenderer.color = Color.black;
            Debug.Log("Changed " + spriteRenderer.color.ToString());
            yield return new WaitForSeconds(0.05f);
            spriteRenderer.color = Color.white;
            changeColor = false;
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
            LevelManager.instance.FreezeHit();
            StartCoroutine(Wait());

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

                EffectsManager.instance.Spawn(animator.gameObject.transform.position, "DeathHitfx");
                EffectsManager.instance.Spawn(animator.gameObject.transform.position, "DeadFx");
                Destroy(this.gameObject);
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
        public void TakeDamage(int damage, bool knockback)
        {
            changeColor = true;
            AudioManager.instance.Play("HitSFX");
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
                EffectsManager.instance.Spawn(animator.gameObject.transform.position, "DeathHitfx");
                EffectsManager.instance.Spawn(animator.gameObject.transform.position, "DeadFx");
                Destroy(this.gameObject);
                //animator.SetBool("isDead", true);
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
        // Redoot this 
        // Change to Line Cast or RayCast
        private void ScanEnemy(LayerMask playerMask)
        {
            // Line Cast with range
            // if enemy is in range or 3/4 of the whole cast
            // attack

            if (isRange)
            {
                Vector2 raycastDirection = this.transform.localScale.x == -1 ? Vector2.left : Vector2.right;
                RaycastHit2D hit = Physics2D.Linecast(raycastOrigin.position, new Vector2(raycastDirection.x * range + raycastOrigin.position.x, raycastOrigin.position.y), playerLayer);
                Debug.DrawLine(raycastOrigin.position, new Vector2(raycastDirection.x * range + raycastOrigin.position.x, raycastOrigin.position.y), Color.blue);

                Debug.Log("Name: " + hit.collider.name);
                if (hit.collider.CompareTag("Player")) // well idk wat i DID WIP
                {
                    target = hit.collider.transform;
                    animator.SetBool("isPatrolling", false);
                    animator.SetBool("playerOnSight", true);
                    return;
                }
                else if (hit.collider == null)
                {
                    animator.SetBool("isMoving", false);
                }

            }
            else
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
        }
        #endregion

        public void RangeAttack()
        {
            Vector2 raycastDirection = this.transform.localScale.x == -1 ? Vector2.left : Vector2.right;
            RaycastHit2D hit = Physics2D.Linecast(raycastOrigin.position, new Vector2(raycastDirection.x * range + raycastOrigin.position.x, raycastOrigin.position.y), playerLayer);
            Debug.DrawLine(raycastOrigin.position, new Vector2(raycastDirection.x * range + raycastOrigin.position.x, raycastOrigin.position.y), Color.blue);
            //Instantiate projectile
            //laser.gameObject.SetActive(true);
            //laser.SetPosition(0, Vector2.zero);
            //laser.SetPosition(1, finalTargetPosition - new Vector2(raycastOrigin.position.x, raycastOrigin.position.y));
            Debug.Log("positions: " + raycastOrigin.localPosition + " " + raycastOrigin.position);
            if (hit.collider == null) Debug.Log("nothing hit");
            // Debug.Log(hit.collider.GetType());
            //  if (hit.GetType() == typeof(CapsuleCollider2D))
            // {
            Debug.Log("Player is in range");

            try
            { // Instantiate Projectile
                GameObject arrow = Instantiate(arrowPrefab, attackPoint.position, Quaternion.identity);
                Projectile proj = arrow.GetComponent<Projectile>();
                bowDraw.Play();
                proj.Launch(raycastDirection.x);
                //PlayerStatus playerStatus = hit.collider.gameObject.GetComponent<PlayerStatus>();
                //playerStatus.TakeDamage(attackDamage, animator);
            }
            catch (NullReferenceException)
            {
                Debug.Log("enemy miss");
            }
            //Wait();
            //RegisterAttack(this.animator);
            // }

        }

        // Splitted how enemies see you in range
        // Wizard is now bugged
        #region EnemyInRange
        private void EnemyInRange(LayerMask playerMask, Animator animator)
        {

            if (isRange)
            {
                Vector2 raycastDirection = this.transform.localScale.x == -1 ? Vector2.left : Vector2.right;
                RaycastHit2D hit = Physics2D.Linecast(raycastOrigin.position, new Vector2(raycastDirection.x * range + raycastOrigin.position.x, raycastOrigin.position.y), playerLayer);
                Debug.DrawLine(raycastOrigin.position, new Vector2(raycastDirection.x * range + raycastOrigin.position.x, raycastOrigin.position.y), Color.red);
                //if (hit.collider != null)
                Debug.Log("Name: " + hit.collider.name);
                if (hit.collider.CompareTag("Player")) // well idk wat i DID WIP
                {
                    animator.SetBool("playerInRange", true);
                    animator.SetBool("isMoving", false);
                    return;
                }
                else if (hit.collider == null)
                {
                    animator.SetBool("isMoving", false);
                }
            }
            else
            {
                Collider2D[] playerCollider = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
                foreach (Collider2D player in playerCollider)
                {
                    if (player.CompareTag("Player") && player.GetType() == typeof(CapsuleCollider2D))
                    {
                        animator.SetBool("playerInRange", true);

                        animator.SetBool("isMoving", false);
                        // Wizard Targetting, Refactoring....
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
            AudioManager.instance.Play("HitSFX");
            changeColor = true;
            while (Time.timeScale != 1.0f)
                yield return new WaitForSeconds(0.2f);
        }
        /// <summary>
        /// Callback to draw gizmos that are pickable and always drawn.
        /// </summary>


    }
}