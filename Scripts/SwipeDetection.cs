using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    public event OnSwipeInput SwipeEvent;
    public delegate void OnSwipeInput(Vector2 direction);

    private Vector3 tapPos;
    private Vector3 swipeDelta;

    private float deadZone;

    private Vector2 swipePos;
    private bool isSwiping;
    void Start()
    {
        deadZone = 0.2f;
        tapPos = Vector2.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            if (tapPos == Vector3.zero)
            {
                isSwiping = true;
                tapPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Canceled || Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                ResetSwipe();

            }

        }

        CheckSwipe();

    }

    public void CheckSwipe()
    {
        swipeDelta = Vector3.zero;
        if (isSwiping)
        {
            if(Input.touchCount > 0)
            {
                swipeDelta = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) - tapPos;
           
            }
           
        } 
        if(swipeDelta.magnitude > deadZone)
        {
            if (SwipeEvent != null)
            {
                if (Mathf.Abs(swipeDelta.x) < Mathf.Abs(swipeDelta.y))
                {
                    if (swipeDelta.y > 0)
                    {
                        SwipeEvent( Vector2.up);

                    }
                    else if (swipeDelta.y < 0)
                    {
                        SwipeEvent( Vector2.down);
                    }
                }
            }

            tapPos = Vector3.zero;
        }
     
    }

    private void ResetSwipe()
    {
        isSwiping = false;
        tapPos = Vector3.zero;
        swipeDelta = Vector3.zero;
    }
}
