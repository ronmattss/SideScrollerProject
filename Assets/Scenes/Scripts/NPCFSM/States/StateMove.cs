using UnityEngine;

namespace Scenes.Scripts.NPCFSM.States
{
    public class StateMove : StateMachine
    {
        // Start is called before the first frame update

        public StateMove(GameObject npc, Animator animator, Rigidbody2D npcRn) : base(npc, animator, npcRn)
        {
            currentStateName = CurrentState.Move;
        }

        public override void Enter()
        {
            animator.Play("EnemyMove");
            base.Enter();
        }

        public override void Update()
        {
            if (!npcStatus.isPlayerInSight)
            {
                Debug.Log(npcStatus.isPlayerInSight);
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