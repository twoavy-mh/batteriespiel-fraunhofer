using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ErrorBuilder : MonoBehaviour, ApiErrorEvent.IUseApiError
{
    private TMP_Text _errorStack;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.apiErrorEvent.AddListener(UseApiError);
        _errorStack = transform.GetChild(0).GetComponent<TMP_Text>();
        GetComponent<Button>().onClick.AddListener(Close);
    }

    private void Close()
    {
        GameManager.Instance.ClearError();
    }

    public void NewError(Exception e)
    {
        _errorStack.text += e.ToString() + "\n";
    }

    public void UseApiError(Exception e)
    {
        StartCoroutine(WaitASec(e));
    }
    
    public IEnumerator WaitASec(Exception e)
    {
        yield return new WaitForSeconds(1);
        NewError(e);
    }
}
