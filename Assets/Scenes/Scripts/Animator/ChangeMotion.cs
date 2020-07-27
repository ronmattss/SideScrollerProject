using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//since theres a lot of Animation clip available
// Why not just change the animation Motion
// What will this script do?
// Change Motion Clip when activating the third eye
namespace SideScrollerProject
{
    // Creating enums that will be never used :<
    public enum PlayerAnimations
    {
        PlayerIdle,
        PlayerWalk,
        PlayerDash,
        PlayerBlink,
        PlayerJump,
        PlayerPreAttack1,
        PlayerPreAttack2,
        PlayerPreAttack3,
        PlayerAttack1,
        PlayerAttack2,
        PlayerAttack3,
    }
    public class ChangeMotion : MonoBehaviour
    { // TEST Try Changing Idle Motion Clip
      // Start is called before the first frame update
      // IDK what I'm Doing

        // public AnimationClip[] characterStateAnimation; // for testing purposes 0 and 1 ONLY (Index)
        [SerializeField] private AnimationClip[] thirdEyeOpenAnimations; // Store Third eye animation clips here
        [SerializeField] AnimationClip[] baseAnimations; // Store Base animation clips here
        protected AnimationClipOverrides clipOverrides;

        public Animator playerAnimator;
        public MaskController thirdEyeController;
        public AnimatorOverrideController animatorOverrideController;
        void Start()
        {
            playerAnimator = PlayerManager.instance.GetPlayerAnimator();
            animatorOverrideController = new AnimatorOverrideController(playerAnimator.runtimeAnimatorController);

            playerAnimator.runtimeAnimatorController = animatorOverrideController;
            clipOverrides = new AnimationClipOverrides(animatorOverrideController.overridesCount);

            animatorOverrideController.GetOverrides(clipOverrides);
            //store base animation clips
            baseAnimations = animatorOverrideController.animationClips;
        }

        // Update is called once per frame
        void Update()
        {

        }
        // Change the AnimationMotion Bla Bla
        // Find all Base Animations
        // if it contains EyeOn ignore it
        // iterate baseAnimations  change it one by one
        // vice versa
        public void ChangeAnimationClip()
        {
            string trimWords = "EyeOn";
            // First is Loop all Base Animations
            // if name match swap the animation
            // 
            //  animatorOverrideController["PlayerIdle"] = thirdEyeOpenAnimations[0];
            for (int i = 0; i < baseAnimations.Length; i++) // slow for loop maybe change to LINQ??? idk 
            {
                for (int j = 0; j < thirdEyeOpenAnimations.Length; j++)
                {

                    if (baseAnimations[i].name == thirdEyeOpenAnimations[j].name.Replace(trimWords, "").ToString())
                    {
                        baseAnimations[i] = thirdEyeOpenAnimations[j];
                        Debug.Log($"{baseAnimations[i]} == {thirdEyeOpenAnimations[j].name.Replace(trimWords, "").ToString()}");
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }
        public void ChangeMultipleAnimationClips()
        {
            // what I understand is
            // idk 
            // List of keyvalue pairs overrides Multiple Animation Clips by using a list lmao idk
            // what dis will do?
            // stored animations 
            string trimWords = "EyeOn";
            if (thirdEyeController.thirdEyeOn)
            {

                // eye is on then do this else do that
                foreach (var clip in thirdEyeOpenAnimations) // iterate each clip
                {
                    clipOverrides[clip.name.Replace(trimWords, "")] = clip; // check if the name is same with the default clip
                }
                animatorOverrideController.ApplyOverrides(clipOverrides);
            }
            else // dis is wrong lmao
            {
                foreach (var clip in baseAnimations) // iterate each clip
                {
                    clipOverrides[clip.name] = clip; // check if the name is same with the default clip
                }
                animatorOverrideController.ApplyOverrides(clipOverrides);
            }
        }




    }
}
