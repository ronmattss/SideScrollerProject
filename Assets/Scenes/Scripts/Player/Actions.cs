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
        public float runSpeed = 40f;
        public float dashForce = 100f;
        public int horizontalMovement = 0;
        public Transform attackPoint;
        public float attackRange;
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
        public Animator animator;
        public MaterialPropertyBlock material;
        public PlayerStatus playerStatus;
        public ParticleSystem dustEffect;




        // Start is called before the first frame update

        // Update is called once per frame
        // Refactor This class sometime later
        void Update()
        {


            // Register Movement Bool

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (isInteracting)
                {
                    interactables.Interact(isInteracting);
                }

            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && movement.isOnOneWayPlatform)
            {
                movement.canGoDown = true;
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                //  if (InputManager.instance.attackCounter == 0)
                // {
                animator.SetBool(AnimatorParams.Attacking.ToString(), true);

                //  }
                //InputManager.instance.attackCounter++;
                animator.SetInteger(AnimatorParams.AttackCounter.ToString(), InputManager.instance.attackCounter);
            }

            if (Input.GetKeyDown(KeyCode.X) && movement.availableDash > 0)
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

            if (Input.GetButtonDown("Vertical"))
            {
                crouch = true;
            }
            else if (Input.GetButtonUp("Vertical"))
            {
                crouch = false;
            }
            if (movement.availableDash <= 0)
                movement.Recharge();

            if (movement.m_Grounded && Input.GetButtonDown("Jump"))
            {
                movement.m_Rigidbody2D.velocity = Vector2.up * jumpForce;
                isJumping = true;
                jump = true;
                jumpTimeCounter = jumpTime;

                animator.SetBool("Jumping", true);
                animator.SetBool(AnimatorParams.Attacking.ToString(), false);
                PlayerParticleSystemManager.instance.StartParticle(PlayerParticles.JumpDust);
            }
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
            horizontalMovement = Convert.ToInt16(Input.GetAxisRaw("Horizontal")); //* runSpeed);
            movement.xMovement = horizontalMovement * (runSpeed / 10);

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
            if (animator.GetBool(AnimatorParams.Attacking.ToString()))
            {
                horizontalMovement = 0;
            }            //  Debug.Log(Input.GetAxisRaw("Horizontal"));

            movement.Move((horizontalMovement * runSpeed) * Time.fixedDeltaTime, (dashForce * this.transform.localScale.x), crouch, jump);
            movement.SmallDash();
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
