using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameViewController : MonoBehaviour, ISaveable
{
    [SerializeField] private Canvas m_ScoreCanvas;

    public static GameViewController Instance; 
    
    private int _currentScore;
    
    private static void SaveJsonData(GameViewController a_GameViewController)
    {
        SaveData saveData = new SaveData();
        a_GameViewController.PopulateSaveData(saveData);
        
        if (FileManager.WriteToFile("SaveData.dat", saveData.ToJson()))
        {
            Debug.Log("SaveData.dat saved");
        }
        else
        {
            Debug.Log("SaveData.dat not saved");
        }
    }

    private void Awake()
    {
        _currentScore = 0;
    }

    private void Start()
    {
        // TODO: IMPLEMENT SCORE CANVAS 
        // m_ScoreCanvas.SetScore(_currentScore);
        
        LoadJsonData(this);
        Debug.Log("SCORE" + _currentScore);
    }

    public void PopulateSaveData(SaveData a_SaveData)
    {
        a_SaveData.m_GlobalScore = _currentScore;
    }
    
    private static void LoadJsonData(GameViewController a_GameViewController)
    {
        if (FileManager.LoadFromFile("SaveData.dat", out var json))
        {
            SaveData saveData = new SaveData();
            saveData.LoadFromJson(json);
            
            a_GameViewController.LoadFromSaveData(saveData);
            Debug.Log("SaveData.dat loaded");
        }
        else
        {
            Debug.Log("SaveData.dat not loaded");
        }
    }
    
    public void LoadFromSaveData(SaveData a_SaveData)
    {
        _currentScore = a_SaveData.m_GlobalScore;
    }

}
