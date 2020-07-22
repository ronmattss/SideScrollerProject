using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Not entirely a Mask Controller
// but just a resizer of it
// Third eye ability
// see certain enemies and obstacles, uncover things
public class MaskController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject maskObject;
    public ParticleSystem ripple;
    public ParticleSystem rippleReverse;
    public bool thirdEyeOn = false;
    public float scale = 30f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResizeObject()
    {
        if (thirdEyeOn) // true shrink down
        {
            maskObject.transform.LeanScale(Vector3.zero, 0.25f);
            thirdEyeOn = false;
            rippleReverse.Play();
           // rippleReverse.Stop();
           
        }
        else
        {
            maskObject.transform.LeanScale(new Vector3(scale, scale, 1), 0.25f);
            thirdEyeOn = true;
             ripple.Play();
            // ripple.Stop();
        }

    }
}
