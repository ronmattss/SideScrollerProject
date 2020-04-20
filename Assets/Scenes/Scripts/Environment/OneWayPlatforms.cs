using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{
    public class OneWayPlatforms : MonoBehaviour
    {
        private PlatformEffector2D effector2D;
        float returnTime = 0.2f;
        bool isReturning = false;


        void Start()
        {
            effector2D = GetComponent<PlatformEffector2D>();
        }

        // Update is called once per frame
        //if player is on top
        // if player press downkey and Space
        // rotate to 180
        // then rotate -180 
        void Update()
        {
            if (isReturning)
            {
                returnToOriginal();
            }
        }
        void returnToOriginal()
        {
            returnTime -= Time.deltaTime;
            if (returnTime <= 0)
            {
                effector2D.rotationalOffset = 0;
                returnTime = 0.2f;
                isReturning = false;
            }
        }



        void OnCollisionExit2D(Collision2D other)
        {
            isReturning = true;
        }
    }
}
