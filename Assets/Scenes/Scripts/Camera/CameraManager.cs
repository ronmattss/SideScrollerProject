using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Linq;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    public CinemachineVirtualCamera vCamCurrent;
    private CinemachineBrain brain;
    public List<CinemachineVirtualCamera> listOfVCam;
    public int lastCamera;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        brain = Camera.main.GetComponent<CinemachineBrain>();
        vCamCurrent = listOfVCam[0];
        lastCamera = 0;
        GameEvent.instance.onCameraSwitch += CameraSwap;
        //  Debug.Log("Current Camera Priority" + vCam.m_Priority);


    }

    // Update is called once per frame
    void LateUpdate()
    {
        // force transition of Camera
        if (!player.GetComponent<SpriteRenderer>().isVisible)
        {
            CameraSwap(lastCamera);
        }
    }

    public void CameraSwap(int nextCamera)
    {
        int tempCamIndex = IndexOfCamera();     // Store currentVcam index
        if (nextCamera == tempCamIndex)         // if nextCamIndex == currentCam
        {
        //    vCamCurrent.gameObject.SetActive(false);
            vCamCurrent = listOfVCam[lastCamera]; // Change current Camera to previousCamera
            lastCamera = tempCamIndex;
            //if(vCamCurrent.)
        }
        else // if nextCamera is not the current Camera
        {
            lastCamera = tempCamIndex;
          //  vCamCurrent.gameObject.SetActive(false);
            vCamCurrent = listOfVCam[nextCamera];

        }

        vCamCurrent.gameObject.SetActive(true);
        vCamCurrent.MoveToTopOfPrioritySubqueue();
      //  BackgroundMovement.instance.cameraTransform = vCamCurrent.gameObject.transform;
    }



    public void ChangeActiveCamera(int id)
    {
        foreach (CinemachineVirtualCamera cam in listOfVCam)
        {
            if (cam != vCamCurrent)
                cam.gameObject.SetActive(false);
        }
    }
    int IndexOfCamera()
    {
        int vCamIndex = listOfVCam.IndexOf(vCamCurrent);
        return vCamIndex;
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    private void OnDestroy()
    {
        GameEvent.instance.onCameraSwitch -= CameraSwap;
    }
}
