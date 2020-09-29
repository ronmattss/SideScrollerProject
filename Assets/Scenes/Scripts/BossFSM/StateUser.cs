using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SideScrollerProjectFSM;


namespace SideScrollerProject
{
    /// <summary>
    /// Script that enables the enemy to use an FSM for deciding what actions to do next
    ///</summary>>
    public class StateUser : MonoBehaviour
    {
        [Header("State Properties")]
        public Transform player;
        public BossProperties binagoonanPropeties;
        Animator animator;
        Rigidbody2D rb;
        public Color c = Color.red;
        public State currentState;
        [Header("Boss Properties")]
        public int maxHp = 50;
        public int currentHp;
        public Material whiteFlash;
        public SpriteRenderer bossRenderer;
        void Awake()
        {
            currentHp = maxHp;
            bossRenderer = GetComponent<SpriteRenderer>();
            player = PlayerManager.instance.PlayerTransform;
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            currentState = new StateIdle(this.gameObject, rb, animator, player);

        }
        /// <summary>
        /// This function is called when the object becomes enabled and active.
        /// </summary>
        void OnEnable()
        {
            currentHp = maxHp;
            bossRenderer = GetComponent<SpriteRenderer>();
            player = PlayerManager.instance.PlayerTransform;
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
            Gizmos.color = c;
            Gizmos.DrawWireSphere(binagoonanPropeties.meleeAttackPosition.position, binagoonanPropeties.diameter);
            Gizmos.DrawWireSphere(binagoonanPropeties.flameHitPosition.transform.position, 7f);

            Vector2 direction = (this.gameObject.transform.localScale.x == -1 ? Vector2.left : Vector2.right);
            Vector2 origPos = binagoonanPropeties.meleeAttackPosition.position;
            Vector2 targetPos = new Vector2(origPos.x + (direction.x * 23), origPos.y);
            Debug.Log(origPos + " " + targetPos);
            Gizmos.DrawLine(origPos, targetPos);
        }

        public void WaitThenExitState()
        {
            StartCoroutine(DamageIndicator());
            StopCoroutine(DamageIndicator());
        }

        // This is here so that it can override the states late 
        public void TakeDamage(int damage)
        {
            currentHp -= damage;
            WaitThenExitState();
            if (currentHp <= 0)
            {
                currentState = new StateDead(this.gameObject, rb, animator, player);

            }
        }

        // Function that can be called in other states
        public void Flip()
        {
            if (player != null)
                if (this.gameObject.transform.position.x > player.position.x)
                {
                    this.gameObject.transform.localScale = new Vector3(-1, 1, 1);
                    // moveSpeed *= 1f;
                }
                else
                {
                    this.gameObject.transform.localScale = new Vector3(1, 1, 1);
                    // moveSpeed *= 1f;
                }
        }
        IEnumerator DamageIndicator()
        {
            var tempShade = bossRenderer.material;
            bossRenderer.material = whiteFlash;
            yield return new WaitForSecondsRealtime(0.09f); // gonna change to for Real Seconds
            bossRenderer.material = tempShade;
            yield return null;
        }
        IEnumerator Wait()
        {
            yield return new WaitForSecondsRealtime(0.5f);
            currentState.Exit();
            yield return null;
        }
    }
}