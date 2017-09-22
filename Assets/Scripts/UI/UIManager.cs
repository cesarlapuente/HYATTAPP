using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviourSingleton<UIManager>
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

    [Header("Other")]
    public GameObject _carouselContainer;

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
}