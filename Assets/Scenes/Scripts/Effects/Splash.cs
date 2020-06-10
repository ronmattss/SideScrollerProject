using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{
    [CreateAssetMenu(fileName = "New Splash", menuName = "ShadedGames/States/Splash")]
    public class Splash : StateData
    {

       public bool isOmniHit = false;
        public override void OnEnter(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnExit(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            Destroy(animator.gameObject);

        }


        public override void UpdateAbility(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime >= 0.6 && isOmniHit == true)
            {

                AudioManager.instance.Play("OmniHitsSFX");
            }
        }
    }
}
