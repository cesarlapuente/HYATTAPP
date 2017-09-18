using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A nested list UI that allows you to hide the children by clicking on an element of the list.
/// Usually used in a ScrollView, this script should be added to the "Content" GameObject of the ScrollView.
/// Should be used with a VerticalLayoutGroup to place the elements properly.
/// </summary>
public class NestedListUI : MonoBehaviour
{
    public ElementListUI _prefabElementList;
    public SubElementListUI _prefabSubElementList;
    public Button _prefabSubElementButton;

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

        SubElementListUI subElementContainer = Instantiate(_prefabSubElementList);
        subElementContainer.transform.SetParent(parent);
        subElementContainer._contentRectTransform = GetComponent<RectTransform>();

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
                Button subElement = Instantiate(_prefabSubElementButton);
                subElement.transform.SetParent(subElementContainer.transform);
                subElement.GetComponentInChildren<Text>().text = roomCategory._elements[i]._name;
                subElementContainer._defaultHeight += _prefabSubElementButton.GetComponent<RectTransform>().sizeDelta.y;
            }
        }
    }

    public void Awake()
    {
        Room[] underOneRoof =
        {
            new Room(LanguageManager.Instance.GetText("5"), ""),
            new Room(LanguageManager.Instance.GetText("6"), ""),
            new Room(LanguageManager.Instance.GetText("7"), ""),
            new Room(LanguageManager.Instance.GetText("8"), ""),
            new Room(LanguageManager.Instance.GetText("9"), ""),
            new Room(LanguageManager.Instance.GetText("10"), ""),
            new Room(LanguageManager.Instance.GetText("11"), ""),
            new Room(LanguageManager.Instance.GetText("12"), ""),
            new Room(LanguageManager.Instance.GetText("13"), ""),
            new Room(LanguageManager.Instance.GetText("14"), ""),
            new Room(LanguageManager.Instance.GetText("15"), ""),
            new Room(LanguageManager.Instance.GetText("16"), "")
        };

        Room[] roomsAndSuites =
        {
            new Room(LanguageManager.Instance.GetText("19"), ""),
            new Room(LanguageManager.Instance.GetText("20"), ""),
            new Room(LanguageManager.Instance.GetText("21"), ""),
            new Room(LanguageManager.Instance.GetText("22"), ""),
            new Room(LanguageManager.Instance.GetText("23"), ""),
            new Room(LanguageManager.Instance.GetText("24"), ""),
            new Room(LanguageManager.Instance.GetText("25"), ""),
            new Room(LanguageManager.Instance.GetText("26"), ""),
            new Room(LanguageManager.Instance.GetText("27"), ""),
            new Room(LanguageManager.Instance.GetText("28"), ""),
            new Room(LanguageManager.Instance.GetText("29"), "")
        };

        Room[] palaisDesCongresMeetingSpaces =
        {
            new Room(LanguageManager.Instance.GetText("30"), ""),
            new Room(LanguageManager.Instance.GetText("31"), ""),
            new Room(LanguageManager.Instance.GetText("32"), ""),
            new Room(LanguageManager.Instance.GetText("33"), ""),
            new Room(LanguageManager.Instance.GetText("34"), ""),
            new Room(LanguageManager.Instance.GetText("35"), ""),
            new Room(LanguageManager.Instance.GetText("36"), ""),
            new Room(LanguageManager.Instance.GetText("37"), ""),
            new Room(LanguageManager.Instance.GetText("38"), ""),
            new Room(LanguageManager.Instance.GetText("39"), "")
        };

        Room[] labsMeetingSpaces =
        {
            new Room(LanguageManager.Instance.GetText("41"), ""),
            new Room(LanguageManager.Instance.GetText("42"), ""),
            new Room(LanguageManager.Instance.GetText("43"), ""),
            new Room(LanguageManager.Instance.GetText("44"), ""),
            new Room(LanguageManager.Instance.GetText("45"), ""),
            new Room(LanguageManager.Instance.GetText("46"), ""),
            new Room(LanguageManager.Instance.GetText("47"), ""),
            new Room(LanguageManager.Instance.GetText("48"), ""),
            new Room(LanguageManager.Instance.GetText("49"), ""),
            new Room(LanguageManager.Instance.GetText("50"), ""),
            new Room(LanguageManager.Instance.GetText("51"), ""),
            new Room(LanguageManager.Instance.GetText("52"), "")
        };

        Room[] atelierMeetingSpaces =
        {
            new Room(LanguageManager.Instance.GetText("54"), ""),
            new Room(LanguageManager.Instance.GetText("55"), ""),
            new Room(LanguageManager.Instance.GetText("56"), ""),
            new Room(LanguageManager.Instance.GetText("57"), ""),
            new Room(LanguageManager.Instance.GetText("58"), ""),
            new Room(LanguageManager.Instance.GetText("59"), ""),
            new Room(LanguageManager.Instance.GetText("60"), ""),
            new Room(LanguageManager.Instance.GetText("61"), ""),
            new Room(LanguageManager.Instance.GetText("62"), ""),
            new Room(LanguageManager.Instance.GetText("63"), ""),
            new Room(LanguageManager.Instance.GetText("64"), ""),
            new Room(LanguageManager.Instance.GetText("65"), ""),
            new Room(LanguageManager.Instance.GetText("66"), ""),
            new Room(LanguageManager.Instance.GetText("67"), "")
        };

        Room[] towerMeetingSpaces =
        {
            new Room(LanguageManager.Instance.GetText("69"), ""),
            new Room(LanguageManager.Instance.GetText("70"), ""),
            new Room(LanguageManager.Instance.GetText("71"), ""),
            new Room(LanguageManager.Instance.GetText("72"), ""),
            new Room(LanguageManager.Instance.GetText("73"), ""),
            new Room(LanguageManager.Instance.GetText("74"), ""),
        };

        Room[] meetingSpacesCapacity =
        {
            new Room(LanguageManager.Instance.GetText("76"), ""),
            new Room(LanguageManager.Instance.GetText("77"), "")
        };

        Room[] catering =
        {
            new Room(LanguageManager.Instance.GetText("79"), ""),
            new Room(LanguageManager.Instance.GetText("80"), "")
        };

        RoomCategory[] eventSpacesCategories =
        {
            new RoomCategory(LanguageManager.Instance.GetText("30"), palaisDesCongresMeetingSpaces),
            new RoomCategory(LanguageManager.Instance.GetText("40"), labsMeetingSpaces),
            new RoomCategory(LanguageManager.Instance.GetText("53"), atelierMeetingSpaces),
            new RoomCategory(LanguageManager.Instance.GetText("68"), towerMeetingSpaces),
            new RoomCategory(LanguageManager.Instance.GetText("75"), meetingSpacesCapacity),
            new RoomCategory(LanguageManager.Instance.GetText("78"), catering)
        };

        RoomCategory[] roomCategories =
        {
            new RoomCategory(LanguageManager.Instance.GetText("4"), underOneRoof),
            new RoomCategory(LanguageManager.Instance.GetText("18"), roomsAndSuites),
            new RoomCategory(LanguageManager.Instance.GetText("81"), eventSpacesCategories)
        };

        InitList(roomCategories);
    }
}