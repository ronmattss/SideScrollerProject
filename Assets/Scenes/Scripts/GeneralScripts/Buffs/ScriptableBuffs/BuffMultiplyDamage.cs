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

            entityObject.GetComponent<PlayerStats>().listOfBuffs.Remove(this);
            Debug.Log("SPirit Buff Ended");

        }

        protected override void ApplyEffect()
        {

            entityObject.GetComponent<PlayerStats>().listOfBuffs.Add(this);
        }

        // Start is called before the first frame update

    }
}
