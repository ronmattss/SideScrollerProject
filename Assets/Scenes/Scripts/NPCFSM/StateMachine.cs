using System;
using SideScrollerProject;
using UnityEngine;

namespace Scenes.Scripts.NPCFSM
{
    [Serializable]
    public class StateMachine
    {
        [SerializeField] private StateMachine nextStateMachine;
        public CurrentState currentStateName;
        protected StateProcess stage;
        public Status npcStatus;
        protected Animator animator;
        protected GameObject thisNpc;
        protected Rigidbody2D npcRn; // player ref
        protected StateMachine nextState;


        protected StateProcess stateStatus;
        private bool isRange;
        private Transform transform;
        private Transform raycastOrigin;
        private float range;
        private Transform target;
        private int playerLayer;

        public StateMachine(GameObject npc,Animator animator,
            Rigidbody2D npcRn)
        {
            thisNpc = npc;
            this.animator = animator;
            this.npcRn = npcRn;
            npcStatus = npc.GetComponent<Status>();
            InitializeFields();
            stateStatus = StateProcess.Enter;
        }


        public virtual void Enter()
        {
            stateStatus = StateProcess.Update;
        }

        public virtual void Update()
        {
            CheckGround(); // always check if AI is touching ground
            ScanEnemy();
            stateStatus = StateProcess.Update; // run update while no condition to exit
        }

        public virtual void Exit()
        {
            stateStatus = StateProcess.Exit;
        }

        public StateMachine Process()
        {
            Debug.Log(LogStateStatus(currentStateName, stateStatus));
            if (stateStatus == StateProcess.Enter)
                Enter();
            if (stateStatus == StateProcess.Update)
                Update();
            if (stateStatus == StateProcess.Exit)
            {
                Exit();
                return nextState;
            }

            return this;
        }

        public string LogStateStatus(CurrentState curState,
            StateProcess curStateProcess)
        {
            return "Current State: " + curState.ToString() + " Current State Process: " + curStateProcess.ToString();
        }

        private void CheckGround()
        {
            // TODO: Double check if ground checking is redundant
            npcStatus.CheckGround();
            npcStatus.CheckGroundPoint();
        }

        private void InitializeFields()
        {
            transform = npcStatus.transform;
            raycastOrigin = npcStatus.raycastOrigin;
            range = npcStatus.range;
            playerLayer = npcStatus.playerLayer;
        }

        private void ScanEnemy()
        {
            //This Raycast In a Direction Code <->
            var raycastDirection = transform.localScale.x == -1
                ? Vector2.left
                : Vector2.right;
            var position = raycastOrigin.position;
            var hit = Physics2D.Linecast(position,
                new Vector2(raycastDirection.x * range + position.x,
                    position.y),
                playerLayer);
            Debug.DrawLine(position,
                new Vector2(raycastDirection.x * range + position.x,
                    position.y),
                Color.blue);
            // <->
            // Debug.Log("Name: " + hit.collider.name);

            // Check if something is hit <-->
            if (hit.collider != null)
            {
                if (!hit.collider.CompareTag("Player")) return;
                target = hit.collider.transform; // assign target if raycast collided with player
                npcStatus.isPlayerInSight = true;
            }
            else
            {
                target = null;
                npcStatus.isPlayerInSight = false;
                
            }

            npcStatus.target = target;    // npc target acquired
            //<-->
        }

        public enum CurrentState
        {
            Idle,
            Move,
            Attack,
            Hurt,
            Charge,
            Dead
        }

        public enum StateProcess
        {
            Enter,
            Update,
            Exit
        }
    }
}