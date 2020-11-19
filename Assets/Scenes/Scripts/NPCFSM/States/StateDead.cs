using UnityEngine;

namespace Scenes.Scripts.NPCFSM.States
{
    public class StateDead : StateMachine
    {
        public StateDead(GameObject npc, Animator animator, Rigidbody2D npcRn) : base(npc, animator, npcRn)
        {
            currentStateName = CurrentState.Dead;
        }
        // Start is called before the first frame update

        // Update is called once per frame
        public override void Enter()
        {
            npcStatus.gameObject.layer = 0; // remove 
            npcStatus.gameObject.GetComponent<Collider>().enabled = false;
            animator.Play("EnemyDeath");
            base.Enter();
        }

        public override void Update()
        {
            if(npcStatus.isNpcDead)  base.Update();
            else
            {
                stateStatus = StateProcess.Exit;    
            }
            
           
        }

        public override void Exit()
        {
            npcStatus.DestroyNpc();
            base.Exit();
           
        }




    }
}
