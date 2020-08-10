using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProjectFSM
{
    public class StateFlameStill : State
    {
        public StateFlameStill(GameObject _boss, Rigidbody2D _rb, Animator _bossAnimator, Transform _player) : base(_boss, _rb, _bossAnimator, _player)
        {

        }

        public override void Enter()
        {
            base.Enter();
        }

   
        public override void Exit()
        {
            base.Exit();
        }



        public override void Update()
        {
            
        }
    }
}