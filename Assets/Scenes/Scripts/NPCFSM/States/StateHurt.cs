using UnityEngine;

namespace Scenes.Scripts.NPCFSM.States
{
    public class StateHurt : StateMachine
    {
        public StateHurt(GameObject npc, Animator animator, Rigidbody2D npcRn) : base(npc, animator, npcRn)
        {
            npcStatus.isNpcHurt = true;
            npcStatus.flinchCounter = currentFlinchCounter;
            currentStateName = CurrentState.Hurt;
        }

        public override void Enter()
        {
            animator.Play("EnemyHurt");

            base.Enter();
        }

        public override void Update()
        {
            // Just wait for the animation to end
            if (npcStatus.isNpcHurt == false)
                if (npcStatus.currentHealth <= 0)
                {
                    nextState = new StateDead(thisNpc, animator, npcRn);
                    stateStatus = StateProcess.Exit;
                    return;
                }
                else
                {
                    nextState = new StateIdle(thisNpc, animator, npcRn);
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