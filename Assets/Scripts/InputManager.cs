using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public float _timeToWaitForDoubleClick;
    private bool _firstClick = false;
    private float _timerAfterFirstClick;

    public delegate void TriggerClick();
    public static TriggerClick SingleClickTriggered;
    public static TriggerClick DoubleClickTriggered;

    // Update is called once per frame
    void Update ()
    {
        if (!_firstClick)
        {
            if (Input.GetMouseButtonUp(0))
            {
                _firstClick = true;
            }
        }
        else
        {
            _timerAfterFirstClick += Time.deltaTime;
            if (_timerAfterFirstClick >= _timeToWaitForDoubleClick)
            {
                if (SingleClickTriggered != null)
                {
                    SingleClickTriggered();
                }
                _firstClick = false;
                _timerAfterFirstClick = 0.0f;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (DoubleClickTriggered != null)
                {
                    DoubleClickTriggered();
                }
                _firstClick = false;
                _timerAfterFirstClick = 0.0f;
            }
        }
	}
}
