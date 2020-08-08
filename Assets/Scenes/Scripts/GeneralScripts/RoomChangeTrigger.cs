using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SideScrollerProject
{
    public class RoomChangeTrigger : MonoBehaviour
    {

        public int nextRoom;
        public PossibleSpawnLocations possibleLocation;
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetType() == typeof(CapsuleCollider2D) && other.CompareTag("Player"))
                RoomManager.instance.CallRoom(possibleLocation, nextRoom);
        }
    }
}
