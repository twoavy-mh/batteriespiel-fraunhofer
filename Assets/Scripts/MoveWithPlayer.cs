using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithPlayer : MonoBehaviour
{
    
    public Transform Player;
    
    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(Player.localPosition.x, Player.localPosition.y, 10);
    }
}
