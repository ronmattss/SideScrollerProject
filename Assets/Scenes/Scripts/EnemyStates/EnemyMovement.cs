﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// move to player position
// stop at player position
// then attack
namespace SideScrollerProject
{

    [CreateAssetMenu(fileName = "New Enemy Movement State", menuName = "ShadedGames/StateAbilityData/Enemy/Movement")]
    public class EnemyMovement : StateData
    {
        private Transform playerPosition;

        private Transform enemyPosition;
        private Transform attackPoint;
        private float attackRange;
        private bool e_Grounded;
        public float moveSpeed = 0.5f;
        Rigidbody2D rb;

        public LayerMask playerLayer;

        Vector3 refVel = Vector3.zero;




        public void MoveEnemy(Animator animator)
        {
            rb = animator.gameObject.GetComponent<Rigidbody2D>();
            Flip();
            Vector2 target = new Vector2(playerPosition.position.x, rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, moveSpeed * Time.fixedDeltaTime);
            if (!e_Grounded)
            {   animator.ResetTrigger("isMoving");
                Debug.Log("Aint Grounded");
                rb.AddForce(Vector2.down * 100);
            }
            else
            {
                rb.MovePosition(newPos);
            }
        }
        private void Flip()
        {
            if (enemyPosition != null)
                if (enemyPosition.position.x > playerPosition.position.x)
                {
                    enemyPosition.localScale = new Vector3(-1, 1, 1);
                    // moveSpeed *= 1f;
                }
                else
                {
                    enemyPosition.localScale = new Vector3(1, 1, 1);
                    // moveSpeed *= 1f;
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
            }
        }





        public override void OnEnter(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            playerPosition = animator.gameObject.GetComponent<Status>().target;
            enemyPosition = animator.gameObject.transform;
            attackPoint = animator.gameObject.GetComponent<Status>().attackPoint;
            e_Grounded = animator.gameObject.GetComponent<Status>().isGrounded;
            animator.SetBool("isMoving", true);            //addForce???


        }


        public override void OnExit(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            MoveEnemy(animator);
            //   animator.SetBool("isMoving", false);
            //  rb.velocity = refVel;
        }

        public override void UpdateAbility(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {


            MoveEnemy(animator);


            // not Grounded

            //     EnemyInRange(playerLayer, animator);
        }
    }
}
