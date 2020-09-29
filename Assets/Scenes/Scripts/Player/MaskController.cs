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
    public TrailRenderer trailEffect;
    public bool thirdEyeOn = false;
    public float scale = 30f;
    public float scaleX;
    public float scaleY;
    [SerializeField] float sWidth = 6;
    [SerializeField] float sHeight = 6;
    void Start()
    {
        //sWidth = Camera.main.scaledPixelWidth;
        //sHeight = Camera.main.scaledPixelHeight;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void LateUpdate()
    {
       // sWidth = getScreenWidth();
       // sHeight = getScreenHeight();


    }
    public float getScreenHeight()
    {
        return Camera.main.orthographicSize * 2.0f;

    }
    public float getScreenWidth()
    {
        return getScreenHeight() * Screen.width / Screen.height;
    }
  

    public void ResizeObject()
    {
        if (thirdEyeOn) // true shrink down
        {
            maskObject.transform.LeanScale(Vector3.zero, 0.25f);
            thirdEyeOn = false;
            rippleReverse.Play();
            trailEffect.gameObject.SetActive(false);
            // rippleReverse.Stop();

        }
        else
        {
            maskObject.transform.LeanScale(new Vector3(sWidth, sHeight, 1), 0.25f);
            thirdEyeOn = true;
            ripple.Play();
            trailEffect.gameObject.SetActive(true);
            // ripple.Stop();
        }

    }
}
