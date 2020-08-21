using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class BossTriggers : MonoBehaviour
{
    public UnityEvent onEnableEvent;
    public UnityEvent onDisableEvent;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        if (onEnableEvent != null)
            onEnableEvent.Invoke();
    }
    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        if (onDisableEvent != null)
            onDisableEvent.Invoke();
    }
}
