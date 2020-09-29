using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

//this script serves as an extention for the follow cam of cinemachine
// to address some follow problems
namespace SideScrollerProject
{
    public class CameraFollowSettings : MonoBehaviour
    {
        [SerializeField] float screenYDefault = 0.2f; //0.42f when not playing, probably changeable fro different scenes
        [SerializeField] float screenYFalling = 0.2f; //0.42f when not playing, probably changeable fro different scenes
        [SerializeField] float screenXBias;        // not bothering at the moment
        [SerializeField] CinemachineVirtualCamera vCam; // current vCam
        [SerializeField] GameObject defaultTarget;// if grounded or jumping
        [SerializeField] GameObject fallTarget; // when player velocity is <0 change target to falltarget
        CinemachineFramingTransposer vCamFramingTransposer;
        [SerializeField] float yPlayerVelocity;


        // Start is called before the first frame update
        void Start()
        {
            yPlayerVelocity = PlayerManager.instance.PlayerRigidbody2D.velocity.y;
            //    vCam = CameraManager.instance.vCamCurrent;
            vCamFramingTransposer = vCam.GetCinemachineComponent<CinemachineFramingTransposer>();
            vCam.m_Follow = defaultTarget.transform;
        }

        // Update is called once per frame
        void LateUpdate()
        {

        }
        void ChangeScreenOffset()
        {
            if (vCam.m_Follow == fallTarget.transform)
                vCamFramingTransposer.m_ScreenY = screenYDefault;
            else if (vCam.m_Follow == fallTarget)
            {
                vCamFramingTransposer.m_ScreenY = screenYFalling;

            }

        }
    }
}
