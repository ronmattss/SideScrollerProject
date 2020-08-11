using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SideScrollerProjectFSM;


namespace SideScrollerProject
{
    public class StateUser : MonoBehaviour
    {
        public Transform player;
        public BossProperties binagoonanPropeties;
        Animator animator;
        Rigidbody2D rb;
        public State currentState;
        void Start()
        {
            player = PlayerManager.instance.GetPlayerTransform();
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            currentState = new StateIdle(this.gameObject, rb, animator, player);

        }

        // Update is called once per frame
        void Update()
        {
            currentState = currentState.Process();// returns a new state
                                                  // Debug.Log(currentState.name);

        }

        /// <summary>
        /// Callback to draw gizmos that are pickable and always drawn.
        /// </summary>
        void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(binagoonanPropeties.meleeAttackPosition.position, binagoonanPropeties.diameter);
            Gizmos.DrawWireSphere(binagoonanPropeties.flameHitPosition.transform.position, 5f);
        }

        public void WaitThenExitState()
        {
            StartCoroutine(Wait());
        }
        IEnumerator Wait()
        {
            yield return new WaitForSecondsRealtime(0.5f);
            currentState.Exit();
            yield return null;
        }
    }
}