using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SideScrollerProject;

namespace SideScrollerProjectFSM
{
    public class StateChestAttacking : State
    {
        StateUser bossRef;
        float time = 6;
        float current;
        float hitTickRate = 0.1f;
        float currentTickRate;

        public StateChestAttacking(GameObject _boss, Rigidbody2D _rb, Animator _bossAnimator, Transform _player) : base(_boss, _rb, _bossAnimator, _player)
        {
            name = STATE.LASER;
        }

        public override void Enter()
        {
            animator.SetTrigger("isChestAttacking");
            bossRef = boss.GetComponent<StateUser>();
            bossRef.binagoonanPropeties.chestAttackPosition.gameObject.SetActive(true);
            current = time;
            base.Enter();
        }



        public override void Exit()
        {
            animator.ResetTrigger("isChestAttacking");
             bossRef.binagoonanPropeties.chestAttackPosition.gameObject.SetActive(false);
            base.Exit();
        }
        public override void Update()
        {
            if (current <= 0)
            {
                nextState = new StateIdle(boss, bossRb, animator, player);
                stage = EVENT.EXIT;
            }
            else
            {
                current -= Time.deltaTime;
                currentTickRate -= Time.deltaTime;
                Burn();
            }
        }

        public void Burn()
        {
            Vector2 direction = (this.boss.transform.localScale.x == -1 ? Vector2.left : Vector2.right);
            Vector2 origPos = bossRef.binagoonanPropeties.meleeAttackPosition.position;
            Vector2 targetPos = new Vector2(origPos.x + (direction.x * 23), origPos.y);
            RaycastHit2D[] hit = Physics2D.LinecastAll(origPos, targetPos, bossRef.binagoonanPropeties.playerLayer);

            foreach (var h in hit)
            {
                if (h.collider == null) return;
                else
                {
                    Debug.Log("Laser Burn");
                    if (h.collider.gameObject.CompareTag("Player") && currentTickRate <= 0)
                    {
                        Debug.Log("player?: " + h.collider.name);
                        PlayerManager.instance.GetPlayerStatus().TakeDamage(10);
                        currentTickRate = hitTickRate;
                    }
                }
            }

        }


    }
}