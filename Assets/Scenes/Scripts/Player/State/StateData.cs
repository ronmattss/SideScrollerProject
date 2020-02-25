using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{
    public abstract class StateData : ScriptableObject
    {
        public float duration;

        public abstract void OnEnter(BaseState state, Animator animator, AnimatorStateInfo stateInfo);
        public abstract void UpdateAbility(BaseState state, Animator animator, AnimatorStateInfo stateInfo);
        public abstract void OnExit(BaseState state, Animator animator, AnimatorStateInfo stateInfo);




    }
}
