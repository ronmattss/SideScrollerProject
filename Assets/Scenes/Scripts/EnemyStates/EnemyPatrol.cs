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

        public void Patrol(Animator animator)
        {

            Flip(thisTransform, currentPoint);
            Vector2 target = new Vector2(currentPoint.x, thisTransform.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, moveSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
//            Debug.Log(thisTransform.position.x <= currentPoint.x);

        }
        private void Flip(Transform self, Vector2 patrolPoint)
        {
            if (self != null)
                if (self.position.x > patrolPoint.x)
                {
                    self.localScale = new Vector3(-1, 1, 1);
                    // moveSpeed *= 1f;
                }
                else
                {
                    self.localScale = new Vector3(1, 1, 1);
                    // moveSpeed *= 1f;
                }
        }
        public override void OnEnter(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            rb = animator.gameObject.GetComponent<Rigidbody2D>();
            currentPoint = animator.gameObject.GetComponent<Status>().nextPoint;
            thisTransform = animator.gameObject.transform;


        }

        public override void OnExit(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool("isPatrolling", false);
            animator.ResetTrigger("isPatrolling");
        }

        public override void UpdateAbility(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            Patrol(animator);


        }

        // Start is called before the first frame update

    }
}
