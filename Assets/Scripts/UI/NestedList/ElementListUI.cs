using UnityEngine;

/// <summary>
/// An element of the NestedListUI
/// Clicking on an element will displayed a list of sub elements
/// </summary>
public class ElementListUI : MonoBehaviour
{
    /// <summary>
    /// The SubElement that will be opened when clicking on the ElementListUI Button
    /// </summary>
    public SubElementContainerListUI _subElement;
    public NestedListUI _parentNestedList;

    /// <summary>
    /// Display or hide the linked subElement
    /// </summary>
    public void ToggleSubElement()
    {
        DisplaySubElement(!_subElement._isDisplayed);
    }

    /// <summary>
    /// Will display the SubElement if true, will hide it otherwise
    /// </summary>
    /// <param name="display"></param>
    public void DisplaySubElement(bool display)
    {
        if (_subElement._isDisplayed != display)
        {
            int spriteId = display ? 1 : 0;

            GetComponentInChildren<SpriteChanger>().ChangeSprite((uint)spriteId);

            if (display)
            {
                _parentNestedList.HideAllElements();
            }

            _subElement.DisplayList(display);
        }
    }
}