using UnityEngine;

namespace Scenes.Scripts.NPCFSM
{
    
    //sits as a component in an object
    // this is where states are used
    public class StateMachineDriver : MonoBehaviour
    {
        private StateMachine baseState;
        private NpcProperties npcProperties;

      //  public StateParameters parameters;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        private void OnEnable()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            baseState.Process();
        }
    }
    
}
