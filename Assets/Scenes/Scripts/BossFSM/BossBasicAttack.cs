using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{
    [CreateAssetMenu(fileName = "New BossBasicAttack State", menuName = "ShadedGames/StateAbilityData/Enemy/BossBasicAttack")]
    public class BossBasicAttack : StateData
    {
        StateUser meleeState;
        public bool shake = false;

        public override void OnEnter(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            meleeState = animator.gameObject.GetComponent<StateUser>();
            meleeState.c = Color.blue;
            meleeState.Flip();
            HitPlayer(animator);
            if(shake)
            {
               CameraManager.instance.Shake(10,3f);
            }

        }

        public override void OnExit(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        void HitPlayer(Animator animator)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(meleeState.binagoonanPropeties.meleeAttackPosition.position, meleeState.binagoonanPropeties.diameter, meleeState.binagoonanPropeties.playerLayer);

            foreach (var hit in hits)
            {
                if (hit == null) return;
                else
                {
                    if (hit.CompareTag("Player") && hit.GetType() == typeof(CapsuleCollider2D))
                    {

                        PlayerManager.instance.PlayerStatus.TakeDamage(20, animator);
                        hits = null;
                        return;
                    }
                }
            }
        }

    }
}