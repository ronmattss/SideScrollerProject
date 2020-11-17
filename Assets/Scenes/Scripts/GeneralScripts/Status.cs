using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;


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
    public class Status : EntityStatus
    {    [Header("Base Properties")]
        public int maxHealth = 100;
        public int currentHealth;
        public float moveSpeed = 0.5f;
        public float speed = 0.5f;
        public UnityEvent onDeathEvent;
        [Header("Inputs for state machine")] 
        public bool isPlayerInRange;
        public bool isPlayerInSight;
        public bool isNpcPatrolling;
        public bool isNpcAttacking;
        public bool isNpcHurt;
        public bool isNpcDead;
        public bool isNpcMoving;
        [Header("NPC is on Ground")]
        public bool isGrounded;
        public bool iSGroundPatrolCheckerGrounded = true;
        
        [Header("Collision Points")]
        public Transform attackPoint;
        public Transform searchPoint;
        public Transform ground;
        public Transform groundPatrolChecker;
        public Transform lineRendererLocation;

        public LayerMask whatIsGround;
        [Header("Range and Target Properties ")]
        public Transform target;
        public float searchRange = 0;
        public float attackRange = 0;
        public LayerMask playerLayer;
        [Header("Component References")]
        public SpriteRenderer spriteRenderer;
        public Material materialProperty;
        public float dissolveValue = 1f;
        public SliderScript slider;
        private SpriteRenderer enemyRenderer;
        public Material whiteFlash;
        [Header("Patrol Properties")]
        public Transform pointA;
        public Transform pointB;
        public Vector2 nextPoint;
        private Animator animator;
        private Vector2 pA;
        private Vector2 pB;
        public bool isPatrolling = false;
        [Header("Range Properties")]
        public bool isRange = false;
        [Tooltip("Point in the game object where a line will be cast as line of sight ")]
        public Transform raycastOrigin;
        public Transform targetInitialPosition;
        public LineRenderer laser;
        public GameObject line;
        public bool targetLock = false;
        private bool changeColor = false;
        public GameObject playerPosition;
        public bool travelsOnOneAxis;
        public GameObject projectilePrefab;
        public AudioSource bowDraw;
        public float range = 7;
        private Rigidbody2D rb;
        [Header ("Debug")]
        public bool useStateMachine = false;

        public DamageModifier damageModifier = new DamageModifier();
        void Start()
        {
            speed = moveSpeed;
            enemyRenderer = GetComponent<SpriteRenderer>();
            LeanTween.init(3000);
            line = GameObject.Find("Line");
            if (line != null)
                laser = line.GetComponent<LineRenderer>();
            spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
            materialProperty = spriteRenderer.material;
            animator = gameObject.GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
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
            currentHealth = base.currentStatus.maxHealth;
            slider.SetMaxValue(base.currentStatus.maxHealth);
            spriteRenderer.material.SetColor("_Color", Color.black);
            damageModifier.entityBuffGiver = GetComponent<BuffGiver>();
        }
        void LateUpdate()
        {
            if (changeColor)
                StartCoroutine(ChangeColor());
        }
        void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(ground.position, 0.3f);
            Gizmos.DrawRay(groundPatrolChecker.position, Vector2.down);
            Gizmos.DrawRay(lineRendererLocation.position, Vector2.down);
            Debug.DrawLine(lineRendererLocation.position, new Vector2(lineRendererLocation.position.x + 3, lineRendererLocation.position.y));
        }
        public Vector2 ChangeDirection()
        {
            return nextPoint = nextPoint != pA ? pA : pB;
        }
        public void CheckGround()
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

        public void CheckGroundPoint()
        {
            // iSGroundPatrolCheckerGrounded = false;
            RaycastHit2D groundCollider = Physics2D.Raycast(groundPatrolChecker.position, Vector2.down, 1, whatIsGround);
            if (groundCollider.collider != null)
            {
                iSGroundPatrolCheckerGrounded = true;
                //   Debug.Log("Ground Check Collider: " + groundCollider.transform.name);
            }
            else
            {
                iSGroundPatrolCheckerGrounded = false;
            }

        }
        public Vector2 LockTarget()
        {
            targetLock = true;
            return targetInitialPosition.position;


        }
        // will



        void Update()
        {
            if (useStateMachine == true) return;
            CheckGround();
            CheckGroundPoint();
            // if player is not on Sight
            if (!animator.GetBool("playerOnSight"))
            {
                ScanEnemy(playerLayer);
            }
            else if (target != null && isRange)
            {

                animator.SetBool("isMoving", false);
                Flip(this.transform, target.position);
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

            if ((isRange && isPatrolling) && target == null)
            {
                animator.SetBool("isPatrolling", true);
                if (Vector2.Distance(this.transform.position, nextPoint) <= 1)
                {
                    // Debug.Log("Patrolneed to changeBefore: " + nextPoint);

                    //                    Debug.Log("Patrolneed to changed: " + nextPoint);
                }
            }

            if (isPatrolling)
            {
                // if (Vector2.Distance(this.transform.position, nextPoint) <= 1)
                // {
                //     //            Debug.Log("Patrolneed to changeBefore: " + nextPoint);
                //     nextPoint = ChangeDirection();
                //     //           Debug.Log("Patrolneed to changed: " + nextPoint);
                // }
                //Patrol(this.animator, this.transform, nextPoint, rb, 1);
                Patrol(this.transform, 2);
            }

            // if (isPatrolling && isRange)
            // {
            //     // if (Vector2.Distance(this.transform.position, nextPoint) <= 1)
            //     // {
            //     //     //            Debug.Log("Patrolneed to changeBefore: " + nextPoint);
            //     //     nextPoint = ChangeDirection();
            //     //     //           Debug.Log("Patrolneed to changed: " + nextPoint);
            //     // }
            //     // Patrol(this.animator, this.transform, nextPoint, rb, 1);
            //     Patrol(this.transform, rb, 2);
            // }
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
        #region TakeDamage
        public override void TakeDamage(int baseDamage)
        {
            //LevelManager.instance.FreezeHit();
            StartCoroutine(Wait());
            WaitThenExitState();

            Debug.Log($"Damage:{baseDamage}");
            if (!animator.GetBool("isAttacking"))
                animator.SetBool("isHurt", true);
            currentHealth -= baseDamage;
            slider.SetValue(currentHealth);
            if (target != null)
                // Knockback();
                if (currentHealth <= 0)
                {
                    this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    Destroy(slider.slider);

                    EffectsManager.instance.Spawn(animator.gameObject.transform.position, "DeathHitfx");
                    EffectsManager.instance.Spawn(animator.gameObject.transform.position, "DeadFx");
                    onDeathEvent?.Invoke();
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
        #endregion

        public void Die()
        {
            Destroy(this.gameObject);
        }
        #region Knockback
        public void Knockback(float knockbackStrenth)
        {
            if (target == null)
            {
                return;
            }
            Transform transform = this.GetComponent<Transform>();
            // (250) needs to be substituted to a variable, also the 100
            // fucking FIX THIS
            float direction = target.localScale.x;//==1 ? 1:-1;
            Vector2 knockback = new Vector2(knockbackStrenth * direction, 10);
            this.gameObject.GetComponent<Rigidbody2D>().AddForce((knockback), ForceMode2D.Impulse);

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

                // Debug.Log("Name: " + hit.collider.name);
                if (hit.collider != null)
                {
                    if (hit.collider.CompareTag("Player")) // well idk wat i DID WIP
                    {
                        target = hit.collider.transform;
                        animator.SetBool("isPatrolling", false);
                        animator.SetBool("playerOnSight", true);
                        return;
                    }
                    else if (hit.collider == null && target != null)
                    {
                        animator.SetBool("isMoving", false);
                    }
                }
                else
                {
                    target = null;
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
                        target = player.transform;
                        animator.SetBool("playerOnSight", false);
                    }
                }
            }
        }
        #endregion

        public void RangeAttack()
        {
            Vector2 raycastDirection = this.transform.localScale.x == -1 ? Vector2.left : Vector2.right;
            var position = raycastOrigin.position;
            RaycastHit2D hit = Physics2D.Linecast(position, new Vector2(raycastDirection.x * range + position.x, position.y), playerLayer);
            Debug.DrawLine(position, new Vector2(raycastDirection.x * range + position.x, position.y), Color.blue);
            GameObject arrow = Instantiate(projectilePrefab, attackPoint.position, Quaternion.identity);
            Projectile proj = arrow.GetComponent<Projectile>();
            if (target != null && !travelsOnOneAxis)
            {
                proj.Launch(target.position, raycastDirection.x);
            }
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

                bowDraw.Play();
                if (travelsOnOneAxis)
                    proj.Launch(raycastDirection.x); // if basic left or right projectile like the Archer
                else
                    proj.Launch(target.position, raycastDirection.x); // if it target locks (Not Homing)
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
                if (target != null && !travelsOnOneAxis)
                {
                    if (Vector2.Distance(this.transform.position, target.position) <= range)
                        hit = Physics2D.Linecast(raycastOrigin.position, new Vector2(raycastDirection.x + target.position.x, target.position.y), playerLayer);
                    Debug.DrawLine(raycastOrigin.position, new Vector2(raycastDirection.x + target.position.x, target.position.y), Color.red);
                }
                Debug.DrawLine(raycastOrigin.position, new Vector2(raycastDirection.x * range + raycastOrigin.position.x, raycastOrigin.position.y), Color.red);
                //if (hit.collider != null)
                // Debug.Log("Name: " + hit.collider.name);
                if (hit.collider != null) // overwrite if projectile
                    if (hit.collider.CompareTag("Player")) // well idk wat i DID WIP
                    {
                        animator.SetBool("playerInRange", true);
                        animator.SetBool("isMoving", false);
                        return;
                    }
                    else if (hit.collider == null)
                    {
                        target = null;
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
        public void RegisterAttack()
        {
            // Register enemies
            Collider2D[] playerCollider = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

            // Damage enemies
            foreach (Collider2D player in playerCollider)
            {
                if (!player.isTrigger)
                {
                    Debug.Log($"Enemy hit: {player.name}");
                    EntityStatus playerStatus = player.gameObject.GetComponent<PlayerStatus>();

                    if (playerStatus == null)
                        return;
                    else
                    {
                        damageModifier.DoDamage(currentStatus.initialDamage, player.gameObject);
                        // playerStatus.TakeDamage(attackDamage);
                    }
                }


            }


        }
        #endregion
        // instead of going to two points, go to the left or right until one of the condition is met
        // if isGround == false flip || wall distance <= 2
        public void Patrol(Animator animator, Transform thisTransform, Vector2 currentPoint, Rigidbody2D rb, float moveSpeed)
        {

            Flip(thisTransform, currentPoint);
            Vector2 target = new Vector2(currentPoint.x, thisTransform.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, moveSpeed * Time.fixedDeltaTime);
            this.gameObject.transform.position = newPos;//rb.velocity = new Vector2(newPos.x, 0);
            rb.velocity = new Vector2(thisTransform.localScale.x * UnityEngine.Random.Range(0.5f, 1.1f), 0);
            //rb.MovePosition(newPos);
            // thisTransform.LeanMoveX(newPos.x,0.5f);
            //            Debug.Log(thisTransform.position.x <= currentPoint.x);

        }
        public void Patrol(Transform thisTransform, float moveSpeed)
        {//Debug.Log($"{this.transform.name } is patrolling");
            Vector2 raycastDirection = this.transform.localScale.x == -1 ? Vector2.left : Vector2.right;
            RaycastHit2D lineHit = Physics2D.Linecast(lineRendererLocation.position, new Vector2(raycastDirection.x * 3 + lineRendererLocation.position.x, lineRendererLocation.position.y), whatIsGround);
            if (iSGroundPatrolCheckerGrounded == false || lineHit.collider != null)
            {
                Flip(thisTransform);
            }
            //rb.MovePosition(new Vector2(thisTransform.position.x+(1*this.transform.localScale.x),this.transform.position.y));
            rb.velocity = new Vector2(thisTransform.localScale.x * moveSpeed, 0);
        }
        private void Flip(Transform self, Vector2 patrolPoint)
        {
            if (self != null)
                if (self.position.x > patrolPoint.x)
                {
                    self.localScale = new Vector3(-1, 1, 1);
                    // moveSpeed *= 1f;
                }
                else
                {
                    self.localScale = new Vector3(1, 1, 1);
                    // moveSpeed *= 1f;
                }
        }
        private void Flip(Transform self)
        {
            if (self != null)
                if (self.localScale.x == 1)
                {
                    self.localScale = new Vector3(-1, 1, 1);
                    // moveSpeed *= 1f;
                }
                else
                {
                    self.localScale = new Vector3(1, 1, 1);
                    // moveSpeed *= 1f;
                }
            //      Debug.Log($"{this.transform.name } is flipping");
        }
        IEnumerator DamageIndicator()
        {
            var tempShade = enemyRenderer.material;
            enemyRenderer.material = whiteFlash;
            yield return new WaitForSecondsRealtime(0.09f); // gonna change to for Real Seconds
            enemyRenderer.material = tempShade;
            yield return null;
        }
        public void WaitThenExitState()
        {
            StartCoroutine(DamageIndicator());
            //   StopCoroutine(DamageIndicator());
        }
        IEnumerator Wait()
        {
            AudioManager.instance.Play("HitSFX");
            changeColor = true;
            while (Time.timeScale != 1.0f)
                yield return new WaitForSeconds(0.2f);
        }


    }
}