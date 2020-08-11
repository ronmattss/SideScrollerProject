using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SideScrollerProject;

namespace SideScrollerProjectFSM
{
    public class StateWalkingBurner : State
    {
        int randomNumber = 0;
        float time = 5;
        float current;
        StateUser bossRef;
        public StateWalkingBurner(GameObject _boss, Rigidbody2D _rb, Animator _bossAnimator, Transform _player) : base(_boss, _rb, _bossAnimator, _player)
        {
            name = STATE.FLAMETHROWER;
        }

        public override void Enter()
        {//isChestCharging
            Debug.Log("Is this entering after charging");
            current = time;
            bossRef = boss.GetComponent<StateUser>();
            bossRef.binagoonanPropeties.flamePosition.SetActive(true);
            // nextState = new StateIdle(boss, bossRb, animator, player);

            Debug.Log("Pattern:" + animator.GetInteger("pattern"));
            switch (animator.GetInteger("pattern"))
            {
                case 0:
                    // animator.SetTrigger("isStillBurner");
                    // Do some time logic here
                    // Do some damage Calculation
                    current = 4f;
                    break;
                case 1:
                    //  animator.SetTrigger("isWalkingBurner");
                    current = 4;
                    // Do some damage Calculation
                    // Time Logic then Exit
                    break;
                default:
                    break;
            }
            Debug.Log("Line Before Updating Pattern should be: " + animator.GetInteger("pattern"));
            base.Enter();
        }



        public override void Exit()
        {
            bossRef.binagoonanPropeties.flamePosition.SetActive(false);
            animator.ResetTrigger("isWalkingBurner");
            base.Exit();
        }



        public override void Update()
        {

            //  nextState = new StateIdle(boss, bossRb, animator, player);
            if (current <= 0)
            {

                nextState = new StateIdle(boss, bossRb, animator, player);
                //nextState = new StateIdle(boss, bossRb, animator, player);
                stage = EVENT.EXIT;
            }
            else
            {
                Burn();
                current -= Time.fixedDeltaTime;
                switch (animator.GetInteger("pattern"))
                {
                    case 0:
                        // animator.SetTrigger("isStillBurner");
                        // Do some time logic here
                        // Do some damage Calculation
                        break;
                    case 1:
                        //  animator.SetTrigger("isWalkingBurner");
                        MoveEnemy();
                        // Do some damage Calculation
                        // Time Logic then Exit
                        break;
                    default:
                        break;
                }
                Debug.Log(" Pattern should be: " + animator.GetInteger("pattern"));
            }



            nextState = new StateIdle(boss, bossRb, animator, player);
            //stage = EVENT.EXIT;

        }
        public void Burn()
        {
            Vector2 direction = new Vector2(boss.transform.localScale.x, 0);
            RaycastHit2D[] hit = Physics2D.CircleCastAll(bossRef.binagoonanPropeties.flameHitPosition.transform.position, 5f, direction, bossRef.binagoonanPropeties.playerLayer);

            foreach (var h in hit)
            {
                if (h.collider == null) return;
                else
                {
                    Debug.Log("BURN BURN BURN");
                    if (h.collider.gameObject.CompareTag("Player"))
                    {
                        Debug.Log("player?: " + h.collider.name);
                        PlayerManager.instance.GetPlayerStatus().TakeDamage(1);
                    }
                }
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