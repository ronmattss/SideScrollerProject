﻿using System.Collections;
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
    Transform playerTransform;
    float vDirection = 0;
    float direction = 0;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        playerTransform = player.transform;
        direction = playerTransform.localScale.x;
        brain = Camera.main.GetComponent<CinemachineBrain>();
        vCamCurrent = listOfVCam[0];
        this.lastCamera = 0;
        // GameEvent.instance.onCameraSwitch += CameraSwap;
        //  Debug.Log("Current Camera Priority" + vCam.m_Priority);


    }

    // Update is called once per frame
    void LateUpdate()
    {
        direction = playerTransform.localScale.x;
        vDirection = player.GetComponent<Animator>().GetInteger("Falling");
    }
    public void SwapCameraHorizontally(int leftCamera, int rightCamera)
    {
        //if camera is on left 
        if (direction < 0) //going left
        {
            vCamCurrent = listOfVCam[leftCamera];
      //      listOfVCam[rightCamera].gameObject.SetActive(false);
        }
        else if (direction > 0)
        {
            vCamCurrent = listOfVCam[rightCamera];
    //        listOfVCam[leftCamera].gameObject.SetActive(false);
        }
        vCamCurrent.gameObject.SetActive(true);
        vCamCurrent.MoveToTopOfPrioritySubqueue();
    }

    public void SwapCameraVertically(int upCamera, int downCamera)
    {
        //if camera is on left 
        if (vDirection <= 0) //going left
        {
            vCamCurrent = listOfVCam[downCamera];
            Debug.Log("Down");
  //          listOfVCam[upCamera].gameObject.SetActive(false);
        }
        else if (vDirection > 0)
        {
            vCamCurrent = listOfVCam[upCamera];
//            listOfVCam[downCamera].gameObject.SetActive(false);
            Debug.Log("Up");
        }
        vCamCurrent.gameObject.SetActive(true);
        vCamCurrent.MoveToTopOfPrioritySubqueue();
    }

    // Trigger Areas will need store a lastCam index
    public void CameraSwap(int nextCamera, int lastCamera, int fromWhatCamera)
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
        this.lastCamera = lastCamera;
        vCamCurrent.gameObject.SetActive(true);
        vCamCurrent.MoveToTopOfPrioritySubqueue();
        //  BackgroundMovement.instance.cameraTransform = vCamCurrent.gameObject.transform;
    }


    public int TrySwap(int nextCam)
    {
        int tempCam = IndexOfCamera(); // last Camera
        this.lastCamera = tempCam;
        vCamCurrent = listOfVCam[nextCam];
        vCamCurrent.MoveToTopOfPrioritySubqueue();
        return tempCam;
    }

    public bool CheckIfVisible()
    {
        Debug.Log(player.GetComponent<SpriteRenderer>().isVisible);
        return player.GetComponent<SpriteRenderer>().isVisible;
    }





    public void ChangeActiveCamera(int id)
    {
        foreach (CinemachineVirtualCamera cam in listOfVCam)
        {
            if (cam != vCamCurrent)
                cam.gameObject.SetActive(false);
        }
    }
    public int IndexOfCamera()
    {
        int vCamIndex = listOfVCam.IndexOf(vCamCurrent);
        return vCamIndex;
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    private void OnDestroy()
    {

    }
}