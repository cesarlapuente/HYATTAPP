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
    public SubElementContainerListUI _prefabSubElementList;
    public SubElementListUI _prefabSubElementButton;
    public Carousel _carousel;

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

        SubElementContainerListUI subElementContainer = Instantiate(_prefabSubElementList);
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
                SubElementListUI subElement = Instantiate(_prefabSubElementButton);
                subElement._room = roomCategory._elements[i] as Room;
                subElement._carousel = _carousel;
                subElement.transform.SetParent(subElementContainer.transform);
                subElement.GetComponentInChildren<Text>().text = roomCategory._elements[i]._name;
                subElementContainer._defaultHeight += _prefabSubElementButton.GetComponent<RectTransform>().sizeDelta.y;
            }
        }
    }

    public void Awake()
    {
        string underOneRoofPath = "Images/EspaceView/Under One Roof/";

        Room[] underOneRoof =
        {
            new Room(LanguageManager.Instance.GetText("5"), new string[]{underOneRoofPath + "A) LOBBY SPACE"}),
            new Room(LanguageManager.Instance.GetText("6"), new string[]{underOneRoofPath + "B) LOBBY BAR -_TOP_CHANGE"}),
            new Room(LanguageManager.Instance.GetText("7"), new string[]{underOneRoofPath + "C) LOBBY RESTAURANT - TOP_CHANGE", underOneRoofPath + "C) LOBBY RESTAURANT - TOP_CHANGE", underOneRoofPath + "C) LOBBY RESTAURANT_RIGHT _CHANGE"}),
            new Room(LanguageManager.Instance.GetText("8"), new string[]{underOneRoofPath + "D) LOBBY MARKET_TOP_CHANGE", underOneRoofPath + "D) MARKET"}),
            new Room(LanguageManager.Instance.GetText("9"), new string[]{underOneRoofPath + "E) PANORAMIC BAR", underOneRoofPath + "E) PANORAMIC BAR_FRONT"}),
            new Room(LanguageManager.Instance.GetText("10"), new string[]{underOneRoofPath + "F) REGENCY CLUB"}),
            new Room(LanguageManager.Instance.GetText("11"), new string[]{underOneRoofPath + ""}),
            new Room(LanguageManager.Instance.GetText("12"), new string[]{underOneRoofPath + "G) FITNESS CENTER", underOneRoofPath + "G) FITNESS CENTER_RIGHT", underOneRoofPath + "G) FITNESS CENTER_TOP"}),
            new Room(LanguageManager.Instance.GetText("13"), new string[]{underOneRoofPath + "H) METRO ACCESS 1", underOneRoofPath + "H) METRO ACCESS 2", underOneRoofPath + "H) METRO ACCESS_TOP"}),
            new Room(LanguageManager.Instance.GetText("14"), new string[]{underOneRoofPath + "I) PARKING", underOneRoofPath + "I) PARKING ACCESS_TOP"}),
            new Room(LanguageManager.Instance.GetText("15"), new string[]{underOneRoofPath + "J) AIRPORT SHUTLE_TOP_CHANGE", underOneRoofPath + "J) AIRPORT SHUTTLE"}),
            new Room(LanguageManager.Instance.GetText("16"), new string[]{underOneRoofPath + "K) CONGRESS CENTER BOUTIQUES", underOneRoofPath + "K) PALAIS DES CONGRES-BOUTIQUES_LEFT_CHANGE", underOneRoofPath + "K) PALAIS DES CONGRES-BOUTIQUES_RIGHT_CHANGE", underOneRoofPath + "K) PALAIS DES CONGRES-BOUTIQUES_TOP_CHANGE"}),
            new Room(LanguageManager.Instance.GetText("17"), new string[]{underOneRoofPath + "L) MOVIE THEATER", underOneRoofPath + "L) MOVIE THEATER_LEFT_CHANGE", underOneRoofPath + "L) MOVIE THEATER_RIGHT_CHANGE", underOneRoofPath + "L) MOVIE THEATER_TOP_CHANGE"})
        };

        Room[] roomsAndSuites =
        {
            new Room(LanguageManager.Instance.GetText("19"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("20"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("21"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("22"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("23"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("24"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("25"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("26"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("27"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("28"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("29"), new string[]{""})
        };

        Room[] palaisDesCongresMeetingSpaces =
        {
            new Room(LanguageManager.Instance.GetText("30"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("31"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("32"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("33"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("34"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("35"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("36"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("37"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("38"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("39"), new string[]{""})
        };

        Room[] labsMeetingSpaces =
        {
            new Room(LanguageManager.Instance.GetText("41"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("42"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("43"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("44"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("45"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("46"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("47"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("48"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("49"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("50"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("51"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("52"), new string[]{""})
        };

        Room[] atelierMeetingSpaces =
        {
            new Room(LanguageManager.Instance.GetText("54"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("55"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("56"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("57"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("58"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("59"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("60"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("61"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("62"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("63"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("64"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("65"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("66"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("67"), new string[]{""})
        };

        Room[] towerMeetingSpaces =
        {
            new Room(LanguageManager.Instance.GetText("69"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("70"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("71"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("72"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("73"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("74"), new string[]{""}),
        };

        Room[] meetingSpacesCapacity =
        {
            new Room(LanguageManager.Instance.GetText("76"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("77"), new string[]{""})
        };

        Room[] catering =
        {
            new Room(LanguageManager.Instance.GetText("79"), new string[]{""}),
            new Room(LanguageManager.Instance.GetText("80"), new string[]{""})
        };

        RoomCategory[] eventSpacesCategories =
        {
            new RoomCategory(LanguageManager.Instance.GetText("30"), palaisDesCongresMeetingSpaces),
            new RoomCategory(LanguageManager.Instance.GetText("40"), labsMeetingSpaces),
            new RoomCategory(LanguageManager.Instance.GetText("53"), atelierMeetingSpaces),
            new RoomCategory(LanguageManager.Instance.GetText("68"), towerMeetingSpaces),
            new RoomCategory(LanguageManager.Instance.GetText("75"), meetingSpacesCapacity)
        };

        RoomCategory[] roomCategories =
        {
            new RoomCategory(LanguageManager.Instance.GetText("4"), underOneRoof),
            new RoomCategory(LanguageManager.Instance.GetText("18"), roomsAndSuites),
            new RoomCategory(LanguageManager.Instance.GetText("81"), eventSpacesCategories),
            new RoomCategory(LanguageManager.Instance.GetText("78"), catering)
        };

        InitList(roomCategories);
    }
}