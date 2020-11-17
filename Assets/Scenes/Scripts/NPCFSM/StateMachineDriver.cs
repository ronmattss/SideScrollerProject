using Scenes.Scripts.NPCFSM.States;
using SideScrollerProject;
using UnityEngine;

namespace Scenes.Scripts.NPCFSM
{
    //sits as a component in an object
    // this is where states are used
    public class StateMachineDriver : MonoBehaviour
    {
        private StateMachine baseState;
        [SerializeField] private Status npcProperties;

        //  public StateParameters parameters;
        // Start is called before the first frame update
        void Start()
        {
        }

        private void Awake()
        {
            npcProperties = GetComponent<Status>();
            baseState = new StateIdle(this.gameObject, GetComponent<Animator>(), GetComponent<Rigidbody2D>());
        }

        // Update is called once per frame
        void Update()
        {
            baseState = baseState.Process();
        }
    }
}