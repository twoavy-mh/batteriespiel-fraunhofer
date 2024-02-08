using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomObstacleImage : MonoBehaviour
{

    private Sprite[] _levelSprites;

    private void Awake()
    {
        _levelSprites = Resources.LoadAll<Sprite>($"Images/JnRLevel/obstacle/level1");
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = Helpers.Utility.GetRandom<Sprite>(_levelSprites);
        if (GetComponent<SpriteRenderer>().sprite.GetPhysicsShapeCount() == 2)
        {
            List<Vector2> points = new List<Vector2>();
            GetComponent<SpriteRenderer>().sprite.GetPhysicsShape(0, points);
            transform.tag = "Obstacle";
            GetComponent<PolygonCollider2D>().SetPath(0, points.ToArray());
            
            
            GameObject second = new GameObject();
            second.transform.SetParent(transform.parent.parent);
            second.transform.position = transform.position;
            second.AddComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
            PolygonCollider2D secondPoly = second.AddComponent<PolygonCollider2D>();
            List<Vector2> secondPoints = new List<Vector2>();
            GetComponent<SpriteRenderer>().sprite.GetPhysicsShape(1, secondPoints);
            secondPoly.SetPath(0, secondPoints.ToArray());
            second.tag = "Floor";
        }
        else
        {
            List<Vector2> points = new List<Vector2>();
            GetComponent<SpriteRenderer>().sprite.GetPhysicsShape(0, points);
            transform.tag = "Obstacle";
            GetComponent<PolygonCollider2D>().SetPath(0, points.ToArray());
        }
    }
}
