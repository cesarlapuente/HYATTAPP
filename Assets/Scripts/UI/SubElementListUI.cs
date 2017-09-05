using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubElementListUI : MonoBehaviour {

    private bool _isDisplayed = false;
    private RectTransform _rectTransform;
    private float _defaultHeight;

    public RectTransform _verticalLayoutGroup;

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _defaultHeight = _rectTransform.sizeDelta.y;
        Debug.Log(_defaultHeight);
    }

	// Use this for initialization
	void Start () {
    }

    // Update is called once per frame
    void Update () {
		if (Input.GetKeyDown(KeyCode.A))
        {
            ToggleList();
        }
	}

    /// <summary>
    /// Will display the list if it is hidden. Will hide it otherwise
    /// </summary>
    public void ToggleList()
    {
        float newHeight = 0;
        if (!_isDisplayed)
        {
            newHeight = _defaultHeight;
        }
        _isDisplayed = !_isDisplayed;

        StartCoroutine(ChangeHeightCoroutine(newHeight));
    }

    private IEnumerator ChangeHeightCoroutine(float newHeight)
    {
        float currentHeight = _rectTransform.sizeDelta.y;
        float step = 500.0f / Mathf.Abs(currentHeight - newHeight) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f)
        {
            t += step;
            _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, Mathf.Lerp(_rectTransform.sizeDelta.y, newHeight, t));
            LayoutRebuilder.ForceRebuildLayoutImmediate(_verticalLayoutGroup);
            yield return new WaitForFixedUpdate();
        }
    }
}
