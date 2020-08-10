using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProjectFSM
{
    public class StateIdle : State
    {
        public float waitSeconds = 1f;
        public float current;
        public int randNum;
        public StateIdle(GameObject _boss, Rigidbody2D rb, Animator _bossAnimator, Transform _player) : base(_boss, rb, _bossAnimator, _player)
        {
            name = STATE.IDLE;
        }
        public override void Enter()
        {
            current = waitSeconds;
            //do some animations
            animator.SetTrigger("isIdle");
            // targets
            // bla bla
            base.Enter();
        }
        public override void Exit()
        {
            animator.ResetTrigger("isIdle"); //resets anim
            base.Exit();
        }
        public override void Update()
        {
            // Condition to exit Idle State
            // Should be a move or an attack 
            // Debug.Log(stage);
            randNum = Random.Range(0, 2);
            if (current <= 0)
                if (player != null)
                {
                    switch (randNum)
                    {
                        case 0:
                            nextState = new StateMove(boss, bossRb, animator, player);
                            break;
                        case 1:
                            nextState = new StateCharging(boss, bossRb, animator, player);
                            break;

                        default:
                            break;
                    }
                    stage = EVENT.EXIT;
                    Debug.Log("has Player ref");
                }
                else { }
            else
            {
                current -= Time.fixedDeltaTime;
            }
            Debug.Log("is it counting? " + current);
            //base.Update();
        }
        // Wait for 0.5f



    }
}