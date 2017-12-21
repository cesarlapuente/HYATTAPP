using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Range(0.0f, 10.0f)]
    public float _maxZoomValue;

    private Vector3 _originalPosition;
    private Vector3 _targetPosition;

    private bool _isZoomedIn = false;

    private Coroutine _MoveObjectCoroutine;

    private enum PinchState { Empty, PinchStarted, Pinching }

    private PinchState _currentPinchState;

    private PinchState CurrentPinchState
    {
        get
        {
            return _currentPinchState;
        }

        set
        {
            _currentPinchState = value;
            Debug.Log(value);
            switch (_currentPinchState)
            {
                case PinchState.Empty:
                    break;
                case PinchState.PinchStarted:
                    // https://stackoverflow.com/questions/9605556/how-to-project-a-point-onto-a-plane-in-3d#comment12186019_9605695
                    RaycastHit raycastHit;
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit, Mathf.Infinity))
                    {
                        _targetPosition = raycastHit.point;
                        //Vector3 cameraHitVector = raycastHit.point - transform.position;
                        //float distanceBetweenHitAndCameraPlane = Vector3.Dot(cameraHitVector, transform.forward);
                        //_targetPosition = raycastHit.point - distanceBetweenHitAndCameraPlane * transform.forward;
                    }
                    CurrentPinchState = PinchState.Pinching;
                    break;
                case PinchState.Pinching:
                    break;
                default:
                    break;
            }
        }
    }

    void Start()
    {
        _originalPosition = transform.position;
        InputManager.DoubleClickTriggered += ToggleCameraZoom;
    }

    private void Update()
    {
        //if (CurrentPinchState == PinchState.Pinching)
        //{
        //    if (Input.touchCount == 2)
        //    {
        //        // Store both touches.
        //        Touch touchZero = Input.GetTouch(0);
        //        Touch touchOne = Input.GetTouch(1);

        //        // Find the position in the previous frame of each touch.
        //        Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
        //        Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

        //        // Find the magnitude of the vector (the distance) between the touches in each frame.
        //        float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
        //        float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

        //        // Find the difference in the distances between each frame.
        //        float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

        //        Vector3 currentPosition = transform.position;

        //        float zoomValue = - deltaMagnitudeDiff / 100.0f;
        //        Vector3 newPosition = currentPosition + (_targetPosition - currentPosition) * zoomValue;
        //        if (Vector3.Dot(_targetPosition - _originalPosition, newPosition - _originalPosition) >= 0)
        //        {
        //            transform.position = newPosition;
        //        }
        //    }
        //    else
        //    {
        //        CurrentPinchState = PinchState.Empty;
        //    }
        //}
        //else if (Input.touchCount == 2)
        //{
        //    CurrentPinchState = PinchState.PinchStarted;
        //}
    }

    private void ToggleCameraZoom()
    {
        if (!_isZoomedIn)
        {
            // Center the camera where the user clicked
            // https://stackoverflow.com/questions/9605556/how-to-project-a-point-onto-a-plane-in-3d#comment12186019_9605695
            RaycastHit raycastHit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit, Mathf.Infinity))
            {
                Vector3 cameraHitVector = raycastHit.point - transform.position;
                float distanceBetweenHitAndCameraPlane = Vector3.Dot(cameraHitVector, transform.forward);
                Vector3 newCameraPos = raycastHit.point - distanceBetweenHitAndCameraPlane * transform.forward;
                _targetPosition = transform.forward * _maxZoomValue + newCameraPos;
            }
            ZoomIn();
        }
        else
        {
            Reset();
        }
        _isZoomedIn = !_isZoomedIn;
    }
    
    private void ZoomIn()
    {
        if (_MoveObjectCoroutine != null)
        {
            StopCoroutine(_MoveObjectCoroutine);
            _MoveObjectCoroutine = StartCoroutine(MoveObjectCoroutine(_targetPosition, 1.0f));
        }
    }

    private IEnumerator MoveObjectCoroutine(Vector3 destination, float time)
    {
        float i = 0.0f;
        float rate = 1.0f / time;
        while (i < 1.0)
        {
            i += Time.deltaTime * rate;
            transform.position = Vector3.Lerp(transform.position, destination, i);
            yield return new WaitForFixedUpdate();
        }
    }

    private void Reset()
    {
        if (_MoveObjectCoroutine != null)
        {
            StopCoroutine(_MoveObjectCoroutine);
            StartCoroutine(MoveObjectCoroutine(_originalPosition, 1.0f));
        }
    }
}
