using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{
    public abstract class ProjectileAbility : Ability
    {
        public float duration; // duration before despawn
        public int damage;
        public float speed;   // projectile Speed
        public int maxTarget; // max targets before despawn
        public GameObject projectile; // projectile prefab
    }
}
