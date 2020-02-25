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
        public int horizontalMovement = 0;
        bool jump = false;
        bool crouch = false;
        public Animator animator;
        // Start is called before the first frame update

        // Update is called once per frame
        void Update()
        {
            horizontalMovement = Convert.ToInt16(Input.GetAxisRaw("Horizontal")); //* runSpeed);

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
                animator.SetBool("Jumping", true);

            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
              //  if (InputManager.instance.attackCounter == 0)
               // {
                    animator.SetBool(AnimatorParams.Attacking.ToString(), true);

              //  }
                InputManager.instance.attackCounter++;
                animator.SetInteger(AnimatorParams.AttackCounter.ToString(), InputManager.instance.attackCounter);
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
            Debug.Log(Input.GetAxisRaw("Horizontal"));
            movement.Move((horizontalMovement * runSpeed) * Time.fixedDeltaTime, crouch, jump);
            animator.SetInteger("Moving", (int)horizontalMovement);
            animator.SetInteger("Falling", movement.GetFallVelocity());
            jump = false;
            Debug.Log(horizontalMovement);
        }
    }
}
