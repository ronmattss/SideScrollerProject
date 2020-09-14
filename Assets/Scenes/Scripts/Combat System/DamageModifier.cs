using System;
using System.Collections;
using UnityEngine;


namespace SideScrollerProject
{
    [Serializable]
    // class that accepts/gives damage
    // modifies it base on the current status of the receiver/giver
    // if there are buffs/debuffs from the attack
    // add it to the buff receiver 
    // calculate damage based on the buff, initial damage
    // resulting damage will then be passed on the status
    // finished
    public class DamageModifier : IDoDamage
    {    // Status reference
         // buff/debuff reference


        public void DoDamage(int initialDamage, GameObject target)
        {
            EntityStatus targetStatus = target.GetComponent<EntityStatus>(); // calculate damage here
            int calculatedDamage = initialDamage;
            // get Idamageable
            // calculate damage
            // add buff if theres any

            targetStatus.TakeDamage(initialDamage);
        }
        int CalculateDamage()
        {
            return 0;
        }
    }
}