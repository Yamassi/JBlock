using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class InputManager : MonoBehaviour
{
    public float sensitivity;
    private Transform player;
    private float borderLimit = 4;
    float dead = 0.001f;
    private void Awake()
    {
        EnhancedTouchSupport.Enable();
    }
    private void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject.transform;
    }
    void Update()
    {
        if (Touch.activeFingers.Count == 1)
        {
            Touch activeTouch = Touch.activeFingers[0].currentTouch;
            float horizontalMove = 0;
            if (activeTouch.phase == TouchPhase.Moved)
            {
                var touchPosition = activeTouch.screenPosition;
                float target;
                if (touchPosition.x > Screen.width / 2)
                {
                    target = 1;
                }
                else
                {
                    target = -1;
                }

                horizontalMove = Mathf.MoveTowards(horizontalMove, target, sensitivity * Time.deltaTime);
                // print(horizontalMove);
            }
            else
            {
                horizontalMove = (horizontalMove < dead) ? 0 : Mathf.MoveTowards(horizontalMove, 0, sensitivity * Time.deltaTime);
            }
            player.Translate(Vector3.right * (horizontalMove) * sensitivity * Time.deltaTime);
            if (player.position.x > borderLimit)
            {
                player.position = new Vector3(borderLimit, player.position.y, player.position.z);
            }
            else if (player.position.x < -borderLimit)
            {
                player.position = new Vector3(-borderLimit, player.position.y, player.position.z);
            }

            // print(activeTouch.screenPosition.x);
            Debug.Log($"Phase: {activeTouch.phase} | Position: {activeTouch.startScreenPosition}");
        }

    }
}

