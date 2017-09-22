using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubElementListUI : MonoBehaviour {

    public Room _room;
    public Carousel _carousel;

    public void OpenGalery()
    {
        UIManager.Instance._carouselContainer.SetActive(true);
        _carousel.Init(_room._imagePaths);
    }
}
