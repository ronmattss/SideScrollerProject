using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{

    [CreateAssetMenu(menuName = "Abilities/Player/Spirit Eye")]
    public class SpiritEye : Ability
    {
        PlayerStatus playerStatus;
        MaskController thirdEye;
        Animator animator;
        ChangeMotion motion;
        [SerializeField] public Buff Buff;
        public Sprite eyeClosed;
        public Sprite eyeOpen;
        TimedBuff timedBuff;

        public override void CastAbility()
        {

            thirdEye.ResizeObject();
            animator.SetBool("isEyeOn", thirdEye.thirdEyeOn);
            motion.ChangeMultipleAnimationClips();
            Debug.Log("Buff: " + timedBuff.buff.buffName);
            if (thisAbilityManager.abilityActive)
            {
                thisAbilityManager.abilityActive = false;
                thisAbilityManager.abilityImage.sprite = eyeClosed;
                //.End();

                //timedBuff.isFinished = true;
                playerStatus.gameObject.GetComponent<BuffReceiver>().RemoveBuff(Buff);
                // playerStatus.playerIngameStats.listOfBuffs.Remove((timedBuff as BuffMultiplyDamage));
                // playerStatus.gameObject.GetComponent<PlayerStats>().RemoveModifier(timedBuff);
                Debug.Log("Off " + timedBuff.isFinished);

            }
            else
            {

                thisAbilityManager.abilityImage.sprite = eyeOpen;
                Debug.Log("Damage: " + (timedBuff as BuffMultiplyDamage).extraDamage);
                playerStatus.gameObject.GetComponent<BuffGiver>().receiver = playerStatus.gameObject;
                playerStatus.gameObject.GetComponent<BuffGiver>().SelfBuff(Buff);
                thisAbilityManager.abilityActive = true;

            }
            //motion.ChangeAnimationClip();
        }

        public override void CastOnPressAbility()
        {
            //  throw new System.NotImplementedException();
        }

        public override void ChargeAbility()
        {
            //s  throw new System.NotImplementedException();
        }

        public override void Initialize(GameObject obj)
        {
            playerStatus = obj.GetComponent<PlayerStatus>();
            thirdEye = obj.GetComponent<Actions>().thirdEye;
            motion = obj.GetComponent<Actions>().motion;
            animator = obj.GetComponent<Actions>().animator;
            timedBuff = Buff.InitializeBuff(this.playerStatus.gameObject);
            Debug.Log("Name of Buff: " + timedBuff.buff.buffName);
            // Start is called before the first frame update
        }
    }
}