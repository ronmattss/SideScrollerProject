using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{
    public class BuffIgnite : TimedBuff
    { // ignite logic
        float currentTickRate;
        float tick;
        int damage;
        GameObject target;
        Status entityStatus;
        public BuffIgnite(Buff buff, GameObject obj) : base(buff, obj)
        {
            target = obj;
            currentTickRate = (buff as IgniteObject).tickRate;
            damage = (buff as IgniteObject).damagePerSecond;
            obj = target;
            entityStatus = target.GetComponent<Status>();

        }

        public override void End()
        {
            Debug.Log("Ignite ended");
        }

        protected override void ApplyEffect()
        {
            tick = currentTickRate;
            Debug.Log($"Duration: {duration} Tick rate:{tick} current Tickrate:{currentTickRate}");
        }

        protected override void EffectPerSecond(float delta)
        {
            if (tick <= 0)
            {
                tick = currentTickRate;
            }
            else
            {
                tick -= delta;
                IgniteEntity();
            }
        }

        private void IgniteEntity()
        {
            //refactor to entityStats
            if (entityStatus.currentHealth <= 0)
                return;
            entityStatus.currentHealth -= damage;

        }

        // per tick (0.1)
        // damage receiver
        // Start is called before the first frame update

    }
}
