using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayer : MonoBehaviour
{
    Vector2 velocity;

    public float smoothTimeY;
    public float smoothTimeX;
    public float xOffset = 0;
    public float yOffset = 0;
    public GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x - xOffset, ref velocity.x, smoothTimeX);
        float posy = Mathf.SmoothDamp(transform.position.y, player.transform.position.y - yOffset, ref velocity.y, smoothTimeY);

        transform.position = new Vector3(posX, posy, transform.position.z);
    }
}
