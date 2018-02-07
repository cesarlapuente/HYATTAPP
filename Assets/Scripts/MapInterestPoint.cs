using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInterestPoint : MonoBehaviour
{
    public string _nameKey;
    public string _distance;
    public string _carTime;
    public string _walkTime;
    public string _busTime;
    public string _imagePath;

    public void OpenPopUp()
    {
        AppManager.Instance.OpenMapViewPopUp(this);
    }
}
