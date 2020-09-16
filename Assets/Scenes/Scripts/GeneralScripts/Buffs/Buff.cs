using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Base Buff class
// affects the status of the player and enemies 
// sits as a component
// extends to buffGiver/ buff Receiver
// will be called if entity needs to be buffed or debuffed
// can also be called by abilities and triggers
namespace SideScrollerProject
{
    public enum StatusEffect
    {   //
        poison,
        stun,

    }
    // https://www.jonathanyu.xyz/2016/12/30/buff-system-with-scriptable-objects-for-unity/
    // Reference
    [System.Serializable]
    public abstract class Buff : ScriptableObject
    {
        // name of Buff
        // type of buff: buff or debuff
        // duration of buff
        // isBuffStackable: resets timer
        // Components affected???
        public string buffName;
        public float buffDuration;
        public float buffTick;
        public bool hasDuration;
        public bool isBuffStackable; // if Stackable extend the duration if not reset the counter
        public bool isEffectStacked;
        public abstract TimedBuff InitializeBuff(GameObject obj);
       // public abstract TimedBuff InitializeBuff();




    }
}
