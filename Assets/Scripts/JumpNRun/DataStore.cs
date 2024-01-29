using System;
using System.Collections.Generic;
using Events;
using Models;
using JumpNRun;
using UnityEngine;
using UnityEngine.UI;

public class DataStore : CollectedEvent.IUseCollectable
{
    private static DataStore _dataStore;
    private static readonly object ThreadLock = new object();
    
    public Dictionary<Collectable, CollectableState> collectablesScore = new Dictionary<Collectable, CollectableState>();
    public int score = 100;
    
    public static DataStore Instance
    {
        get
        {
            lock (ThreadLock)
            {
                if (_dataStore == null)
                {
                    _dataStore = new DataStore();
                }

                return _dataStore;
            }
        }
    }

    private DataStore()
    {
        SceneController.Instance.collectEvent.AddListener(UseCollectable);
    }

    public void Init()
    {
        collectablesScore.Add(Collectable.Lithium, new CollectableState(Collectable.Lithium, 50, 10));
        collectablesScore.Add(Collectable.BlueLightning, new CollectableState(Collectable.BlueLightning, 100, 10));
        collectablesScore.Add(Collectable.YellowLightning, new CollectableState(Collectable.YellowLightning, 100, 10));
    }

    public void UseCollectable(Collectable c)
    {
        try
        {
            collectablesScore[c].Add(1);
        }
        catch (KeyNotFoundException)
        {
            collectablesScore.Add(c, new CollectableState(c, 0, 0));
            collectablesScore[c].Add(1);
        }
    }
}