using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviourSingleton<UIManager>
{
    public enum Screen { HotelView, EspaceView, MapView};

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
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit, Mathf.Infinity, LayerMask.GetMask("InterestPoints")))
            {
                OpenHotelViewPopUp(raycastHit.collider.GetComponent<InterestPoint>());
            }
        }
    }

    public void OpenHotelViewPopUp(InterestPoint interestPoint)
    {
        _hotelViewPopUp.SetActive(true);
        _hotelViewImage.sprite = Resources.Load<Sprite>("Images/" + interestPoint._imagePath);
        _hotelViewTitle.text = LanguageManager.Instance.GetText(interestPoint._titleKey);
        _hotelViewDescription.text = LanguageManager.Instance.GetText(interestPoint._descriptionKey);
    }
}
