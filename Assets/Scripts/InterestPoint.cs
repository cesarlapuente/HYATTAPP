﻿using UnityEngine;

public class InterestPoint : MonoBehaviour
{
    public string _name;
    public string _titleKey;
    public string _descriptionKey;
    public string _imagePath;

    private void Update()
    {
        transform.LookAt(Camera.main.transform.position, Vector3.up);
    }

    public void OpenPopUp()
    {
        UIManager.Instance.OpenHotelViewPopUp(this);
    }
}