using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubElementListUI : MonoBehaviour {

    public RoomElement _roomElement;
    public Carousel _carousel;

    public void OpenGalery()
    {
        Room room = _roomElement as Room;
        Debug.Log(_roomElement.GetType());
        if (room != null)
        {
            UIManager.Instance._carouselContainer.SetActive(true);
            UIManager.Instance._espaceViewTitle.text = _roomElement._name;
            _carousel.Init(room._imagePaths);
        }
        else
        {
            RoomPdf roomPdf = _roomElement as RoomPdf;
            if (roomPdf != null)
            {
                Debug.Log(Application.streamingAssetsPath + "/" + roomPdf._pdfPath);
                Application.OpenURL(Application.streamingAssetsPath + "/" + roomPdf._pdfPath);
            }
        }

    }
}
