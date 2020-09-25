using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace SideScrollerProject
{
    public class BuffMultiplyDamage : TimedBuff
    {
        private GameObject entityObject;
        public int extraDamage = 10;
        public BuffMultiplyDamage(Buff buff, GameObject obj) : base(buff, obj)
        {
            entityObject = obj;
            obj = entityObject;
            extraDamage = (buff as MultiplyDamageObject).additionalDamage;
        }

        public override void End()
        {
            Debug.Log("Entity: "+entityObject.gameObject.name);
            entityObject.GetComponent<PlayerStats>().listOfBuffs.Remove(this);
            Debug.Log("SPirit Buff Ended");

        }

        protected override void ApplyEffect()
        {

            entityObject.GetComponent<PlayerStats>().listOfBuffs.Add(this);
        }

        protected override void EffectPerSecond(float delta)
        {
            return;
           // throw new NotImplementedException();
        }

        // Start is called before the first frame update

    }
}
