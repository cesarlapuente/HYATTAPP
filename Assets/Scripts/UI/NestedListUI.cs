using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NestedListUI : MonoBehaviour {

    public ElementListUI _prefabElementList;
    public SubElementListUI _prefabSubElementList;
    public Button _prefabSubElementButton;

    public void InitList(RoomCategory[] _roomCategories)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        for (int i = 0; i < _roomCategories.Length; i++)
        {
            ElementListUI element = Instantiate(_prefabElementList);
            element.GetComponentInChildren<Text>().text = _roomCategories[i]._name;
            element.transform.SetParent(transform);

            SubElementListUI subElementContainer = Instantiate(_prefabSubElementList);
            subElementContainer.transform.SetParent(transform);
            subElementContainer._contentRectTransform = rectTransform;
            subElementContainer._defaultHeight = _roomCategories[i]._rooms.Length * _prefabSubElementButton.GetComponent<RectTransform>().sizeDelta.y;

            element._subElement = subElementContainer;

            for (int j = 0; j < _roomCategories[i]._rooms.Length; j++)
            {
                Button subElement = Instantiate(_prefabSubElementButton);
                subElement.transform.SetParent(subElementContainer.transform);
                subElement.GetComponentInChildren<Text>().text = _roomCategories[i]._rooms[j]._name;
            }
        }

        float contentSize = _roomCategories.Length * _prefabElementList.GetComponent<RectTransform>().sizeDelta.y;
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, contentSize);

        // Make sure to change content size when modifying the height of the subelement
    }

    public void Awake()
    {
        Room[] rooms1 = { new Room("1", ""), new Room("2", ""), new Room("3", ""), new Room("4", "") };
        Room[] rooms2 = { new Room("11", ""), new Room("22", "")};
        Room[] rooms3 = { new Room("111", ""), new Room("222", ""), new Room("333", "")};

        RoomCategory[] roomCategories = { new RoomCategory("Cat 1", rooms1), new RoomCategory("Cat 2", rooms2), new RoomCategory("Cat 3", rooms3) };

        InitList(roomCategories);
    }
}
