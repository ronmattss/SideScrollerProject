using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SideScrollerProjectFSM
{
    public class State
    {
        public enum STATE
        {
            IDLE, MOVE, ATTACK, HURT, CHARGE, FLAMETHROWER, LASER, DEAD
        }
        public enum EVENT
        {
            ENTER, UPDATE, EXIT
        }

        public STATE name;
        protected EVENT stage;
        protected GameObject boss;
        protected Animator animator;
        protected Rigidbody2D bossRb;
        protected Transform player; // player ref
        protected State nextState;

        float attackDistance = 2.0f; ///????
        public State(GameObject _boss, Rigidbody2D _rb, Animator _bossAnimator, Transform _player)
        {
            boss = _boss;
            bossRb = _rb;
            animator = _bossAnimator;
            player = _player;
            stage = EVENT.ENTER;
        }

        public virtual void Enter()
        {
            Debug.Log("Entered State:" + name);
            stage = EVENT.UPDATE;
        }
        public virtual void Update()
        {
            stage = EVENT.UPDATE; // run update while no condition to exit
        }
        public virtual void Exit()
        {
            stage = EVENT.EXIT;
        }

        public State Process()
        {
            Debug.Log(stage);
            if (stage == EVENT.ENTER) Enter();
            if (stage == EVENT.UPDATE) Update();
            if (stage == EVENT.EXIT)
            {
                Exit();
                return nextState;
            }
            return this;
        }

    }
}
