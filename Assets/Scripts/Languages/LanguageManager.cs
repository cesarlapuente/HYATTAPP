using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageManager : MonoBehaviourSingleton<LanguageManager>
{
    private Dictionary<string, string> _texts = new Dictionary<string, string>();
    private string _keySeparator = "=";
    public List<TextTranslated> _registeredTextTranslated = new List<TextTranslated>();

    public GameObject _languagesGo;
    public Text _loadingText;
    public GameObject _languageSelectionPanel;

    public void LoadLanguage(string lang)
    {
        _texts.Clear();
        TextAsset textAsset = Resources.Load<TextAsset>("Languages/" + lang);
        string allTexts = textAsset.text;
        string[] lines = allTexts.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
        string key, value;
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].IndexOf(_keySeparator) >= 0 && !lines[i].StartsWith("#"))
            {
                key = lines[i].Substring(0, lines[i].IndexOf(_keySeparator));
                value = lines[i].Substring(lines[i].IndexOf(_keySeparator) + 1, lines[i].Length - lines[i].IndexOf(_keySeparator) - 1).Replace("\\n", Environment.NewLine);
                _texts.Add(key, value);
            }
        }

        RefreshAllTexts();
    }

    public void LoadLanguageAndStartApp(string lang)
    {
        _languagesGo.SetActive(false);
        switch (lang)
        {
            case "fr":
                _loadingText.text = "Chargement en cours...";
                break;

            case "en":
                _loadingText.text = "Loading...";
                break;
        }
    }

    public string GetText(string key)
    {
        string text;
        if (!_texts.TryGetValue(key, out text))
        {
            Debug.LogError("The key '" + key + "' does not exist");
        }
        return text;
    }

    public void RefreshAllTexts()
    {
        for (int i = 0; i < _registeredTextTranslated.Count; i++)
        {
            _registeredTextTranslated[i].Refresh();
        }
    }

    public void RegisterTextTranslated(TextTranslated text)
    {
        _registeredTextTranslated.Add(text);
    }
}