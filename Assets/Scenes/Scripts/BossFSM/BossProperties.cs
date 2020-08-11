using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BossProperties
{
    public Transform meleeAttackPosition;
    public GameObject flameHitPosition;
    public GameObject flamePosition;
    public float diameter = 3f;
    public LayerMask playerLayer;
}
