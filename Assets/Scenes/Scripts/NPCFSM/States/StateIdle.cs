using SideScrollerProject;
using UnityEngine;

namespace Scenes.Scripts.NPCFSM.States
{
    public class StateIdle : StateMachine
    {
        public StateIdle(GameObject npc, Animator animator, Rigidbody2D npcRn) : base(npc, animator, npcRn)
        {
            currentStateName = CurrentState.Idle;
        }

        public override void Enter()
        {
            Debug.Log($"Current State: {currentStateName.ToString()}");
            
            animator.Play("EnemyIdle");
            base.Enter();
        }

        public override void Update()
        {
            //code goes brrrt;
          
            // Condition To Exit Here
            if (npcStatus.isPlayerInSight)
            {
                nextState = new StateMove(thisNpc, animator, npcRn);
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