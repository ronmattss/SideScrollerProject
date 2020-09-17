﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{
    // Buff Logic
    [System.Serializable]
    public class BuffInstaKill : TimedBuff
    {
        private GameObject toKillObj;
        public BuffInstaKill(Buff buff, GameObject _obj) : base(buff, _obj)
        {
            toKillObj = _obj;
            Debug.Log("Buff Activated on: " + toKillObj.name);
            obj = toKillObj;
        }
        public BuffInstaKill() : base()
        {
        }
        public override void End()
        {

        }

        protected override void ApplyEffect()
        {
            toKillObj.GetComponent<Status>().TakeDamage(10);
        }

        protected override void EffectPerSecond(float delta)
        {
            Debug.Log("do something");
           // throw new System.NotImplementedException();
        }
    }

}