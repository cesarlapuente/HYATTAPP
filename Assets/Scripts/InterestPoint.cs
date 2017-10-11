using UnityEngine;

/// <summary>
/// This element is used for the 3D Hotel View
/// </summary>
public class InterestPoint : MonoBehaviour
{
    public enum Type { UnderOneRoof, RoomsAndSuites, EventSpaces }

    public string _name;
    public string _titleKey;
    public string _descriptionKey;
    public string _imagePath;
    public Type _type;
    // Room ID used to display its galery
    public int _roomId;

    private void Update()
    {
        transform.LookAt(Camera.main.transform.position, Vector3.up);
    }

    public void OpenPopUp()
    {
        AppManager.Instance.OpenHotelViewPopUp(this);
    }

}