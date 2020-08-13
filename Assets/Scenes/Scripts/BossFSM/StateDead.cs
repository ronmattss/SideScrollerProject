using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SideScrollerProjectFSM
{
    public class StateDead : State
    {
        float currentTime = 3;
        public StateDead(GameObject _boss, Rigidbody2D _rb, Animator _bossAnimator, Transform _player) : base(_boss, _rb, _bossAnimator, _player)
        {
            name = STATE.DEAD;
        }

        public override void Enter()
        {

            animator.SetTrigger("isDead");
            base.Enter();
        }



        public override void Exit()
        {
            boss.gameObject.SetActive(false);
            base.Exit();
        }



        public override void Update()
        {
            if (currentTime <= 0)
                stage = EVENT.EXIT;
            else
                currentTime -= Time.deltaTime;
           // base.Update();
        }
    }
}