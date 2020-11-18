using UnityEngine;

namespace Scenes.Scripts.NPCFSM.States
{
    public class StateAttack : StateMachine
    {
        public StateAttack(GameObject npc, Animator animator, Rigidbody2D npcRn) : base(npc, animator, npcRn)
        {
            
            currentStateName = CurrentState.Attack;
        }

        public override void Enter()
        {
            npcStatus.isNpcAttacking = true;
            Debug.Log("Attacking: "+npcStatus.isNpcAttacking);
            animator.Play("EnemyAttack");
            base.Enter();
        }

        public override void Update()
        {
            if (npcStatus.isNpcAttacking == false)
            {
                if (npcStatus.isPlayerInRange)
                {
                    nextState = new StateAttack(thisNpc, animator, npcRn);
                }
                else
                {
                    nextState = new StateMove(thisNpc, animator, npcRn);
                }

                stateStatus = StateProcess.Exit;
            }
            else
            {
                base.Update();
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}