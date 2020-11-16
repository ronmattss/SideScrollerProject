using UnityEngine;

namespace Scenes.Scripts.NPCFSM
{
    
    public class NpcProperties : ScriptableObject,IDamageable
    {
        private BaseStatus baseStatus;
        // this will suppose to replace Status Class which contains NPC properties

        public void TakeDamage(int baseDamage)
        {
            // calculate damage here
        }
    }
}
