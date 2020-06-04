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
        public  GameObject obj;
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
            duration -= delta;
            if (duration <= 0)
            {
                End();
                isFinished = true;
            }
        }

        public void Activate()
        {   Debug.Log("WTF IS THE GAMEOBJECT: "+obj.name);
            if (buff.isEffectStacked || duration <= 0)
            {
                ApplyEffect();
                effectStacks++;
            }
            if (buff.isBuffStackable || duration <= 0)
            {
                duration += buff.buffDuration;
            }
        }
        protected abstract void ApplyEffect();
        public abstract void End();
    }
}
