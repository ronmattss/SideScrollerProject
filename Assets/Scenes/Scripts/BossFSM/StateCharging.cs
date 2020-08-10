using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SideScrollerProjectFSM
{
    public class StateCharging : State
    {
        int randomNumber = 0;
        public StateCharging(GameObject _boss, Rigidbody2D _rb, Animator _bossAnimator, Transform _player) : base(_boss, _rb, _bossAnimator, _player)
        {
            name = STATE.CHARGE;
        }

        public override void Enter()
        {
            randomNumber = Random.Range(0, 2);
            animator.SetInteger("pattern", Random.Range(0, 2));
            switch (randomNumber)
            {
                case 0:
                    animator.SetInteger("chargePattern", randomNumber);
                    animator.SetTrigger("isFlameCharging");
                    break;
                case 1:
                    animator.SetInteger("chargePattern", randomNumber);
                    animator.SetTrigger("isChestCharging");
                    break;
                default:
                    break;
            }
            base.Enter();

        }

        public override void Exit()
        {
            Debug.Log("This should be called after Charging");
            switch (randomNumber)
            {
                case 0:
                    nextState = new StateWalkingBurner(boss, bossRb, animator, player);
                    Debug.Log("NEXTSTATE: " + nextState.GetType().Name);

                    animator.ResetTrigger("isFlameCharging");
                    break;
                case 1:
                    nextState = new StateChestAttacking(boss, bossRb, animator, player);
                    Debug.Log("NEXTSTATE: " + nextState.GetType().Name);
                    animator.ResetTrigger("isChestCharging");
                    break;
                default:
                    break;
            }
            stage = EVENT.EXIT;
            base.Exit();
        }

        public override void Update()
        {

        }
    }
}