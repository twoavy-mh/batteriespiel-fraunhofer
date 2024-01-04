using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RandomObstacleImage : MonoBehaviour
{
    
    public Sprite[] ObstacleImages;
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = Helpers.Utility.GetRandom(ObstacleImages);
    }
}
