using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BuffGiver
// MonoBehaviour class which sits on skills/ attacks/ etc...
// gives buffs to player or enemies
// 
namespace SideScrollerProject
{
    public class BuffGiver : MonoBehaviour
    {
        public List<Buff> buffsToInit = new List<Buff>();
        [SerializeField] public List<TimedBuff> buffsToGive = new List<TimedBuff>();
        public GameObject receiver;
        public TimedBuff timedBuff;
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void InitialiazeBuffs()
        {
            foreach (var b in buffsToInit)
            {
                Debug.Log("Is this buff initializing?");
                buffsToGive.Add(b.InitializeBuff(receiver));
            }
        }

        public void GiveBuff(Buff _buff)
        {
            timedBuff = _buff.InitializeBuff(receiver);
            Debug.Log("Buff Receiever: " + timedBuff.obj.name + " " + receiver.name);
            timedBuff.obj = receiver;
            buffsToGive.Add(timedBuff);
            if (buffsToGive.Count > 0)
                foreach (var buff in buffsToGive)
                {
                    receiver.GetComponent<BuffReceiver>().AddBuff(buff);
                }
        }
        public void SelfBuff(Buff _buff)
        {
            timedBuff = _buff.InitializeBuff(receiver);
            timedBuff.obj = receiver;
            receiver.GetComponent<BuffReceiver>().AddBuff(timedBuff);

        }

        // FOR TESTING ONLY
        public void GiveBuff()
        {
            timedBuff = buffsToInit[0].InitializeBuff(receiver);
            Debug.Log("Buff Receiever: " + timedBuff.obj.name + " " + receiver.name);
            timedBuff.obj = receiver;
            buffsToGive.Add(timedBuff);
            if (buffsToGive.Count > 0)
                foreach (var buff in buffsToGive)
                {
                    receiver.GetComponent<BuffReceiver>().AddBuff(buff);
                }
        }
        public void GiveBuffToEnemy()
        {
            timedBuff = buffsToInit[0].InitializeBuff(receiver);
            Debug.Log("Buff Receiever: " + timedBuff.obj.name + " " + receiver.name);
            timedBuff.obj = receiver;
            buffsToGive.Add(timedBuff);
            if (buffsToGive.Count > 0)
                foreach (var buff in buffsToGive)
                {
                    receiver.GetComponent<BuffReceiver>().AddBuff(buff);
                }
        }

    }

}