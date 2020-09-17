using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{
    [CreateAssetMenu(menuName = "ShadedGames/BuffsDebuffs/Buffs/MultiplyDamage")]

    public class MultiplyDamageObject : Buff
    {
        
        // Instead of calling TimedBuff
        // call it here
        public int additionalDamage =10;
        public override TimedBuff InitializeBuff(GameObject obj)
        {
            Debug.Log("initialized Object: " + obj.name);
            return new BuffMultiplyDamage(this, obj);
        }
    }
}