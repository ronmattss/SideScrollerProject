using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SideScrollerProject
{
    [CreateAssetMenu(fileName = "New Hit Effect", menuName = "ShadedGames/Hit Effect")]
    public class HitEffect : StateData

    {
        public float transitionValue = 0.8f;
        public ParticleSystem extraHit;
        private ParticleSystemRenderer extraHitRenderer;
        public override void OnEnter(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            extraHitRenderer = extraHit.GetComponent<ParticleSystemRenderer>();
            extraHitRenderer.flip = new Vector2(Mathf.RoundToInt(-animator.transform.localScale.x), 1);
            extraHit.Play();
            Debug.Log($"Transform  x of this object is {animator.transform.localScale.x}");
        }

        public override void OnExit(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            extraHit.Stop();
        }

        public override void UpdateAbility(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime >= transitionValue)
                Destroy(animator.gameObject);
        }
    }
}
