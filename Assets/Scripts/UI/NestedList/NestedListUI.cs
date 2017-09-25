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
                subElement._roomElement = roomCategory._elements[i];
                subElement._carousel = _carousel;
                subElement.transform.SetParent(subElementContainer.transform);
                subElement.GetComponentInChildren<Text>().text = roomCategory._elements[i]._name;
                subElementContainer._defaultHeight += _prefabSubElementButton.GetComponent<RectTransform>().sizeDelta.y;
                subElement.transform.localScale = Vector3.one;
            }
        }
    }

    public void Awake()
    {
        string underOneRoofPath = "Images/EspaceView/Under One Roof/";

        Room[] underOneRoof =
        {
            new Room(LanguageManager.Instance.GetText("5"), new string[]{underOneRoofPath + "A) LOBBY SPACE"}),
            new Room(LanguageManager.Instance.GetText("6"), new string[]{underOneRoofPath + "B) LOBBY BAR -_TOP_CHANGE", underOneRoofPath + "B) LOBBY BAR"}),
            new Room(LanguageManager.Instance.GetText("7"), new string[]{underOneRoofPath + "C) LOBBY RESTAURANT - TOP_CHANGE", underOneRoofPath + "C) LOBBY RESTAURANT - TOP_CHANGE", underOneRoofPath + "C) LOBBY RESTAURANT_RIGHT _CHANGE"}),
            new Room(LanguageManager.Instance.GetText("8"), new string[]{underOneRoofPath + "D) LOBBY MARKET_TOP_CHANGE", underOneRoofPath + "D) MARKET"}),
            new Room(LanguageManager.Instance.GetText("9"), new string[]{underOneRoofPath + "E) PANORAMIC BAR_FRONT", underOneRoofPath + "E) PANORAMIC BAR"}),
            new Room(LanguageManager.Instance.GetText("10"), new string[]{underOneRoofPath + "F) REGENCY CLUB"}),
            new Room(LanguageManager.Instance.GetText("12"), new string[]{ underOneRoofPath + "G) FITNESS CENTER_RIGHT", underOneRoofPath + "G) FITNESS CENTER_TOP", underOneRoofPath + "G) FITNESS CENTER"}),
            new Room(LanguageManager.Instance.GetText("13"), new string[]{underOneRoofPath + "H) METRO ACCESS 2", underOneRoofPath + "H) METRO ACCESS_TOP", underOneRoofPath + "H) METRO ACCESS 1"}),
            new Room(LanguageManager.Instance.GetText("14"), new string[]{underOneRoofPath + "I) PARKING ACCESS_TOP", underOneRoofPath + "I) PARKING"}),
            new Room(LanguageManager.Instance.GetText("15"), new string[]{underOneRoofPath + "J) AIRPORT SHUTLE_TOP_CHANGE", underOneRoofPath + "J) AIRPORT SHUTTLE"}),
            new Room(LanguageManager.Instance.GetText("16"), new string[]{underOneRoofPath + "K) PALAIS DES CONGRES-BOUTIQUES_LEFT_CHANGE", underOneRoofPath + "K) PALAIS DES CONGRES-BOUTIQUES_RIGHT_CHANGE", underOneRoofPath + "K) PALAIS DES CONGRES-BOUTIQUES_TOP_CHANGE", underOneRoofPath + "K) CONGRESS CENTER BOUTIQUES"}),
            new Room(LanguageManager.Instance.GetText("17"), new string[]{underOneRoofPath + "L) MOVIE THEATER_LEFT_CHANGE", underOneRoofPath + "L) MOVIE THEATER_RIGHT_CHANGE", underOneRoofPath + "L) MOVIE THEATER_TOP_CHANGE", underOneRoofPath + "L) MOVIE THEATER"})
        };

        string roomsAndSuitesPath = "Images/EspaceView/Room And Suites/";

        Room[] roomsAndSuites =
        {
            new Room(LanguageManager.Instance.GetText("19"), new string[]{ roomsAndSuitesPath + "A) STANDAR_FRONT", roomsAndSuitesPath + "A) STANDARD", roomsAndSuitesPath + "A) STANDARD_"}),
            new Room(LanguageManager.Instance.GetText("20"), new string[]{ roomsAndSuitesPath + "B) STANDARD EIFFEL TOWER", roomsAndSuitesPath + "B) STANDARD EIFFEL_TOWER"}),
            new Room(LanguageManager.Instance.GetText("21"), new string[]{ roomsAndSuitesPath + "C) DELUXE", roomsAndSuitesPath + "C) DELUXE_FRONT", roomsAndSuitesPath + "C) DELUXE_"}),
            new Room(LanguageManager.Instance.GetText("22"), new string[]{ roomsAndSuitesPath + "D) DELUXE EIFFEL TOWER", roomsAndSuitesPath + "D) DELUXE EIFFEL TOWER_"}),
            new Room(LanguageManager.Instance.GetText("23"), new string[]{ roomsAndSuitesPath + "E) CLUB", roomsAndSuitesPath + "E) CLUB_"}),
            new Room(LanguageManager.Instance.GetText("24"), new string[]{ roomsAndSuitesPath + "F) CLUB EIFFEL TOWER_", roomsAndSuitesPath + "F) CLUB EIFFEL TOWER"}),
            new Room(LanguageManager.Instance.GetText("25"), new string[]{ roomsAndSuitesPath + "G) REGENCY SUITE KING", roomsAndSuitesPath + "G) REGENCY SUITE_KING"}),
            new Room(LanguageManager.Instance.GetText("26"), new string[]{ roomsAndSuitesPath + "H) REGENCY SUITE KING EIFFEL TOWER_", roomsAndSuitesPath + "H) REGENCY SUITE KING EIFFEL TOWER"}),
            new Room(LanguageManager.Instance.GetText("27"), new string[]{ roomsAndSuitesPath + "I) REGENCY EXECUTIVE SUITE", roomsAndSuitesPath + "I) REGENCY EXECUTIVE SUITE_FRONT", roomsAndSuitesPath + "I) REGENCY EXECUTIVE"}),
            new Room(LanguageManager.Instance.GetText("28"), new string[]{ roomsAndSuitesPath + "J) LOFT_SUITE_", roomsAndSuitesPath + "J) LOFT_SUITE"}),
            new Room(LanguageManager.Instance.GetText("29"), new string[]{ roomsAndSuitesPath + "K) PRESIDENTIAL SUITE", roomsAndSuitesPath + "K) PRESIDENTIAL_SUITE"})
        };

        string eventSpacesPath = "Images/EspaceView/Event Spaces/";

        RoomElement[] eventSpaces =
        {
            new Room(LanguageManager.Instance.GetText("31"), new string[] {
                eventSpacesPath + "a) CONGRESS CENTER MEETING SPACES",
                eventSpacesPath + "a) CONGRESS CENTER MEETING SPACES (2)",
                eventSpacesPath + "a) CONGRESS CENTER MEETING SPACES_TOP",
                eventSpacesPath + "a) CONGRESS CENTER MEETING SPACES_RIGHT CHANGE",
                eventSpacesPath + "A) CONGRESS CENTER MEETING SPACES_",
                eventSpacesPath + "A) CONGRESS CENTER MEETING SPACES_0",
                eventSpacesPath + "A) CONGRESS CENTER MEETING SPACES_1",
                eventSpacesPath + "A) CONGRESS CENTER MEETING SPACES_2",
                eventSpacesPath + "A) CONGRESS CENTER MEETING SPACES_3",
                eventSpacesPath + "A1) GRAND AMPHITHEATRE",
                eventSpacesPath + "A2) HALL NEULLY",
                eventSpacesPath + "A3) HALL TERNES",
                eventSpacesPath + "A4) HALL PARIS",
                eventSpacesPath + "A5) HALL PASSY",
                eventSpacesPath + "A6) SALLE PASSY",
                eventSpacesPath + "A7) HALL MAILLOT",
                eventSpacesPath + "A8) SALLE MAILLOT",
                eventSpacesPath + "A9) AMPHITHEATRE BLEU",
                eventSpacesPath + "A10) hall BORDEAUX",
                eventSpacesPath + "A11) hall havana",
                eventSpacesPath + "A12) AMPHITHEATRE  bordeaux",
                eventSpacesPath + "A13) AMPHITHEATRE HAVANE"
                }),
            new Room(LanguageManager.Instance.GetText("32"), new string[]{
                eventSpacesPath + "B) LAB MEETING SPACES (2)",
                eventSpacesPath + "B) LABS MEETING SPACES_LEFT",
                eventSpacesPath + "B) LABS MEETING SPACES_RIGHT",
                eventSpacesPath + "B) LABS MEETING SPACES_TOP_CHANGE",
                eventSpacesPath + "B) LAB MEETING SPACES"
            }),
            new Room(LanguageManager.Instance.GetText("33"), new string[]{
                eventSpacesPath + "C) STUDIO MEETING SPACES_LEFT",
                eventSpacesPath + "C) STUDIO MEETING SPACES_TOP_CHANGE",
                eventSpacesPath + "C) STUDIO MEETING SPACES_RIGHT",
                eventSpacesPath + "C) STUDIO MEETING SPACES_2",
                eventSpacesPath + "C) STUDIO MEETING SPACES",
                eventSpacesPath + "C) STUDIO MEETING SPACE_BRIDGE",
                eventSpacesPath + "C) STUDIO MEETING SPACES_Prefunction-Rendering",
                eventSpacesPath + "C) STUDIO MEETING SPACES_Ballroom-3D-Rendering",
                eventSpacesPath + "C) STUDIO MEETING SPACES_CONGRESS CORRIDOR",
                eventSpacesPath + "C) STUDIO MEETING SPACES_CONGRESS ENTRY",
                eventSpacesPath + "C) STUDIO MEETING SPACES_meeting room_1",
                eventSpacesPath + "C) STUDIO MEETING SPACES_TOILET LOUNGE AREA"
                }),
            new Room(LanguageManager.Instance.GetText("34"), new string[]{
                eventSpacesPath + "D) TOWER MEETING SPACES_FRONT",
                eventSpacesPath + "D) TOWER MEETING SPACES_RIGHT",
                eventSpacesPath + "D) TOWER MEETING ROOMS"
                }),
            new RoomPdf(LanguageManager.Instance.GetText("35"), "StudiosCapacityEn"),
            new RoomPdf(LanguageManager.Instance.GetText("36"), "StudiosCapaciteFr")
        };

        RoomElement[] catering =
        {
            new RoomPdf(LanguageManager.Instance.GetText("38"), "CateringSpringSummerEn"),
            new RoomPdf(LanguageManager.Instance.GetText("39"), "CateringFallWinterEn"),
            new RoomPdf(LanguageManager.Instance.GetText("40"), "CateringPrintempsEteFr"),
            new RoomPdf(LanguageManager.Instance.GetText("41"), "CateringAutomneHiverFr")
        };

        RoomCategory[] roomCategories =
        {
            new RoomCategory(LanguageManager.Instance.GetText("4"), underOneRoof),
            new RoomCategory(LanguageManager.Instance.GetText("18"), roomsAndSuites),
            new RoomCategory(LanguageManager.Instance.GetText("30"), eventSpaces),
            new RoomCategory(LanguageManager.Instance.GetText("37"), catering)
        };

        InitList(roomCategories);
    }
}