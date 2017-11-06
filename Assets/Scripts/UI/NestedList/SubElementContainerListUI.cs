using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A SubElement container used for the NestedListUI
/// </summary>
public class SubElementContainerListUI : MonoBehaviour
{
    public bool _isDisplayed = false;
    private RectTransform _rectTransform;
    public float _defaultHeight;

    public RectTransform _contentRectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    /// <summary>
    /// Will display the list if it is hidden. Will hide it otherwise
    /// </summary>
    public void ToggleList()
    {
        DisplayList(!_isDisplayed);
    }

    public void DisplayList(bool display)
    {
        if (_isDisplayed != display)
        {
            _isDisplayed = display;
            float newHeight = 0;

            if (display)
            {
                newHeight = _defaultHeight;
            }
            StartCoroutine(ChangeHeightCoroutine(newHeight));
        }
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

        // Resize the parent when it's different from the content
        // Make sure the height of a SubElement is enough to display its elements
        if (transform.parent != _contentRectTransform.transform)
        {
            RectTransform parentRectTransform = transform.parent.GetComponent<RectTransform>();
            parentRectTransform.GetComponent<RectTransform>().sizeDelta = new Vector2(parentRectTransform.sizeDelta.x, parentRectTransform.sizeDelta.y - heightDifferential);
        }

        float i = 0.0f;
        float time = 0.5f;
        float rate = 1.0f / time;
        while (i < 1.0)
        {
            i += Time.deltaTime * rate;
            _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, Mathf.Lerp(_rectTransform.sizeDelta.y, newHeight, i));
            LayoutRebuilder.ForceRebuildLayoutImmediate(_contentRectTransform);
            yield return new WaitForFixedUpdate();
        }

        // Properly resize the content rectTransform
        _contentRectTransform.sizeDelta = new Vector2(_contentRectTransform.sizeDelta.x, _contentRectTransform.sizeDelta.y - heightDifferential);
    }
}