using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : TriggerArea
{

    public int leftCamera;
    public int rightCamera;
    public int upCamera;
    public int downCamera;


    public void TriggerCameraSwitch()
    {

        CameraManager.instance.SwapCameraHorizontally(leftCamera, rightCamera);


    }
    public void TriggerCameraSwitchVertical()
    {
        CameraManager.instance.SwapCameraVertically(upCamera,downCamera);
    }
}
