using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SideScrollerProject
{
    // materialize when spirit eye is active
    // interact when spirit eye is active
    // handles if object can be interactable or not while eye is on or off
    // make interface for handling eyeStateEvent
    public class SpiritObject : MonoBehaviour
    {
        // Start is called before the first frame update
        public MonoBehaviour script;
        public SpriteRenderer spriteRenderer;
        public Collider2D colliderBox;
        [Header("If enabled components will be enabled while spirit form is disabled")]
        [Tooltip("Check if you want the components to be enabled on non-SpiritForm")]
        public bool inverseState;
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            DisableEnableComponent(PlayerManager.instance.thirdEyeState);

        }
        void DisableEnableComponent(bool enable)
        {
            if (inverseState)
            {
                if (script != null)
                    script.enabled = !enable;

                if (spriteRenderer != null)
                {
                    if (spriteRenderer.maskInteraction != SpriteMaskInteraction.None)
                        spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
                    // spriteRenderer.enabled = !enable;
                }
                if (colliderBox != null)
                    colliderBox.enabled = !enable;

            }
            else
            {
                if (script != null)
                    script.enabled = enable;
                if (spriteRenderer != null)
                {
                    //  spriteRenderer.enabled = enable;
                    // if (spriteRenderer.maskInteraction != SpriteMaskInteraction.None)
                    //     spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                }
                if (colliderBox != null)
                    colliderBox.enabled = enable;

            }

        }



    }
}
