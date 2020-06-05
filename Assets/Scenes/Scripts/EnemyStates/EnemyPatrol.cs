using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{


    [CreateAssetMenu(fileName = "New Enemy Patrol State", menuName = "ShadedGames/StateAbilityData/Enemy/Patrol")]
    public class EnemyPatrol : StateData
    {

        private Vector2 currentPoint;
        private Transform thisTransform;
        public float moveSpeed;
        private Animator animator;

        Rigidbody2D rb;
        // public void MoveEnemy(Animator animator)
        // {
        //     rb = animator.gameObject.GetComponent<Rigidbody2D>();
        //     Flip();
        //     Vector2 target = new Vector2(playerPosition.position.x, rb.position.y);
        //     Vector2 newPos = Vector2.MoveTowards(rb.position, target, moveSpeed * Time.fixedDeltaTime);
        //     if (!e_Grounded)
        //     {
        //         animator.ResetTrigger("isMoving");
        //         Debug.Log("Aint Grounded");
        //         rb.AddForce(Vector2.down * 100);
        //     }
        //     else
        //     {
        //         rb.MovePosition(newPos);
        //     }
        // }


        public override void OnEnter(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            this.animator = animator;
            rb = this.animator.gameObject.GetComponent<Rigidbody2D>();
            currentPoint = this.animator.gameObject.GetComponent<Status>().nextPoint;
            thisTransform = this.animator.gameObject.transform;


        }

        public override void OnExit(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
           // animator.SetBool("isPatrolling", false);
           // animator.ResetTrigger("isPatrolling");
        }

        public override void UpdateAbility(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            //this.animator.GetComponent<Status>().Patrol(this.animator,thisTransform,currentPoint,rb,moveSpeed);


        }

        // Start is called before the first frame update

    }
}
