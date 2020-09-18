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
        EntityStatus entityStatus;
        public BuffIgnite(Buff buff, GameObject obj) : base(buff, obj)
        {
            target = obj;
            currentTickRate = (buff as IgniteObject).tickRate;
            damage = (buff as IgniteObject).damagePerSecond;
            obj = target;
            entityStatus = target.GetComponent<EntityStatus>();

        }

        public override void End()
        {
            Debug.Log("Ignite ended");
        }

        protected override void ApplyEffect()
        {
            tick = currentTickRate;
        }

        protected override void EffectPerSecond(float delta)
        {
            if (tick <= 0)
            {
                tick = currentTickRate;
                IgniteEntity();
            }
            else
            {
                tick -= delta;
                //Debug.Log($"current Tick rate:{tick}");
            }
            //    Debug.Log($"Duration: {duration} Tick rate:{tick} current Tickrate:{currentTickRate}");
        }

        private void IgniteEntity()
        {
            //refactor to entityStats
            if (entityStatus.gameObject.tag == "Player")
            {
                Debug.Log("is the player being burned?");
                entityStatus.gameObject.GetComponent<PlayerStats>().CurrentHealth -= damage;
            }
            else
            {
                entityStatus.gameObject.GetComponent<Status>().currentHealth -= damage;
            }
            // }
            // if (entityStatus.currentHealth <= 0)
            //     return;
            // entityStatus.currentHealth -= damage;

        }

        // per tick (0.1)
        // damage receiver
        // Start is called before the first frame update

    }
}
