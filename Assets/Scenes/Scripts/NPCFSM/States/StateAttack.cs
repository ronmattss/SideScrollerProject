using UnityEngine;

namespace Scenes.Scripts.NPCFSM.States
{
    public class StateAttack : StateMachine
    {
        public StateAttack(GameObject npc, Animator animator, Rigidbody2D npcRn) : base(npc,animator, npcRn)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}