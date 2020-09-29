using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{
    [System.Serializable]
    public abstract class TimedBuff
    {
        protected float duration;
        protected int effectStacks;
        public Buff buff { get; }
        public GameObject obj;
        public bool isFinished;


        public TimedBuff(Buff buff, GameObject obj)
        {
            this.buff = buff;
            this.obj = obj;
        }
        public TimedBuff()
        {

        }

        public void Tick(float delta)
        {
            if (buff.isTimed)
                duration -= delta;
            Debug.Log("Duration of buff: " + duration);
            EffectPerSecond(delta);
            if (isFinished || (duration <= 0 && buff.isTimed))
            {
                isFinished = true;
                End();
            }
        }

        public void Activate()
        {
            Debug.Log("Activated on GAMEOBJECT: " + obj.name);
            Debug.Log("Will this activate? " + buff.buffName+ " Duration: "+buff.buffDuration);
            duration = buff.buffDuration;
            // if just activate or deactivate
            if (!buff.isBuffStackable && !buff.isBuffStackable)
            {
                ApplyEffect();
                return;
            }
            else
            {
                if (buff.isEffectStacked || duration <= 0)
                {
                    ApplyEffect();
                    effectStacks++;
                }
                if (buff.isBuffStackable || duration <= 0)
                {
                    duration += buff.buffDuration;
                    Debug.Log("current Duration: " + duration);
                }
            }
        }
        protected abstract void ApplyEffect();
        protected abstract void EffectPerSecond(float delta);
        public abstract void End();
    }
}
