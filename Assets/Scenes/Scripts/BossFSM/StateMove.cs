using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SideScrollerProjectFSM
{
    public class StateMove : State
    {
        /// <summary> State that moves the npc to a given Transform</summary>

        public StateMove(GameObject _boss, Rigidbody2D _rb, Animator _bossAnimator, Transform _player) : base(_boss, _rb, _bossAnimator, _player)
        {
            name = STATE.MOVE;
            // move speed
            // rigidbody ref?
        }

        public override void Enter()
        {   // reference of the player

            animator.SetTrigger("isMoving");
            // walking anim
            base.Enter();
        }

        public override void Exit()
        {
            animator.ResetTrigger("isMoving");
            base.Exit();
        }

        public override void Update()
        {  //
           // movement code
           //  
            if (player != null)
            {
                if (Vector2.Distance(boss.transform.position, player.position) > 5.5f)
                    MoveEnemy();
                else
                {
                    nextState = new StateMeleeAttack(boss, bossRb, animator, player);
                    stage = EVENT.EXIT;
                }

                // if player distance < 5.5f Go to Attack State

            }
            //base.Update();
        }

        public void MoveEnemy()
        {

            Flip();
            Vector2 target = new Vector2(player.position.x, bossRb.position.y);
            Vector2 newPos = Vector2.MoveTowards(bossRb.position, target, 2f * Time.fixedDeltaTime);
            // Debug.Log("Distance of HB to Player: " + Vector2.Distance(boss.transform.position, player.position));


            bossRb.MovePosition(newPos);

        }
        private void Flip()
        {
            if (player != null)
                if (boss.transform.position.x > player.position.x)
                {
                    boss.transform.localScale = new Vector3(-1, 1, 1);
                    // moveSpeed *= 1f;
                }
                else
                {
                    boss.transform.localScale = new Vector3(1, 1, 1);
                    // moveSpeed *= 1f;
                }
        }
    }
}
