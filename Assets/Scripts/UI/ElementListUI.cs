using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementListUI : MonoBehaviour {

    public SubElementListUI _subElement;

    /// <summary>
    /// Display or hide the linked subElement
    /// </summary>
    public void ToggleSubElement()
    {
        _subElement.ToggleList();
    }
}
