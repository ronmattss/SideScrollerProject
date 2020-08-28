using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{
    public class AttackTriggerManager : MonoBehaviour
    {
        public static AttackTriggerManager instance;
        public Animator playerAnimator;
        // Start is called before the first frame update
        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        public TriggerAttackAnimation attackAnimationController;
        void Awake()
        {
//            playerAnimator = PlayerManager.instance.GetPlayerAnimator();
            instance = this;
        }
        void Start()
        {
            attackAnimationController = new TriggerAttackAnimation(playerAnimator);

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}