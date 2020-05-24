using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// moving Enemy
// In Idle State Enemy is just idling,
// scanning for players, 
// then if player is in range
// then bool enemy in range then transition to move state,
// in movestate enemy go to player position
// if player is out of range go to original position (true/false) or stay
// if player is in range and in attack range transition to attack state
namespace SideScrollerProject
{
    [CreateAssetMenu(fileName = "New Enemy Idle State", menuName = "ShadedGames/StateAbilityData/Enemy/Idle")]
    public class EnemyIdle : StateData
    {

        // Colliders 
        // RigidBody
        // bool
        // Player GameObject

        Transform searchPoint;
        Transform attackPoint;
        float searchRange;
        LayerMask playerLayer;
        float attackRange;
        Animator animator;
        Status status;


        private void ScanEnemy(LayerMask playerMask)
        {
            int c = 0;
            Collider2D[] playerCollider = Physics2D.OverlapCircleAll(searchPoint.position, searchRange, playerLayer);

            foreach (Collider2D player in playerCollider)
            {
                Debug.Log("IS DIS WORKING " + animator.GetComponent<Transform>().name);

                if (player.CompareTag("Player"))
                {
                    Debug.Log("Detected: " + player.tag + " " + c + " From " + animator.gameObject.transform.name);
                    status.target = player.transform;
                    if (!player.CompareTag("Player"))
                    { }
                    animator.SetBool("playerOnSight", true);
                    animator.SetBool("isMoving", true);
                    c++;

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
            }
        }


        public override void OnEnter(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool("isIdle", true);
            animator.SetBool("isAttacking", false);
            this.animator = animator;
            searchPoint = animator.gameObject.GetComponent<Status>().searchPoint;
            searchRange = animator.gameObject.GetComponent<Status>().searchRange;
            attackPoint = animator.gameObject.GetComponent<Status>().attackPoint;
            attackRange = animator.gameObject.GetComponent<Status>().attackRange;
            playerLayer = animator.gameObject.GetComponent<Status>().playerLayer;
            status = animator.gameObject.GetComponent<Status>();
           
        }

        public override void UpdateAbility(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {

            //    ScanEnemy(playerLayer);
            //   EnemyInRange(playerLayer, animator);

        }

        public override void OnExit(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool("isIdle", false);
        }
    }
}