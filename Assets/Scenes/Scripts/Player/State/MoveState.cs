using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{
    [CreateAssetMenu(fileName = "New State", menuName = "ShadedGames/StateAbilityData/Move")]
    public class MoveState : StateData
    {
        public float move = 1f;
        public float targetDashDistance = 2f;
        private Rigidbody2D rb;
        public override void OnEnter(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            // Debug.Log("WTFFF");
            //   Movement dash = animator.GetComponentInParent<Movement>();
            Transform playerFace = animator.GetComponent<Transform>();
            rb = animator.GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(move * playerFace.localScale.x, rb.velocity.y), ForceMode2D.Impulse);
            Debug.Log($"Distance:{targetDashDistance}");
        }

        public override void OnExit(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            return;
        }

        public override void UpdateAbility(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            //  Debug.Log("MoveState");
        }








    }
}
