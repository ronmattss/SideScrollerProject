using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 otherPostion;
    private Vector2 origPosition;
    private Vector2 nextPos;
    private Vector2 pA;
    private Vector2 pB;
    public bool isOnOrigin = true;
    public float waitTime = 1;
    public float timeMoving = 0.5f;
    public float repeatTime = 2f;
    public float invokeTime = 1f;
    public bool teleportToSafePlace = false;
    public bool moveVertical = true;
    public float position = 3;
    public bool spikeMove = true;

    List<Vector3> locations = new List<Vector3>();
    Vector3 currentPosition;
    void Start()
    {
        origPosition = this.transform.position;
        //hidePosition = new Vector2(this.transform.position.x, this.transform.position.y - 3);
        pA = origPosition;
        pB = otherPostion;
        nextPos = pA;

        if (spikeMove)
            InvokeRepeating("StartCoroutineMethod", invokeTime, repeatTime);
    }

    // Update is called once per frame

    //       Vector3 currentPosition = this.transform.position;
    void ChangeDirection()
    {
        nextPos = nextPos != pA ? pA : pB;
    }
    void StartCoroutineMethod()
    {
        if (moveVertical)
            StartCoroutine(PlatformMoveVertical());
        else
            StartCoroutine(PlatformMoveHorizontal());
    }
    IEnumerator PlatformMoveVertical()
    {

        if (isOnOrigin)
        {
            LeanTween.moveY(this.gameObject, this.transform.position.y - position, timeMoving).setEaseInOutSine();
            StartCoroutine(Wait());
            isOnOrigin = false;
        }
        else
        {
            LeanTween.moveY(this.gameObject, origPosition.y, timeMoving).setEaseInOutSine();
            StartCoroutine(Wait());
            isOnOrigin = true;
        }
        if (isOnOrigin)
            yield return new WaitForSecondsRealtime(waitTime);
        //   StartCoroutine(Spikey());


    }

    IEnumerator PlatformMoveHorizontal()
    {
        if (isOnOrigin)
        {
            LeanTween.moveX(this.gameObject, this.transform.position.x - position, timeMoving).setEaseInOutQuad();
            StartCoroutine(Wait());
            isOnOrigin = false;
        }
        else
        {
            LeanTween.moveX(this.gameObject, origPosition.x, timeMoving).setEaseInOutQuad();
            StartCoroutine(Wait());
            isOnOrigin = true;
        }
        yield return new WaitForSecondsRealtime(waitTime);

    }
    IEnumerator Wait()
    {

        yield return new WaitForSeconds(1);
        yield return new WaitForSeconds(1);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Player")
        {
            other.transform.parent = this.gameObject.transform;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.tag == "Player")
        {
            other.transform.parent = null;
        }
    }



    // IENumerator

}
