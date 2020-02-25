using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{
    public class BaseState : StateMachineBehaviour
    {
        public List<StateData> listStateData = new List<StateData>();

        public void UpdateAll(BaseState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            foreach (StateData d in listStateData)
            {
                d.UpdateAbility(characterState, animator, stateInfo);
            }
        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (StateData d in listStateData)
            {
                d.OnEnter(this,animator,stateInfo);
            }
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            UpdateAll(this, animator, stateInfo);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (StateData d in listStateData)
            {
                d.OnExit(this,animator,stateInfo);
            }
        }

        private GameObject parentObject;
        public GameObject GetParentObject(Animator animator)
        {
            if (parentObject == null)
            {
                parentObject = animator.gameObject;
            }
            return parentObject;
        }
    }
}

