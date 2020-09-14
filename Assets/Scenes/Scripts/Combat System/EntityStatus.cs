using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{
    public enum StatusEffects
    {
        Poisoned, Stunned, Cursed
    }

    public abstract class EntityStatus : MonoBehaviour, IDamageable
    {   // holds base status
        public BaseStatus currentStatus;
        public BuffReceiver buffReceiver;

        public abstract void TakeDamage(int baseDamage);
        // holds buff reference
        // Start is called before the first frame update
    }
}