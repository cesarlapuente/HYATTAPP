using UnityEngine;
using UnityEngine.UI;

public class TextTranslated : MonoBehaviour
{
    public string _textKey;
    private Text _textComponent;

    private void Awake()
    {
        _textComponent = gameObject.GetComponent<Text>();
        if (LanguageManager.Instance != null)
        {
            LanguageManager.Instance.RegisterTextTranslated(this);
        }
        else
        {
            Debug.LogWarning("LanguageManager was not found.");
        }
    }

    public void Refresh()
    {
        if (LanguageManager.Instance != null)
        {
            _textComponent.text = LanguageManager.Instance.GetText(_textKey);
        }
        else
        {
            Debug.LogWarning("LanguageManager was not found.");
        }
    }
}