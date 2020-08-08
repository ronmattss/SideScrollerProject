using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace SideScrollerProject
{
    public enum PossibleSpawnLocations
    {
        TopLeft, TopRight, Top, LeftTop, LeftBottom, BottomLeft, BottomRight, Bottom, RightTop, RightBottom
    }
    public class RoomInformation : MonoBehaviour
    {
        // Room Information Script V0.0.1
        // Contains information about the room the player currently on

        public string roomName;       // to easily Classify what room you are currently in
        public GameObject roomObject; // Im thinking about making the rooms a Prefab or disabled objects then load/enable/disable it
        public bool changeCamera = false;
        public CinemachineVirtualCamera roomCamera;
        public RoomInformation[] adjacentRooms; // Clockwise placement of rooms 0 top 1 right 2 bottom..
        [SerializeField] private Transform[] spawnPositions; // where player will be located hmmmm 
        [SerializeField] private PossibleSpawnLocations[] spawnCode;  // each transform should indicate what spawnCode they are
        public Collider2D cameraBounds;

        public Dictionary<PossibleSpawnLocations, Transform> spawnPairCode = new Dictionary<PossibleSpawnLocations, Transform>();
        //logic to spawn correctly 

        public void Awake()
        {
            for (int i = 0; i < spawnCode.Length; i++)
            {
                spawnPairCode.Add(spawnCode[i], spawnPositions[i]);
                Debug.Log($"{spawnCode[i].ToString()} {spawnPositions[i].name}");
            }
        }

    }
}