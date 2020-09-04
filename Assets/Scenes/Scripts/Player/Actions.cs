using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using EZCameraShake;


// TODO:
//
/*
Player Attack combo
How to do it
Research about how it is done
*/

namespace SideScrollerProject
{
    public class Actions : MonoBehaviour
    {
        public Movement movement;
        public MaskController thirdEye;
        public float runSpeed = 40f;
        public float dashForce = 100f;
        public float smallMovementDuration = 0.025f;
        public int horizontalMovement = 0;
        public bool canRun = true;
        public Transform attackPoint;
        public float attackRange;
        public int attackCounter = 0;
        public bool isInteracting = false;
        public Interactable interactables;
        bool jump = false;
        int jumpCount = 0;
        public float jumpTime;
        public float jumpForce;
        private float jumpTimeCounter;
        private bool isJumping;
        bool doubleJump = false;
        bool crouch = false;
        public bool canJump = true;
        public bool canMove = true;
        public bool canDash = true;
        public bool canAttack = true;
        public bool canSee = true;

        public Animator animator;
        public MaterialPropertyBlock material;
        public PlayerStatus playerStatus;
        public ParticleSystem dustEffect;
        public ChangeMotion motion;
        [Tooltip("HitStopPausesomething")] public float hitStop = .75f;
        [Tooltip("HitStopPausesomething")] public const float hitStopDuration = .140f;





        // Start is called before the first frame update

        // Update is called once per frame
        // Refactor This class sometime later
        void Update()
        {

            if (Input.GetKeyUp(KeyCode.F))
            {
                if (isInteracting)
                {
                    interactables.Interact(isInteracting);
                }

            }
            // Register Movement Bool
            if (canMove)// ifplayer can move
            {
                if (canRun)
                    horizontalMovement = Convert.ToInt16(Input.GetAxisRaw("Horizontal")); //* runSpeed);
                movement.xMovement = horizontalMovement * (runSpeed / 10);

                if (Input.GetKeyDown(KeyCode.DownArrow) && movement.isOnOneWayPlatform)
                {
                    movement.canGoDown = true;
                }
                if (Input.GetKeyUp(KeyCode.S) && canSee)
                {
                    thirdEye.ResizeObject();
                    animator.SetBool("isEyeOn", thirdEye.thirdEyeOn);
                    //motion.ChangeAnimationClip();
                    motion.ChangeMultipleAnimationClips();
                }

                if (Input.GetKeyDown(KeyCode.Z) && canAttack)
                {
                    // horizontalMovement = 0;

                    //  if (InputManager.instance.attackCounter == 0)
                    // {
                    AttackTriggerManager.instance.attackAnimationController.TriggerAnimationBool(AnimatorParams.Attacking.ToString());
                    //animator.SetBool(AnimatorParams.Attacking.ToString(), true);
                    //animator.SetBool(AnimatorParams.IsInCombo.ToString(), true);
                    animator.SetInteger(AnimatorParams.AttackCounter.ToString(), InputManager.instance.attackCounter);
                    if (Input.GetAxisRaw("Horizontal") != 0)
                    {
                        //  horizontalMovement = Convert.ToInt16(Input.GetAxisRaw("Horizontal")); //* runSpeed);
                        //  movement.xMovement = horizontalMovement * (runSpeed / 10);
                    }

                    //  }
                    //InputManager.instance.attackCounter++;
                }
                if (Input.GetKeyDown(KeyCode.H) && canAttack)
                {
                    AttackTriggerManager.instance.attackAnimationController.TriggerAnimationBool(AnimatorParams.IsHeavyAttacking.ToString());
                    //AttackTriggerManager.instance.attackAnimationController.TriggerAnimationBool(AnimatorParams.Attacking.ToString());
                    animator.SetInteger(AnimatorParams.AttackCounter.ToString(), InputManager.instance.attackCounter);
                }

                if (Input.GetKeyDown(KeyCode.X) && movement.availableDash > 0 && canDash)
                {
                    // Test Camera Shake
                    // CameraShaker.Instance.ShakeOnce(4,4,.1f,.1f);
                    //  CreateDust();
                    movement.isDashing = true;
                    movement.availableDash--;
                    if (movement.isDashing)
                    {
                        movement.DashSound();
                    }
                    // playerStatus.DepleteResourceBar(10);
                    playerStatus.ResetCountDown();
                    // TriggerAbility();
                }
            }
            // if (Input.GetButtonDown("Vertical"))
            // {
            //     crouch = true;
            // }
            // else if (Input.GetButtonUp("Vertical"))
            // {
            //     crouch = false;
            // }
            if (movement.availableDash <= 0)
                movement.Recharge();

            /* if (movement.m_Grounded && Input.GetButtonDown("Jump"))
             {
                 movement.m_Rigidbody2D.velocity = Vector2.up * jumpForce;
                 isJumping = true;
                 jump = true;
                 jumpTimeCounter = jumpTime;

                 animator.SetBool("Jumping", true);
                 animator.SetBool(AnimatorParams.Attacking.ToString(), false);
                 PlayerParticleSystemManager.instance.StartParticle(PlayerParticles.JumpDust);
             }*/
            // if (Input.GetButton("Jump") && isJumping == true)
            // {
            //     if (jumpTimeCounter > 0)
            //     {
            //         movement.m_Rigidbody2D.velocity = Vector2.up * jumpForce;
            //         jumpTimeCounter -= Time.deltaTime;
            //     }
            //     else
            //     {
            //         isJumping = false;
            //     }
            // }
            // if (Input.GetKeyUp("Jump"))
            // {
            //     isJumping = false;
            // }


        }
        public void OnLanding()
        {
            animator.SetBool("Jumping", false);
            PlayerParticleSystemManager.instance.StartParticle(PlayerParticles.GroundImpact);
            isJumping = false;
            //  movement.jumpDust.Play();

        }

        void FixedUpdate()
        {
            // if (animator.GetBool(AnimatorParams.Attacking.ToString()))
            // {
            //     horizontalMovement = 0;
            //     Debug.Log($" This should be 0{horizontalMovement}");

            // }


            movement.Move((horizontalMovement * runSpeed) * Time.fixedDeltaTime, (dashForce * this.transform.localScale.x), crouch, jump);
            movement.SmallDash();
            //  movement.SmallMovement();
            animator.SetInteger("Moving", (int)horizontalMovement);
            animator.SetInteger("Falling", movement.GetFallVelocity());
            jump = false;
            //  Debug.Log(horizontalMovement);
        }

        void CreateDust()
        {
            dustEffect.Play(false);
        }
        void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
        public void SetDash(bool decision)
        {
            canDash = decision;
        }
        public void SetThirdEye(bool decision)
        {
            canSee = decision;
        }
        public IEnumerator HitStop(Animator enemyAnimator = null, float duration = 0.15f)
        {
            animator.speed = 0;
            if (enemyAnimator != null)
                enemyAnimator.speed = 0;
            yield return new WaitForSecondsRealtime(duration);
            Debug.Log("animatorSpeed:" + animator.speed);
            //animator.speed = 1;
            animator.speed = LeanTween.easeInOutQuad(0, 1, hitStop);
            if (enemyAnimator != null)
            {
                enemyAnimator.speed = LeanTween.easeInOutQuad(0, 1, hitStop);
            }
            yield return null;
        }
        public void ApplyHitStop(Animator otherAnimator = null, float duration = hitStopDuration)
        {
            StartCoroutine(HitStop(otherAnimator, duration));
        }





        //TODO: I need to make a ability class or somesort of ability manager

        // private void OnTriggerStay2D(Collider2D other)
        // {

        //     if (other.gameObject.GetComponent<Interactable>() != null)
        //     {
        //         // Debug.Log(" Interactable: " + other.tag);
        //         if (Input.GetKeyDown(KeyCode.E))
        //         {
        //             Debug.Log("Pressing E");
        //             { other.GetComponent<Interactable>().Interact(true); }
        //         }
        //     }

        // }

        // Collect all Interactable objects
    }
}
