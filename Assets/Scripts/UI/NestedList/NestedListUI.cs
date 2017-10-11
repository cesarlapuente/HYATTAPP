using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// A nested list UI that allows you to hide the children by clicking on an element of the list.
/// Usually used in a ScrollView, this script should be added to the "Content" GameObject of the ScrollView.
/// Should be used with a VerticalLayoutGroup to place the elements properly.
/// </summary>
public class NestedListUI : MonoBehaviour
{
    public ElementListUI _prefabElementList;
    public SubElementContainerListUI _prefabSubElementList;
    public SubElementListUI _prefabSubElementButton;
    public Carousel _carousel;

    public Dictionary<int, SubElementListUI> _subElementsDictionnary = new Dictionary<int, SubElementListUI>();

    public void InitList(RoomCategory[] _roomCategories)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        for (int i = 0; i < _roomCategories.Length; i++)
        {
            CreateCategory(_roomCategories[i], transform);
        }

        float contentSize = _roomCategories.Length * _prefabElementList.GetComponent<RectTransform>().sizeDelta.y;
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, contentSize);
    }

    private void CreateCategory(RoomCategory roomCategory, Transform parent)
    {
        ElementListUI element = Instantiate(_prefabElementList);
        element.GetComponentInChildren<Text>().text = roomCategory._name;
        element.transform.SetParent(parent);
        // We need to set the scale at (1,1,1) because the Canvas scaler messes with the scale when using "Scale With Screen Size"
        element.transform.localScale = Vector3.one;

        SubElementContainerListUI subElementContainer = Instantiate(_prefabSubElementList);
        subElementContainer.transform.SetParent(parent);
        subElementContainer._contentRectTransform = GetComponent<RectTransform>();
        subElementContainer.transform.localScale = Vector3.one;

        element._subElement = subElementContainer;

        for (int i = 0; i < roomCategory._elements.Length; i++)
        {
            // Try to cast the element as a RoomCategory
            RoomCategory childRoomCategory = roomCategory._elements[i] as RoomCategory;

            // Call CreateCategory recursively if the child is a RoomCategory
            if (childRoomCategory != null)
            {
                CreateCategory(childRoomCategory, subElementContainer.transform);
                subElementContainer._defaultHeight += _prefabElementList.GetComponent<RectTransform>().sizeDelta.y;
            }
            else
            {
                SubElementListUI subElement = Instantiate(_prefabSubElementButton);
                subElement._room = roomCategory._elements[i] as Room;
                subElement._carousel = _carousel;
                subElement.transform.SetParent(subElementContainer.transform);
                subElement.GetComponentInChildren<Text>().text = roomCategory._elements[i]._name;
                subElementContainer._defaultHeight += _prefabSubElementButton.GetComponent<RectTransform>().sizeDelta.y;
                subElement.transform.localScale = Vector3.one;

                // We had all the subElements to the dictionnary to make it easier to retrive them
                // The Dictionnary is essential to the interaction with Interest Points, it allows us to open the gallery when viewing an Interest Point
                _subElementsDictionnary.Add(subElement._room._id, subElement);
            }
        }
    }

    public void Awake()
    {

    }
}