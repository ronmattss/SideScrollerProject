using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


namespace SideScrollerProject
{
    public class RoomManager : MonoBehaviour
    {

        // Room Manager Handles all rooms that is present to the game
        // a Single Instance 
        // Start is called before the first frame update
        // handles the calling of rooms
        public event Action<int> roomTriggerCall;
        public static RoomManager instance;
        public RoomInformation[] rooms;
        [SerializeField] RoomInformation currentRoom;
        CinemachineConfiner confiner;



        public void Start()
        {
            if (instance == null)
                instance = this;
            else
            {
                Destroy(this.gameObject);
                return;
            }
            currentRoom = rooms[0];
            currentRoom.roomObject = rooms[0].roomObject;
        }
        // Load Room
        // Call this from a Trigger
        public void CallRoom(PossibleSpawnLocations locations, int nextRoomIndex)
        {
            Transform x = this.transform;

            currentRoom.adjacentRooms[nextRoomIndex].roomObject.SetActive(true);
            var y = currentRoom.adjacentRooms[nextRoomIndex].spawnPairCode;
            Debug.Log($"Arguments: {locations} {nextRoomIndex} AdjacentRoom: Does Have code: {currentRoom.adjacentRooms[nextRoomIndex].spawnPairCode.ContainsKey(locations)} ");
            foreach (var z in y)
            {
                Debug.Log($"Keys: {z.Key} Values: {z.Value}");
            }
            if (currentRoom.adjacentRooms[nextRoomIndex].spawnPairCode.TryGetValue(locations, out x))
            {
                Debug.Log(x.position);
                PlayerManager.instance.GetPlayerTransform().position = x.position;
                CameraManager.instance.confiner.m_BoundingShape2D = currentRoom.adjacentRooms[nextRoomIndex].cameraBounds;
                CameraManager.instance.confiner.InvalidatePathCache();
            }
            else
            {
                Debug.Log(x.position);

            }
            currentRoom.roomObject.SetActive(false);
            currentRoom = currentRoom.adjacentRooms[nextRoomIndex];


        }

    }
}