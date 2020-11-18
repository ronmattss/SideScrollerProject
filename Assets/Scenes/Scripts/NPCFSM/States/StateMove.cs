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
            npcStatus.isNpcMoving = true;
            base.Enter();
        }

        public override void Update()
        {
            if (npcStatus.isPlayerInRange && npcStatus.isPlayerInSight)
            {
                nextState = new StateAttack(thisNpc,animator,npcRn);
                stateStatus = StateProcess.Exit;
            }
            else if (!npcStatus.isPlayerInSight)
            {
                Debug.Log(npcStatus.isPlayerInSight);
                nextState = new StateIdle(thisNpc, animator, npcRn);
                stateStatus = StateProcess.Exit;
            }
            else
            {
                MoveEnemy();
                base.Update();
            }
        }

        public override void Exit()
        {
            npcStatus.isNpcMoving = false;
            base.Exit();
        }
        
        private void MoveEnemy()
        {

            Flip();
            var position = npcRn.position;
            var target = new Vector2(npcStatus.target.position.x, position.y);
            var newPos = Vector2.MoveTowards(position, target, 2f * Time.fixedDeltaTime);
            // Debug.Log("Distance of HB to Player: " + Vector2.Distance(boss.transform.position, player.position));


            npcRn.MovePosition(newPos);

        }
        private void Flip()
        {
            if (npcStatus.target != null)
                thisNpc.transform.localScale = thisNpc.transform.position.x > npcStatus.target.position.x
                    ? new Vector3(-1, 1, 1)
                    : new Vector3(1, 1, 1);
        }
        
        
    }
}