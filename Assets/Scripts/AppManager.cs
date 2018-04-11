using UnityEngine;
using UnityEngine.UI;

public class AppManager : MonoBehaviourSingleton<AppManager>
{
    public enum Screen { HotelView, EspaceView, MapView };

    private InterestPoint _currentInterestPoint;

    [Header("Menu")]
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
    public Text _mapViewBusTime;
    public GameObject _mapViewWalkIcon;

    public void ChangeScreen(int screenNumber)
    {
        ChangeScreen((Screen)screenNumber);
    }

    public void Start()
    {
        InputManager.SingleClickTriggered += OpenPopUpIfOnMouse;
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
                _hotelSprite.ChangeSprite(0);
                _espaceSprite.ChangeSprite(1);
                _mapSprite.ChangeSprite(1);
                break;

            case Screen.EspaceView:
                _hotelView.SetActive(false);
                _espaceView.SetActive(true);
                _mapView.SetActive(false);
                _hotelModel.SetActive(false);
                _hotelSprite.ChangeSprite(1);
                _espaceSprite.ChangeSprite(0);
                _mapSprite.ChangeSprite(1);
                break;

            case Screen.MapView:
                _hotelView.SetActive(false);
                _espaceView.SetActive(false);
                _mapView.SetActive(true);
                _hotelModel.SetActive(false);
                _hotelSprite.ChangeSprite(1);
                _espaceSprite.ChangeSprite(1);
                _mapSprite.ChangeSprite(0);
                break;

            default:
                break;
        }
    }

    private void OpenPopUpIfOnMouse()
    {
        RaycastHit raycastHit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit, Mathf.Infinity, LayerMask.GetMask("InterestPoints")))
        {
            OpenHotelViewPopUp(raycastHit.collider.GetComponent<InterestPoint>());
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
                path = "Images/HotelView/Other/";
                break;
        }
        Debug.Log(path + interestPoint._imagePath);
        if (!string.IsNullOrEmpty(interestPoint._imagePath))
        {
            _hotelViewImage.sprite = Resources.Load<Sprite>(path + interestPoint._imagePath); 
        }

        // Enable the button only if it is linked to an element in the galery/espace view
        _hotelViewImage.GetComponent<Button>().interactable = _nestedListUI._subElementsDictionnary.ContainsKey(_currentInterestPoint._roomId);

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
        _mapViewBusTime.text = mapInterestPoint._busTime;
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
        // Initialize the galeries displayed in the Espace View

        // Some image paths are commented so they're not displayed. They were not deleted because the client might want them back (images are still stored in the resources folder).

        string underOneRoofPath = "Images/EspaceView/Under One Roof/";


        Room[] underOneRoof =
        {
            new Room(0, LanguageManager.Instance.GetText("5"), new string[]{underOneRoofPath + "A) LOBBY SPACE"}),
            new Room(1, LanguageManager.Instance.GetText("6"), new string[]{
                underOneRoofPath + "C) MAYO RESTAURANT",
                underOneRoofPath + "Mayo 1",
                underOneRoofPath + "Mayo 2",
                underOneRoofPath + "Mayo 3",
                underOneRoofPath + "Mayo 4",
            }),
            new Room(3, LanguageManager.Instance.GetText("8"), new string[]{
                underOneRoofPath + "D) MAYO",
                underOneRoofPath + "D) MARKET",
            }),
            new Room(4, LanguageManager.Instance.GetText("9"), new string[]{
                underOneRoofPath + "E) PANORAMIC BAR",
                underOneRoofPath + "E) New Bar",
            }),
            new Room(5, LanguageManager.Instance.GetText("10"), new string[]{underOneRoofPath + "F) REGENCY CLUB"}),
            new Room(6, LanguageManager.Instance.GetText("12"), new string[]{
                underOneRoofPath + "G) FITNESS CENTER",
            }),
            new Room(7, LanguageManager.Instance.GetText("13"), new string[]{
                underOneRoofPath + "H) METRO ACCESS 1",
                underOneRoofPath + "H) METRO ACCESS 2"
                }),
            new Room(8, LanguageManager.Instance.GetText("14"), new string[]{
                underOneRoofPath + "I) PARKING ACCESS_TOP"
            }),
            new Room(9, LanguageManager.Instance.GetText("15"), new string[]{
                underOneRoofPath + "J) AIRPORT SHUTTLE"
            }),
            new Room(10, LanguageManager.Instance.GetText("16"), new string[]{
                underOneRoofPath + "K) CONGRESS CENTER BOUTIQUES",
            }),
            new Room(11, LanguageManager.Instance.GetText("17"), new string[]{
                underOneRoofPath + "L) MOVIE THEATER",
                underOneRoofPath + "L) MOVIE THEATER_TOP_CHANGE"
            })
        };

        string roomsAndSuitesPath = "Images/EspaceView/Room And Suites/";

        Room[] roomsAndSuites =
        {
            new Room(12, LanguageManager.Instance.GetText("19"), new string[]{
                roomsAndSuitesPath + "A) STANDARD_",
                roomsAndSuitesPath + "A) STANDAR_FRONT",
                roomsAndSuitesPath + "A) STANDARD"
                }),
            new Room(13, LanguageManager.Instance.GetText("20"), new string[]{
                roomsAndSuitesPath + "B) STANDARD EIFFEL_TOWER",
                roomsAndSuitesPath + "B) STANDARD EIFFEL TOWER"
                }),
            new Room(14, LanguageManager.Instance.GetText("21"), new string[]{
                roomsAndSuitesPath + "C) DELUXE_",
                roomsAndSuitesPath + "C) DELUXE",
                roomsAndSuitesPath + "C) DELUXE_FRONT"
                }),
            new Room(15, LanguageManager.Instance.GetText("22"), new string[]{
                roomsAndSuitesPath + "D) DELUXE EIFFEL TOWER_",
                roomsAndSuitesPath + "D) DELUXE EIFFEL TOWER"
                }),
            new Room(16, LanguageManager.Instance.GetText("23"), new string[]{
                roomsAndSuitesPath + "E) CLUB_",
                roomsAndSuitesPath + "E) CLUB",
                roomsAndSuitesPath + "E) CLUB FRONT"
                }),
            new Room(17, LanguageManager.Instance.GetText("24"), new string[]{
                roomsAndSuitesPath + "F) CLUB EIFFEL TOWER",
                roomsAndSuitesPath + "F) CLUB EIFFEL TOWER_"
                }),
            new Room(18, LanguageManager.Instance.GetText("25"), new string[]{
                roomsAndSuitesPath + "G) REGENCY SUITE_KING",
                roomsAndSuitesPath + "G) REGENCY SUITE KING"
                }),
            new Room(19, LanguageManager.Instance.GetText("26"), new string[]{
                roomsAndSuitesPath + "H) REGENCY SUITE KING EIFFEL TOWER",
                roomsAndSuitesPath + "H) REGENCY SUITE KING EIFFEL TOWER_"
                }),
            new Room(21, LanguageManager.Instance.GetText("28"), new string[]{
                roomsAndSuitesPath + "J) LOFT_SUITE",
                roomsAndSuitesPath + "J) LOFT_SUITE_"
                }),
            new Room(20, LanguageManager.Instance.GetText("27"), new string[]{
                roomsAndSuitesPath + "I) REGENCY EXECUTIVE",
                roomsAndSuitesPath + "I) REGENCY EXECUTIVE SUITE",
                roomsAndSuitesPath + "I) REGENCY EXECUTIVE SUITE_FRONT"
                }),
            new Room(22, LanguageManager.Instance.GetText("29"), new string[]{
                roomsAndSuitesPath + "K) PRESIDENTIAL_SUITE",
                roomsAndSuitesPath + "K) PRESIDENTIAL SUITE"
                })
        };

        string eventSpacesPath = "Images/EspaceView/Event Spaces/";

        Room[] eventSpaces =
        {
            new Room(24, LanguageManager.Instance.GetText("32"), new string[]{
                eventSpacesPath + "B) Plan LAB",
                eventSpacesPath + "B) LABS MEETING SPACES_TOP_CHANGE",
                eventSpacesPath + "B) Capacites Tower"
            }),
            new Room(25, LanguageManager.Instance.GetText("33"), new string[]{
                eventSpacesPath + "C) STUDIO MEETING SPACE_BRIDGE",
                eventSpacesPath + "C) STUDIO MEETING SPACES_Prefunction-Rendering",
                eventSpacesPath + "C) STUDIO MEETING SPACES_Ballroom-3D-Rendering",
                eventSpacesPath + "C) STUDIO MEETING SPACES_CONGRESS CORRIDOR",
                eventSpacesPath + "C) STUDIO MEETING SPACES_CONGRESS ENTRY",
                eventSpacesPath + "C) STUDIO MEETING SPACES_meeting room_1",
                eventSpacesPath + "C) STUDIO MEETING SPACES_TOP_CHANGE",
                eventSpacesPath + "C) Plans REGENCY",
                eventSpacesPath + "C) Capacités Regency ",
                }),
            new Room(26, LanguageManager.Instance.GetText("34"), new string[]{
                eventSpacesPath + "D) Plan TOWER",
                eventSpacesPath + "D) Capacités TOWER",
                }),
            new Room(23, LanguageManager.Instance.GetText("31"), new string[] {
                eventSpacesPath + "A12) AMPHITHEATRE  bordeaux",
                eventSpacesPath + "A) CONGRESS CENTER MEETING SPACES_0",
                eventSpacesPath + "A) CONGRESS CENTER MEETING SPACES_1",
                eventSpacesPath + "A) CONGRESS CENTER MEETING SPACES_2",
                eventSpacesPath + "A) CONGRESS CENTER MEETING SPACES_3"
                }),
        };

        string cateringPath = "Images/EspaceView/Catering/";

        Room[] catering =
        {
            new Room(30, LanguageManager.Instance.GetText("39"), cateringPath + "Fall Winter/"),
            new Room(32, LanguageManager.Instance.GetText("41"), cateringPath + "Automne Hiver/"),
            new Room(29, LanguageManager.Instance.GetText("38"), cateringPath + "Spring Summer/"),
            new Room(31, LanguageManager.Instance.GetText("40"), cateringPath + "Printemps Ete/"),
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