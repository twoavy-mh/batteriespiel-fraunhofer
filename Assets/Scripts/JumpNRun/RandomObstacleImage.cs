using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObstacleImage : MonoBehaviour
{
    
    public Sprite[] ObstacleImages;
    
    // Start is called before the first frame update
    void Start()
    {
        System.Random random = new System.Random();
        int r = random.Next(0, ObstacleImages.Length);
        GetComponent<SpriteRenderer>().sprite = ObstacleImages[r];
    }
}
