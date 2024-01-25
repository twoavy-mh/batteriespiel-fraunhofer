using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;

public class Init3dModel : MonoBehaviour
{
    public GameObject pouchCell;
    public GameObject prismCell;
    public GameObject cylinderCell;
    public GameObject car;
    // Start is called before the first frame update
    void Start()
    {
        InitModel();
    }

    // Update is called once per frame
    void InitModel()
    {
        switch (GameState.Instance.current3dModel)
        {
            case GameState.Models.Cells:
                InstantiateModel(pouchCell);
                InstantiateModel(prismCell);
                InstantiateModel(cylinderCell);
                break;
            case GameState.Models.Pouch:
                InstantiateModel(pouchCell);
                break;
            case GameState.Models.Car:
                InstantiateModel(car);
                break;
        }
    }

    void InstantiateModel(GameObject prefab)
    {
        GameObject model = Instantiate(prefab);
        model.transform.SetParent(transform);
        model.transform.localPosition = Vector3.zero;
        model.transform.localScale = Vector3.one;
    }
}
