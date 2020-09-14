using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

// interface that describes an entity that DoDamage
// will sit on an attack class
public interface IDoDamage
{
    void DoDamage(int initialDamage,GameObject target);
}
