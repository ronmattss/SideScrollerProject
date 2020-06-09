using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{
    [CreateAssetMenu(fileName = "New OmniSlashSFX", menuName = "ShadedGames/States/OmniSlashSFX")]
    public class OmniSlashSFX : StateData
    {

        bool didPlay = true;
        public override void OnEnter(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            AudioManager.instance.Play("Swoosh1");
        }

        public override void OnExit(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {


        }


        public override void UpdateAbility(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
          //  if (stateInfo.normalizedTime >= 0.85f)
        //       AudioManager.instance.Play("HitSFX");
////
        }
    }
}