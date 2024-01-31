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
                InstantiateModel(prismCell, new Vector3(0,0,2));
                InstantiateModel(cylinderCell, new Vector3(0,0,-2));
                break;
            case GameState.Models.Pouch:
                GameObject cell = InstantiateModel(pouchCell);
                cell.AddComponent<AnimationController>();
                break;
            case GameState.Models.Car:
                InstantiateModel(car);
                break;
        }
    }

    GameObject InstantiateModel(GameObject prefab, Vector3? localPosition = null)
    {
        GameObject model = Instantiate(prefab);
        model.transform.SetParent(transform);
        model.transform.localPosition =localPosition == null? Vector3.zero:(Vector3) localPosition;
        
        // model.transform.localScale = Vector3.one;

        return model;
    }
}
