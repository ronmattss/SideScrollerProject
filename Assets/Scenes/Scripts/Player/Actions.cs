using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



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
        public Interactable interactableObject;
        bool jump = false;
        int jumpCount = 0;
        bool doubleJump = false;
        bool crouch = false;
        public Animator animator;
        // Start is called before the first frame update

        // Update is called once per frame
        void Update()
        {


            // Register Movement Bool
            horizontalMovement = Convert.ToInt16(Input.GetAxisRaw("Horizontal")); //* runSpeed);

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isInteracting)
                {
                    interactableObject.Interact(isInteracting);
                }

            }

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;

                animator.SetBool("Jumping", true);
                animator.SetBool(AnimatorParams.Attacking.ToString(), false);
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                //  if (InputManager.instance.attackCounter == 0)
                // {
                animator.SetBool(AnimatorParams.Attacking.ToString(), true);

                //  }
                InputManager.instance.attackCounter++;
                animator.SetInteger(AnimatorParams.AttackCounter.ToString(), InputManager.instance.attackCounter);
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                movement.isDashing = true;
            }

            if (Input.GetButtonDown("Vertical"))
            {
                crouch = true;
            }
            else if (Input.GetButtonUp("Vertical"))
            {
                crouch = false;
            }

        }
        public void OnLanding()
        {
            animator.SetBool("Jumping", false);

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
        void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }

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
