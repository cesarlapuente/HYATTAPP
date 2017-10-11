using UnityEngine;
using UnityEngine.UI;

public class AppManager : MonoBehaviourSingleton<AppManager>
{
    public enum Screen { HotelView, EspaceView, MapView };

    private InterestPoint _currentInterestPoint;

    [Header("Menu")]
    public Text _title;
    public GameObject _hotelModel;
    public SpriteChanger _hotelSprite;
    public SpriteChanger _espaceSprite;
    public SpriteChanger _mapSprite;

    [Header("Views")]
    public GameObject _hotelView;
    public GameObject _espaceView;
    public GameObject _mapView;

    [Header("Hotel View")]
    public GameObject _hotelViewPopUp;
    public Image _hotelViewImage;
    public Text _hotelViewTitle;
    public Text _hotelViewDescription;

    [Header("Espace View")]
    public GameObject _carouselContainer;
    public Text _espaceViewTitle;
    public NestedListUI _nestedListUI;

    [Header("Map View")]
    public GameObject _mapViewPopUp;
    public Image _mapViewImage;
    public Text _mapViewTitle;
    public Text _mapViewDistance;
    public Text _mapViewCarTime;
    public Text _mapViewWalkTime;
    public GameObject _mapViewWalkIcon;

    public void ChangeScreen(int screenNumber)
    {
        ChangeScreen((Screen)screenNumber);
    }

    public void Start()
    {
        ChangeScreen(Screen.HotelView);
    }

    public void ChangeScreen(Screen screen)
    {
        switch (screen)
        {
            case Screen.HotelView:
                _hotelView.SetActive(true);
                _espaceView.SetActive(false);
                _mapView.SetActive(false);
                _hotelModel.SetActive(true);
                _title.text = LanguageManager.Instance.GetText("0");
                _hotelSprite.ChangeSprite(0);
                _espaceSprite.ChangeSprite(1);
                _mapSprite.ChangeSprite(1);
                break;

            case Screen.EspaceView:
                _hotelView.SetActive(false);
                _espaceView.SetActive(true);
                _mapView.SetActive(false);
                _hotelModel.SetActive(false);
                _title.text = LanguageManager.Instance.GetText("1");
                _hotelSprite.ChangeSprite(1);
                _espaceSprite.ChangeSprite(0);
                _mapSprite.ChangeSprite(1);
                break;

            case Screen.MapView:
                _hotelView.SetActive(false);
                _espaceView.SetActive(false);
                _mapView.SetActive(true);
                _hotelModel.SetActive(false);
                _title.text = LanguageManager.Instance.GetText("2");
                _hotelSprite.ChangeSprite(1);
                _espaceSprite.ChangeSprite(1);
                _mapSprite.ChangeSprite(0);
                break;

            default:
                break;
        }
    }

    public void Update()
    {
        // Cast a ray to check if we the user clicked on an InterestPoint
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit raycastHit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit, Mathf.Infinity, LayerMask.GetMask("InterestPoints")))
            {
                OpenHotelViewPopUp(raycastHit.collider.GetComponent<InterestPoint>());
            }
        }
    }

    public void OpenHotelViewPopUp(InterestPoint interestPoint)
    {
        if (_currentInterestPoint)
        {
            _currentInterestPoint.GetComponent<SpriteChanger>().ChangeSprite(0);
        }
        _currentInterestPoint = interestPoint;
        interestPoint.GetComponent<SpriteChanger>().ChangeSprite(1);
        _hotelViewPopUp.SetActive(true);
        string path = "";
        switch (interestPoint._type)
        {
            case InterestPoint.Type.UnderOneRoof:
                path = "Images/HotelView/Under One Roof/";
                break;
            case InterestPoint.Type.RoomsAndSuites:
                path = "Images/HotelView/Rooms And Suites/";
                break;
            case InterestPoint.Type.EventSpaces:
                path = "Images/HotelView/Event Spaces/";
                break;
            default:
                break;
        }
        Debug.Log(path + interestPoint._imagePath);
        _hotelViewImage.sprite = Resources.Load<Sprite>(path + interestPoint._imagePath);
        _hotelViewTitle.text = LanguageManager.Instance.GetText(interestPoint._titleKey);
        _hotelViewDescription.text = LanguageManager.Instance.GetText(interestPoint._descriptionKey);
    }

    public void OpenMapViewPopUp(MapInterestPoint mapInterestPoint)
    {
        _mapViewPopUp.SetActive(true);
        _mapViewImage.sprite = Resources.Load<Sprite>("Images/MapView/" + mapInterestPoint._imagePath);
        _mapViewTitle.text = LanguageManager.Instance.GetText(mapInterestPoint._nameKey);
        _mapViewDistance.text = mapInterestPoint._distance;
        _mapViewCarTime.text = mapInterestPoint._carTime;
        _mapViewWalkTime.text = mapInterestPoint._walkTime;
        _mapViewWalkIcon.SetActive(!string.IsNullOrEmpty(mapInterestPoint._walkTime));
    }

    public void OpenCurrentInterestPointGalery()
    {
        SubElementListUI subElement;
        if(_nestedListUI._subElementsDictionnary.TryGetValue(_currentInterestPoint._roomId, out subElement))
        {
            ChangeScreen(Screen.EspaceView);
            subElement.OpenGalery();
        }
    }

    public void Awake()
    {
        string underOneRoofPath = "Images/EspaceView/Under One Roof/";

        Room[] underOneRoof =
        {
            new Room(0, LanguageManager.Instance.GetText("5"), new string[]{underOneRoofPath + "A) LOBBY SPACE"}),
            new Room(1, LanguageManager.Instance.GetText("6"), new string[]{underOneRoofPath + "B) LOBBY BAR -_TOP_CHANGE", underOneRoofPath + "B) LOBBY BAR"}),
            new Room(2, LanguageManager.Instance.GetText("7"), new string[]{underOneRoofPath + "C) LOBBY RESTAURANT - TOP_CHANGE", underOneRoofPath + "C) LOBBY RESTAURANT_RIGHT _CHANGE", underOneRoofPath + "C) LOBBY RESTAURANT"}),
            new Room(3, LanguageManager.Instance.GetText("8"), new string[]{underOneRoofPath + "D) LOBBY MARKET_TOP_CHANGE", underOneRoofPath + "D) MARKET"}),
            new Room(4, LanguageManager.Instance.GetText("9"), new string[]{underOneRoofPath + "E) PANORAMIC BAR_FRONT", underOneRoofPath + "E) PANORAMIC BAR"}),
            new Room(5, LanguageManager.Instance.GetText("10"), new string[]{underOneRoofPath + "F) REGENCY CLUB"}),
            new Room(6, LanguageManager.Instance.GetText("12"), new string[]{ underOneRoofPath + "G) FITNESS CENTER_RIGHT", underOneRoofPath + "G) FITNESS CENTER_TOP", underOneRoofPath + "G) FITNESS CENTER"}),
            new Room(7, LanguageManager.Instance.GetText("13"), new string[]{underOneRoofPath + "H) METRO ACCESS 2", underOneRoofPath + "H) METRO ACCESS_TOP", underOneRoofPath + "H) METRO ACCESS 1"}),
            new Room(8, LanguageManager.Instance.GetText("14"), new string[]{underOneRoofPath + "I) PARKING ACCESS_TOP", underOneRoofPath + "I) PARKING"}),
            new Room(9, LanguageManager.Instance.GetText("15"), new string[]{underOneRoofPath + "J) AIRPORT SHUTLE_TOP_CHANGE", underOneRoofPath + "J) AIRPORT SHUTTLE"}),
            new Room(10, LanguageManager.Instance.GetText("16"), new string[]{underOneRoofPath + "K) PALAIS DES CONGRES-BOUTIQUES_LEFT_CHANGE", underOneRoofPath + "K) PALAIS DES CONGRES-BOUTIQUES_RIGHT_CHANGE", underOneRoofPath + "K) PALAIS DES CONGRES-BOUTIQUES_TOP_CHANGE", underOneRoofPath + "K) CONGRESS CENTER BOUTIQUES"}),
            new Room(11, LanguageManager.Instance.GetText("17"), new string[]{underOneRoofPath + "L) MOVIE THEATER_LEFT_CHANGE", underOneRoofPath + "L) MOVIE THEATER_RIGHT_CHANGE", underOneRoofPath + "L) MOVIE THEATER_TOP_CHANGE", underOneRoofPath + "L) MOVIE THEATER"})
        };

        string roomsAndSuitesPath = "Images/EspaceView/Room And Suites/";

        Room[] roomsAndSuites =
        {
            new Room(12, LanguageManager.Instance.GetText("19"), new string[]{ roomsAndSuitesPath + "A) STANDAR_FRONT", roomsAndSuitesPath + "A) STANDARD", roomsAndSuitesPath + "A) STANDARD_"}),
            new Room(13, LanguageManager.Instance.GetText("20"), new string[]{ roomsAndSuitesPath + "B) STANDARD EIFFEL TOWER", roomsAndSuitesPath + "B) STANDARD EIFFEL_TOWER"}),
            new Room(14, LanguageManager.Instance.GetText("21"), new string[]{ roomsAndSuitesPath + "C) DELUXE", roomsAndSuitesPath + "C) DELUXE_FRONT", roomsAndSuitesPath + "C) DELUXE_"}),
            new Room(15, LanguageManager.Instance.GetText("22"), new string[]{ roomsAndSuitesPath + "D) DELUXE EIFFEL TOWER", roomsAndSuitesPath + "D) DELUXE EIFFEL TOWER_"}),
            new Room(16, LanguageManager.Instance.GetText("23"), new string[]{ roomsAndSuitesPath + "E) CLUB", roomsAndSuitesPath + "E) CLUB_"}),
            new Room(17, LanguageManager.Instance.GetText("24"), new string[]{ roomsAndSuitesPath + "F) CLUB EIFFEL TOWER_", roomsAndSuitesPath + "F) CLUB EIFFEL TOWER"}),
            new Room(18, LanguageManager.Instance.GetText("25"), new string[]{ roomsAndSuitesPath + "G) REGENCY SUITE KING", roomsAndSuitesPath + "G) REGENCY SUITE_KING"}),
            new Room(19, LanguageManager.Instance.GetText("26"), new string[]{ roomsAndSuitesPath + "H) REGENCY SUITE KING EIFFEL TOWER_", roomsAndSuitesPath + "H) REGENCY SUITE KING EIFFEL TOWER"}),
            new Room(20, LanguageManager.Instance.GetText("27"), new string[]{ roomsAndSuitesPath + "I) REGENCY EXECUTIVE SUITE", roomsAndSuitesPath + "I) REGENCY EXECUTIVE SUITE_FRONT", roomsAndSuitesPath + "I) REGENCY EXECUTIVE"}),
            new Room(21, LanguageManager.Instance.GetText("28"), new string[]{ roomsAndSuitesPath + "J) LOFT_SUITE_", roomsAndSuitesPath + "J) LOFT_SUITE"}),
            new Room(22, LanguageManager.Instance.GetText("29"), new string[]{ roomsAndSuitesPath + "K) PRESIDENTIAL SUITE", roomsAndSuitesPath + "K) PRESIDENTIAL_SUITE"})
        };

        string eventSpacesPath = "Images/EspaceView/Event Spaces/";

        Room[] eventSpaces =
        {
            new Room(23, LanguageManager.Instance.GetText("31"), new string[] {
                //eventSpacesPath + "a) CONGRESS CENTER MEETING SPACES",
                eventSpacesPath + "a) CONGRESS CENTER MEETING SPACES (2)",
                eventSpacesPath + "a) CONGRESS CENTER MEETING SPACES_TOP",
                //eventSpacesPath + "a) CONGRESS CENTER MEETING SPACES_RIGHT CHANGE",
                //eventSpacesPath + "A) CONGRESS CENTER MEETING SPACES_",
                eventSpacesPath + "A) CONGRESS CENTER MEETING SPACES_0",
                eventSpacesPath + "A) CONGRESS CENTER MEETING SPACES_1",
                eventSpacesPath + "A) CONGRESS CENTER MEETING SPACES_2",
                eventSpacesPath + "A) CONGRESS CENTER MEETING SPACES_3",
                eventSpacesPath + "A1) GRAND AMPHITHEATRE",
                //eventSpacesPath + "A2) HALL NEULLY",
                //eventSpacesPath + "A3) HALL TERNES",
                //eventSpacesPath + "A4) HALL PARIS",
                //eventSpacesPath + "A5) HALL PASSY",
                //eventSpacesPath + "A6) SALLE PASSY",
                //eventSpacesPath + "A7) HALL MAILLOT",
                //eventSpacesPath + "A8) SALLE MAILLOT",
                //eventSpacesPath + "A9) AMPHITHEATRE BLEU",
                //eventSpacesPath + "A10) hall BORDEAUX",
                //eventSpacesPath + "A11) hall havana",
                eventSpacesPath + "A12) AMPHITHEATRE  bordeaux"
                //eventSpacesPath + "A13) AMPHITHEATRE HAVANE"
                }),
            new Room(24, LanguageManager.Instance.GetText("32"), new string[]{
                eventSpacesPath + "B) LAB MEETING SPACES (2)",
                eventSpacesPath + "B) LABS MEETING SPACES_LEFT",
                eventSpacesPath + "B) LABS MEETING SPACES_RIGHT",
                eventSpacesPath + "B) LABS MEETING SPACES_TOP_CHANGE",
                eventSpacesPath + "B) LAB MEETING SPACES"
            }),
            new Room(25, LanguageManager.Instance.GetText("33"), new string[]{
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
            new Room(26, LanguageManager.Instance.GetText("34"), new string[]{
                eventSpacesPath + "D) TOWER MEETING SPACES_FRONT",
                eventSpacesPath + "D) TOWER MEETING SPACES_RIGHT",
                eventSpacesPath + "D) TOWER MEETING ROOMS"
                }),
            new Room(27, LanguageManager.Instance.GetText("35"), new string[] { eventSpacesPath + "STUDIOS CAPACITY FR"}),
            new Room(28, LanguageManager.Instance.GetText("36"), new string[] { eventSpacesPath + "STUDIOS CAPACITY EN"})
        };

        string cateringPath = "Images/EspaceView/Catering/";

        Room[] catering =
        {
            new Room(29, LanguageManager.Instance.GetText("38"), cateringPath + "Spring Summer/"),
            new Room(30, LanguageManager.Instance.GetText("39"), cateringPath + "Fall Winter/"),
            new Room(31, LanguageManager.Instance.GetText("40"), cateringPath + "Printemps Eté/"),
            new Room(32, LanguageManager.Instance.GetText("41"), cateringPath + "Automne Hiver/")
        };

        RoomCategory[] roomCategories =
        {
            new RoomCategory(LanguageManager.Instance.GetText("4"), underOneRoof),
            new RoomCategory(LanguageManager.Instance.GetText("18"), roomsAndSuites),
            new RoomCategory(LanguageManager.Instance.GetText("30"), eventSpaces),
            new RoomCategory(LanguageManager.Instance.GetText("37"), catering)
        };

        _nestedListUI.InitList(roomCategories);
    }
}