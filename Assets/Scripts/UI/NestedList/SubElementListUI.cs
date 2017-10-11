using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubElementListUI : MonoBehaviour {

    public Room _room;
    public Carousel _carousel;

    public void OpenGalery()
    {
        AppManager.Instance._carouselContainer.SetActive(true);
        AppManager.Instance._espaceViewTitle.text = _room._name;

        Sprite[] sprites;

        if (_room._imageFolderPath != null)
        {
            sprites = Resources.LoadAll<Sprite>(_room._imageFolderPath);
        }
        else
        {
            sprites = new Sprite[_room._imagePaths.Length];
            for (int i = 0; i < _room._imagePaths.Length; i++)
            {
                sprites[i] = Resources.Load<Sprite>(_room._imagePaths[i]);
            }
        }
        _carousel.Init(sprites);
    }
}
