using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SideScrollerProject
{
    [CreateAssetMenu(menuName = "ShadedGames/BuffsDebuffs/Buffs/Ignite")]
    public class IgniteObject : Buff
    {
        public int damagePerSecond = 5; // depends on the health of
        public float tickRate = 0.5f;
        public override TimedBuff InitializeBuff(GameObject obj)
        {
           return new BuffIgnite(this,obj);
        }

    }
}