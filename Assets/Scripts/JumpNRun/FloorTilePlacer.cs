using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTilePlacer : MonoBehaviour
{

    public GameObject FloorTile;
    public GameObject ObstacleTile;
    public int Count;
    public int[] Distances;
    
    void Start()
    {
        float offset = CalculateOffset();
        Debug.Log(offset);
        for (int i = 0; i < Count; i++)
        {
            if (i % 10 != 0)
            {
                Vector3 position = new Vector3((i - 1) * offset, -4f, 0f);
                GameObject tile = Instantiate(FloorTile, position, Quaternion.identity);
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

    private float CalculateOffset()
    {
        GameObject tile = Instantiate(FloorTile, Vector3.one, transform.rotation);
        Vector3[] bounds = Helpers.Utility.SpriteLocalToWorld(tile.transform, tile.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite);
        float d = bounds[1].x - bounds[0].x - 2.69f;
        Debug.Log(d);
        Destroy(tile);
        return d;
    }
}
