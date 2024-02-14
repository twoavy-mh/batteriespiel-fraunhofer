using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;
using Models;
using Nobi.UiRoundedCorners;
using TMPro;
using UnityEngine.UI;


public class LanguageToggleController : MonoBehaviour
{
    private TMP_Text _languageText;
    private Language _currentLanguage;
    
    void Start()
    {
        _currentLanguage = Application.systemLanguage == SystemLanguage.German ? Language.De : Language.En;
        _languageText = GetComponentInChildren<TMP_Text>();
        _languageText.text = _currentLanguage == Language.De ? "EN" : "DE";
        GetComponent<Button>().onClick.AddListener(ToggleLanguage);
    }

    private void ToggleLanguage()
    {
        _currentLanguage = _currentLanguage == Language.De ? Language.En : Language.De;
        _languageText.text = _currentLanguage == Language.De ? "EN" : "DE";
        Debug.Log(_currentLanguage);
    }
    
    public Language GetCurrentLanguage()
    {
        return _currentLanguage;
    }
}