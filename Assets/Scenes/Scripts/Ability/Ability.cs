using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SideScrollerProject
{


    public abstract class Ability : ScriptableObject
    {

        public string aName = "New Ability";
        public string animatorParameter= "isCasting";
        public bool hasAnimation;
        public Sprite aSprite;
        //public AudioClip aSound;          // we don't have sounds yet
        public float aBaseCooldown = 1f;
        public int depletionValue;


        public abstract void Initialize(GameObject obj);
        public abstract void TriggerAbility();
    }
}
