using System.Collections.Generic;
using UnityEngine;

public class RandomObstacleImage : MonoBehaviour
{
    
    public Sprite[] ObstacleImages;
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = Helpers.Utility.GetRandom(ObstacleImages);
        List<Vector2> points = new List<Vector2>();
        GetComponent<SpriteRenderer>().sprite.GetPhysicsShape(0, points);
        GetComponent<PolygonCollider2D>().SetPath(0, points.ToArray());
    }
}
