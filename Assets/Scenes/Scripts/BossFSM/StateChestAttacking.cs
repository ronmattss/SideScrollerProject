using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SideScrollerProjectFSM
{
    public class StateChestAttacking : State
    {
        float time = 3;
        float current;
        public StateChestAttacking(GameObject _boss, Rigidbody2D _rb, Animator _bossAnimator, Transform _player) : base(_boss, _rb, _bossAnimator, _player)
        {
            name= STATE.LASER;
        }

        public override void Enter()
        {
            animator.SetTrigger("isChestAttacking");
            current = time;
            base.Enter();
        }



        public override void Exit()
        {
            animator.ResetTrigger("isChestAttacking");
            base.Exit();
        }
        public override void Update()
        {
            if (current <= 0)
            {
                nextState = new StateIdle(boss, bossRb, animator, player);
                stage = EVENT.EXIT;
            }
            else
            {
                current -= Time.fixedDeltaTime;
            }
        }


    }
}