using UnityEngine;
using UnityEngine.EventSystems;

public class RotateObject : MonoBehaviour, IDragHandler
{
    public float _speed;

    public void OnDrag(PointerEventData eventData)
    {
        if (Input.touchSupported)
        {
            transform.Rotate(new Vector3(0, -Input.touches[0].deltaPosition.x, 0) * Time.deltaTime * _speed * 0.5f);
        }
        else
        {
            transform.Rotate(new Vector3(0, -Input.GetAxis("Mouse X"), 0) * Time.deltaTime * _speed);
        }
    }
}