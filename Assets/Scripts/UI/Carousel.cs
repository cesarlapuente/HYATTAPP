using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Carousel that can load and display multiple images
/// </summary>
public class Carousel : MonoBehaviour
{
    public delegate void CarouselMoved();

    public static event CarouselMoved OnCarouselMoved;

    private List<Image> _dots = new List<Image>();
    private List<Image> _images = new List<Image>();
    //private List<ImageContainer> _imagesContainer = new List<ImageContainer>();
    private string[] _imagePath;
    private List<float> _distancesToCenter = new List<float>();
    private State _currentState;
    private Vector3 _contentInitialLocalPosition;
    private Vector3 _startDragContentLocalPosition;

    /// <summary>
    /// Static : the default state
    /// Dragged : when the user is dragging the carousel
    /// Moving : when the carousel is moving on its own, trying to snap to an image
    /// </summary>
    public enum State { Static, Dragged, Moving }

    public RectTransform _contentRectTransform;
    public Image _firstImage;
    public Image _firstDot;

    //public int _numberOfImages;
    public float _minDragDistance;

    public GameObject _initialDotsPosition;
    public Text _imageTitle;
    public Text _imageCopyright;
    public GameObject _previousButton;
    public GameObject _nextButton;
    public GameObject _dotsContainer;
    public int _dotOffset = 10;
    public int _currentImageIndex = 0;

    public Color _dotColor;

    private void Start()
    {
        _contentInitialLocalPosition = _contentRectTransform.localPosition;
    }

    /// <summary>
    /// Initialize and load images from the list.
    /// Can be used to reset the Carousel with another list.
    /// </summary>
    /// <param name="images"></param>
    public void Init(string[] images)
    {
        // Remove previous images and dots
        for (int i = 1; i < _images.Count; i++)
        {
            Destroy(_images[i].gameObject);
            Destroy(_dots[i].gameObject);
        }
        _images.Clear();
        _dots.Clear();
        _distancesToCenter.Clear();

        _imagePath = images;

        _currentImageIndex = 0;
        _contentRectTransform.localPosition = _contentInitialLocalPosition;

        _images.Add(_firstImage);
        _distancesToCenter.Add(0);
        _firstImage.sprite = Resources.Load<Sprite>(images[0]);
        //_imageTitle.text = images[0]._name;
        //_imageCopyright.text = images[0]._copyright;

        _dots.Add(_firstDot);

        _firstDot.transform.parent.localPosition = _initialDotsPosition.transform.localPosition;

        for (int i = 1; i < images.Length; i++)
        {
            // Instantiates images
            Image image = Instantiate<Image>(_firstImage);
            image.name = images[i];
            image.sprite = Resources.Load<Sprite>(images[i]);
            image.transform.SetParent(_contentRectTransform.transform);
            image.transform.localPosition = new Vector2((_firstImage.rectTransform.rect.width + 100) * i, _firstImage.rectTransform.localPosition.y);
            _images.Add(image);

            float distanceToCenter = image.transform.localPosition.x - GetComponent<RectTransform>().anchoredPosition.x;
            _distancesToCenter.Add(distanceToCenter);

            // Instantiate dots
            Image dot = Instantiate<Image>(_firstDot);
            dot.transform.SetParent(_firstDot.transform.parent);
            dot.transform.localPosition = new Vector2((_firstDot.rectTransform.rect.width + _dotOffset) * i, _firstDot.rectTransform.localPosition.y);
            _dots.Add(dot);
        }

        // Enable scrolling and buttons when there is more than one image
        bool moreThanOneImage = images.Length > 1;
        GetComponent<ScrollRect>().horizontal = moreThanOneImage;
        _previousButton.SetActive(moreThanOneImage);
        _nextButton.SetActive(moreThanOneImage);

        // Set color of first dot to orange
        _firstDot.CrossFadeColor(_dotColor, 0.0f, false, false);

        // Move Dots Container so it stays justified on the right
        _firstDot.transform.parent.localPosition -= new Vector3((_firstDot.rectTransform.rect.width + _dotOffset) * (_dots.Count - 1), 0);

        //Desactivate all dots if there is only one image
        if (!moreThanOneImage) _dotsContainer.SetActive(false);
        else _dotsContainer.SetActive(true);
    }

    private void Update()
    {
        switch (_currentState)
        {
            case State.Static:
                break;

            case State.Dragged:
                break;

            case State.Moving:
                Vector3 targetPosition = _contentInitialLocalPosition - new Vector3(_distancesToCenter[_currentImageIndex], 0);
                _contentRectTransform.localPosition = Vector3.MoveTowards(_contentRectTransform.localPosition, targetPosition, Time.deltaTime * 2000);
                if (_contentRectTransform.localPosition.x - targetPosition.x == 0)
                {
                    _currentState = State.Static;
                }
                break;
        }
    }

    /// <summary>
    /// Called when the user starts dragging the carousel
    /// </summary>
    public void BeginDrag()
    {
        _startDragContentLocalPosition = _contentRectTransform.localPosition;
        _currentState = State.Dragged;
    }

    /// <summary>
    /// Called when the user stops dragging the carousel
    /// </summary>
    public void EndDrag()
    {
        if (Mathf.Abs(_startDragContentLocalPosition.x - _contentRectTransform.localPosition.x) >= _minDragDistance)
        {
            if (_startDragContentLocalPosition.x < _contentRectTransform.localPosition.x)
            {
                SetCurrentImageIndex(_currentImageIndex - 1);
            }
            else
            {
                SetCurrentImageIndex(_currentImageIndex + 1);
            }
        }
        _currentState = State.Moving;

        if (OnCarouselMoved != null) OnCarouselMoved();
    }

    /// <summary>
    /// Show next image.
    /// </summary>
    public void NextImage()
    {
        SetCurrentImageIndex(_currentImageIndex + 1);
        _currentState = State.Moving;
        if (OnCarouselMoved != null) OnCarouselMoved();
    }

    /// <summary>
    /// Show previous image
    /// </summary>
    public void PreviousImage()
    {
        SetCurrentImageIndex(_currentImageIndex - 1);
        _currentState = State.Moving;
        if (OnCarouselMoved != null) OnCarouselMoved();
    }

    /// <summary>
    /// Safely set the currentImageIndex
    /// </summary>
    /// <param name="newIndex"></param>
    public void SetCurrentImageIndex(int newIndex)
    {
        _dots[_currentImageIndex].CrossFadeColor(new Color(1.0f, 1.0f, 1.0f), 0.5f, false, false);
        _currentImageIndex = Mathf.Clamp(newIndex, 0, _images.Count - 1);
        _dots[_currentImageIndex].CrossFadeColor(_dotColor, 0.5f, false, false);
        //_imageTitle.text = _imagesContainer[_currentImageIndex]._name;
        //_imageCopyright.text = _imagesContainer[_currentImageIndex]._copyright;
    }
}