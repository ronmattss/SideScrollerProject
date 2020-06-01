using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SideScrollerProject
{
    public class Jump : MonoBehaviour
    {

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Movement.instance.Jump();
        }
    }
}
