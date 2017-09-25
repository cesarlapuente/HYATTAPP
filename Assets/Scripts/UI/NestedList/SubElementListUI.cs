using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubElementListUI : MonoBehaviour {

    public Room _room;
    public Carousel _carousel;

    public void OpenGalery()
    {
        UIManager.Instance._carouselContainer.SetActive(true);
        UIManager.Instance._espaceViewTitle.text = _room._name;
        _carousel.Init(_room._imagePaths);
    }
}
