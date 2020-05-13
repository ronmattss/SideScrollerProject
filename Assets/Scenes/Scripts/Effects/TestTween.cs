using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTween : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.scaleY(this.gameObject, 3f, 0.03f).setEaseInOutSine();
        LeanTween.scaleX(this.gameObject, 3f, 0.03f).setEaseInOutSine();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
