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



        private void InitializeFields()
        {
            transform = npcStatus.transform;
            raycastOrigin = npcStatus.raycastOrigin;
            range = npcStatus.range;
            playerLayer = npcStatus.playerLayer;
        }

        #region AtAnyStateLogic
        // AI logic that is always called on whatever state the AI is currently at
        // These are methods that actively Checks conditions like ground checking  
        private void CheckGround()
        {
            // TODO: Double check if ground checking is redundant
            npcStatus.CheckGround();
            npcStatus.CheckGroundPoint();
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
                var distanceBetweenNpcAndPlayer =
                    Vector3.Distance(thisNpc.transform.position, target.position);
                npcStatus.isPlayerInRange = distanceBetweenNpcAndPlayer <= npcStatus.attackRange;
                //Debug.Log("Distance of NPC to the Player: "+distanceBetweenNpcAndPlayer);
            }
            else
            {
                target = null;
                npcStatus.isPlayerInSight = false;
                npcStatus.isPlayerInRange = false;

            }

            npcStatus.target = target;    // npc target acquired
            //<-->
        }
        
        private void EnemyInRange(LayerMask playerMask, Animator animator)
        {

            if (isRange)
            {
                Vector2 raycastDirection = this.transform.localScale.x == -1 ? Vector2.left : Vector2.right;
                RaycastHit2D hit = Physics2D.Linecast(raycastOrigin.position, new Vector2(raycastDirection.x * range + raycastOrigin.position.x, raycastOrigin.position.y), playerLayer);
                if (target != null && !npcStatus.travelsOnOneAxis)
                {
                    if (Vector2.Distance(this.transform.position, target.position) <= range)
                        hit = Physics2D.Linecast(raycastOrigin.position, new Vector2(raycastDirection.x + target.position.x, target.position.y), playerLayer);
                    Debug.DrawLine(raycastOrigin.position, new Vector2(raycastDirection.x + target.position.x, target.position.y), Color.red);
                }
                Debug.DrawLine(raycastOrigin.position, new Vector2(raycastDirection.x * range + raycastOrigin.position.x, raycastOrigin.position.y), Color.red);
                //if (hit.collider != null)
                // Debug.Log("Name: " + hit.collider.name);
                if (hit.collider != null) // overwrite if projectile
                    if (hit.collider.CompareTag("Player")) // well idk wat i DID WIP
                    {
                        npcStatus.isPlayerInRange = true;
                    }
                    else if (hit.collider == null)
                    {
                        target = null;
                        animator.SetBool("isMoving", false);
                    }
            }
            else
            {
                Collider2D[] playerCollider = Physics2D.OverlapCircleAll(npcStatus.attackPoint.position, npcStatus.attackRange, playerLayer);
                foreach (Collider2D player in playerCollider)
                {
                    if (player.CompareTag("Player") && player.GetType() == typeof(CapsuleCollider2D))
                    {
                        animator.SetBool("playerInRange", true);

                        animator.SetBool("isMoving", false);
                        // Wizard Targetting, Refactoring....
                        if (npcStatus.targetLock == false)
                        {
                            npcStatus.targetInitialPosition = player.transform;
                            // position = player.transform.position;
                            npcStatus.playerPosition = player.gameObject;
                            npcStatus.targetLock = true;
                        }

                        return;
                    }
                    else if (player == null)
                    {
                        animator.SetBool("isMoving", false);
                    }
                }
            }
        }
        #endregion
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