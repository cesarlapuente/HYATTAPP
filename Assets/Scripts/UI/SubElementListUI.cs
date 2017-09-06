using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubElementListUI : MonoBehaviour {

    private bool _isDisplayed = false;
    private RectTransform _rectTransform;
    public float _defaultHeight;

    public RectTransform _contentRectTransform;

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
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

    /// <summary>
    /// Change the height of the rectTransform over time
    /// </summary>
    /// <param name="newHeight"></param>
    /// <returns></returns>
    private IEnumerator ChangeHeightCoroutine(float newHeight)
    {
        float currentHeight = _rectTransform.sizeDelta.y;
        float heightDifferential = currentHeight - newHeight;
        float step = 500.0f / Mathf.Abs(heightDifferential) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f)
        {
            t += step;
            _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, Mathf.Lerp(_rectTransform.sizeDelta.y, newHeight, t));
            LayoutRebuilder.ForceRebuildLayoutImmediate(_contentRectTransform);
            yield return new WaitForFixedUpdate();
        }

        _contentRectTransform.sizeDelta = new Vector2(_contentRectTransform.sizeDelta.x, _contentRectTransform.sizeDelta.y - heightDifferential);
    }
}
