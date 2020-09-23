using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SideScrollerProject
{


    public abstract class Ability : ScriptableObject
    {

        public string aName = "New Ability";
        public string animatorParameter = "isCasting";
        public bool hasAnimation;
        public bool castSomethingWhileCharging;
        public bool holdToCast;
        public bool quickCast;
        public bool castAfterCharge; // cast after achieving cast threshold
        public bool castOnPress; // cast after achieving cast threshold
        public bool depleteResourceWhileCharging;
        public int depletionValueWhileCharging;
        public float castLength; // button held length

        public Sprite aSprite;
        //public AudioClip aSound;          // we don't have sounds yet
        public float aBaseCooldown = 1f;
        public int depletionValue;
        public AbilityManager thisAbilityManager;


        public abstract void Initialize(GameObject obj);
        public abstract void CastAbility(); // cast something after releasing
        public abstract void ChargeAbility();// cast or charge ability while pressing
        public abstract void CastOnPressAbility(); // cast On Press 
    }
}
