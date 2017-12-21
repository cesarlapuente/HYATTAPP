using UnityEngine;
using UnityEngine.EventSystems;

public class RotateObject : MonoBehaviour, IDragHandler
{
    public float _speedOnTablet;
    public float _speedInEditor;

    public void OnDrag(PointerEventData eventData)
    {
        if (Input.touchSupported)
        {
            if (Input.touchCount == 1)
            {
                transform.Rotate(new Vector3(0, -Input.touches[0].deltaPosition.x, 0) * Time.deltaTime * _speedOnTablet); 
            }
        }
        else
        {
            transform.Rotate(new Vector3(0, -Input.GetAxis("Mouse X"), 0) * Time.deltaTime * _speedInEditor);
        }
    }
}