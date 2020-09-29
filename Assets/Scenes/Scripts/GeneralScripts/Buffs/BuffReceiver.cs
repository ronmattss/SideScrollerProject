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
                    buff.isFinished = false;

                    _buffs.Remove(buff.buff);
                    Debug.Log("Removed: " + buff.buff.buffName + " " + buff.isFinished + " " + _buffs.Count);
                }

            }


        }
        public void RemoveBuff(Buff buff)
        {
            if (!_buffs.ContainsKey(buff)) return;
            Debug.Log("Removed: " + buff.buffName);
            _buffs[buff].End();
            _buffs.Remove(buff);
            Debug.Log("Active Buff Count: " + _buffs.Count);
        }

        public void AddBuff(TimedBuff buff)
        {
            Debug.Log("Added Buff is finished?:"+buff.isFinished);
            if (_buffs.ContainsKey(buff.buff))
            {
                _buffs[buff.buff].Activate();
            }
            else
            {
                _buffs.Add(buff.buff, buff);
                buff.Activate();
            }
            Debug.Log("Active Buff Count: " + _buffs.Count);
        }
    }

}