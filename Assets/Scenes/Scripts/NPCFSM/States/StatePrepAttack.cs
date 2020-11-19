using UnityEngine;

namespace Scenes.Scripts.NPCFSM.States
{
    public class StatePrepAttack : StateMachine
    {
        // Start is called before the first frame update

        public StatePrepAttack(GameObject npc,Animator animator, Rigidbody2D npcRn) : base(npc,animator, npcRn)
        {
        }
    }
}
