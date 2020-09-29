using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{
    [CreateAssetMenu(fileName = "New State", menuName = "ShadedGames/StateAbilityData/Move")]
    public class MoveState : StateData
    {
        public float move = 100f;
        public float targetDashDistance = 2f;
        private Rigidbody2D rb;
        public override void OnEnter(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        { 
            PlayerManager.instance.PlayerAction.canRun = false;
            // Debug.Log("WTFFF");
            
            //dash.horizontalMovement =0;
            Transform playerFace = animator.GetComponent<Transform>();
            rb = animator.GetComponent<Rigidbody2D>();
           // Vector2 force = new Vector2(move * playerFace.localScale.x, rb.velocity.y +5);
           // rb.velocity = force;
            //rb.AddForce(force, ForceMode2D.Impulse);
//dash.SmallDash();
//Debug.Log($"Distance:{targetDashDistance} Force: {force }");
        }

        public override void OnExit(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            return;
        }

        public override void UpdateAbility(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            PlayerManager.instance.PlayerAction.horizontalMovement =0;
            //  Debug.Log("MoveState");
        }








    }
}
