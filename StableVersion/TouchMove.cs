using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TouchMove : MonoBehaviour
{
    private Touch touch;
    public float speedModifier;
    private Transform player;
    private float borderLimit = 4;
    void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            // print("Touch");
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                // print("touchMove");
                player.position = new Vector3(transform.position.x + touch.deltaPosition.normalized.x * speedModifier,
                player.position.y,
                player.position.z);
                if (player.position.x > borderLimit)
                {
                    player.position = new Vector3(borderLimit, player.position.y, player.position.z);
                }
                else if (player.position.x < -borderLimit)
                {
                    player.position = new Vector3(-borderLimit, player.position.y, player.position.z);
                }
            }
        }
    }
}
