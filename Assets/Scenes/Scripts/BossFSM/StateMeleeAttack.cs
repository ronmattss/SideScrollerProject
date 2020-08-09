using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SideScrollerProjectFSM
{
    public class StateMeleeAttack : State
    {
        public StateMeleeAttack(GameObject _boss, Rigidbody2D _rb, Animator _bossAnimator, Transform _player) : base(_boss, _rb, _bossAnimator, _player)
        {
            name = STATE.ATTACK;
        }

        // Start is called before the first frame update
        public override void Enter()
        {
            animator.SetTrigger("isMeleeAttacking");
            base.Enter();
        }

        public override void Exit()
        {
            nextState = new StateIdle(boss, bossRb, animator, player);
            animator.ResetTrigger("isMeleeAttacking");
            base.Exit();
        }


        public override void Update()
        {
            // Exit after animation
            // goto IDLE?
        }
    }
}