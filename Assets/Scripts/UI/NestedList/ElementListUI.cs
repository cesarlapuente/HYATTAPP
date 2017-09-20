﻿using UnityEngine;

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

    /// <summary>
    /// Display or hide the linked subElement
    /// </summary>
    public void ToggleSubElement()
    {
        uint spriteId = 0;
        if (!_subElement._isDisplayed)
        {
            spriteId = 1;
        }
        GetComponentInChildren<SpriteChanger>().ChangeSprite(spriteId);

        _subElement.ToggleList();
    }
}