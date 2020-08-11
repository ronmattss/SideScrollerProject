using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{
    [CreateAssetMenu(fileName = "New BossBasicAttack State", menuName = "ShadedGames/StateAbilityData/Enemy/BossBasicAttack")]
    public class BossBasicAttack : StateData
    {
        StateUser meleeState;
        // Start is called before the first frame update
        public override void OnEnter(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            meleeState = animator.gameObject.GetComponent<StateUser>();
            HitPlayer(animator);

        }

        public override void OnExit(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        void HitPlayer(Animator animator)
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(meleeState.binagoonanPropeties.meleeAttackPosition.position, meleeState.binagoonanPropeties.diameter, new Vector2(animator.gameObject.transform.localScale.x, 0), meleeState.binagoonanPropeties.playerLayer);
            foreach (var hit in hits)
            {
                if (hit.collider == null) return;
                else
                {
                    if (hit.collider.CompareTag("Player"))
                        PlayerManager.instance.GetPlayerStatus().TakeDamage(10, animator);
                }
            }
        }
    }
}