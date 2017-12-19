using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Range(0.0f, 10.0f)]
    public float _zoomValue;

    private Vector3 _originalPosition;
    private bool _isZoomedIn = false;

    void Start()
    {
        _originalPosition = transform.position;
        InputManager.DoubleClickTriggered += ToggleCameraZoom;
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
                transform.position = newCameraPos;
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
        transform.position = transform.position + transform.forward * _zoomValue;
    }

    private void Reset()
    {
        transform.position = _originalPosition;
    }
}
