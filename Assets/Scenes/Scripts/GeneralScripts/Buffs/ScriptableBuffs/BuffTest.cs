using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{

    [CreateAssetMenu(menuName = "ShadedGames/BuffsDebuffs/Buffs/InstaKill")]

    public class BuffTest : Buff
    {// Instead of calling TimedBuff
    // call it here
        public override TimedBuff InitializeBuff(GameObject obj)
        {
            Debug.Log("initialized Object: "+obj.name);
            return new BuffInstaKill(this, obj);
        }

        public override TimedBuff InitializeBuff()
        {
            return new BuffInstaKill();
        }
    }
}
