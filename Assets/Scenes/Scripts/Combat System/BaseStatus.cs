using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BaseStatusData", menuName = "ShadedGames/Base Status")]
[Serializable]
public abstract class BaseStatus : ScriptableObject
{
    public int maxHealth;
    public int initialDamage;
    // Add this to the Player Status and Status
    // damage for basic attacks
    //Attack arguments????
    // What happens in an attack
    //1. Accept a base damage: example: 10
    //2. Check if the attack has modifiers(Buffs/Debuffs)
    //3. final damage  is based on buffs/ debuffs
    //4. Check if there is buff/debuff that will be included in the attack ()
    //5. If it hits 

    // Damage Side    
    //6. Check the current status effects (Buff/Debuff) of the receiver
    //7. Calculate the damage and possible buffs the receiver will take or give
    //8. Update the current status of the receiver 

    // base Damage with no buffs or debuffs that will be applied

}