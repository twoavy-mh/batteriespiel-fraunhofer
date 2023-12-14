using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTilePlacer : MonoBehaviour
{

    public GameObject FloorTile;
    public GameObject ObstacleTile;
    public int Count;
    
    void Start()
    {
        for (int i = 0; i < Count; i++)
        {
            if (i % 10 != 0)
            {
                GameObject tile = Instantiate(FloorTile, new Vector3(i * 3.42f, -4.19f, 0), Quaternion.identity);
                tile.transform.SetParent(transform);
                tile.transform.GetChild(0).tag = "Floor";
                tile.layer = 6;
                tile.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = -i;

                if (i % 6 == 0)
                {
                    GameObject obstacle = Instantiate(ObstacleTile, new Vector3(i * 3.42f, -3.5f, 0), Quaternion.identity);
                    obstacle.transform.SetParent(tile.transform);
                    obstacle.transform.GetChild(0).tag = "Obstacle";
                }
            }
        }   
    }
}
