using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SideScrollerProjectFSM
{
    public class StateWalkingBurner : State
    {
        int randomNumber = 0;
        public StateWalkingBurner(GameObject _boss, Rigidbody2D _rb, Animator _bossAnimator, Transform _player) : base(_boss, _rb, _bossAnimator, _player)
        {
            name = STATE.ATTACK;
        }

        public override void Enter()
        {

            randomNumber = Random.Range(1, 3);
            animator.SetTrigger("isFlameCharging");
            switch (randomNumber)
            {
                case 1:
                    animator.SetInteger("pattern",randomNumber);
                    break;
                case 2:
                    animator.SetInteger("pattern",randomNumber);
                    break;
                default:
                    break;
            }

            base.Enter();
        }



        public override void Exit()
        {
            animator.ResetTrigger("isWalkingBurner");
            base.Exit();
        }



        public override void Update()
        {
            switch (randomNumber)
            {
                case 1:
                   // animator.SetTrigger("isStillBurner");
                    // Do some time logic here
                    break;
                case 2:
                  //  animator.SetTrigger("isWalkingBurner");
                    MoveEnemy();
                    // Time Logic then Exit
                    break;
                default:
                    break;
            }
        }
        public void MoveEnemy()
        {

            Flip();
            Vector2 target = new Vector2(player.position.x, bossRb.position.y);
            Vector2 newPos = Vector2.MoveTowards(bossRb.position, target, 2f * Time.fixedDeltaTime);
            // Debug.Log("Distance of HB to Player: " + Vector2.Distance(boss.transform.position, player.position));


            bossRb.MovePosition(newPos);

        }
        private void Flip()
        {
            if (player != null)
                if (boss.transform.position.x > player.position.x)
                {
                    boss.transform.localScale = new Vector3(-1, 1, 1);
                    // moveSpeed *= 1f;
                }
                else
                {
                    boss.transform.localScale = new Vector3(1, 1, 1);
                    // moveSpeed *= 1f;
                }
        }

        // Start is called before the first frame update


    }
}