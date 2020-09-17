using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace SideScrollerProject
{
    // Receiver receives buffs from BuffGiver
    // this is where buffs will live until its duration

    public class BuffReceiver : MonoBehaviour
    {
        [SerializeField] private readonly Dictionary<Buff, TimedBuff> _buffs = new Dictionary<Buff, TimedBuff>();

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            foreach (var buff in _buffs.Values.ToList())
            {
                buff.Tick(Time.deltaTime);
                if (buff.isFinished)
                {
                    Debug.Log("Removed: " + buff.buff.buffName);
                    buff.isFinished = false;
                    _buffs.Remove(buff.buff);
                }

            }

        }

        public void AddBuff(TimedBuff buff)
        {
            if (_buffs.ContainsKey(buff.buff))
            {
                _buffs[buff.buff].Activate();
            }
            else
            {
                _buffs.Add(buff.buff, buff);
                buff.Activate();
            }
        }
    }

}