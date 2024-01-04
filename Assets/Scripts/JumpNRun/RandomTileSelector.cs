using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

public class RandomTileSelector : MonoBehaviour
{
    public Sprite[] Options;
    void Start()
    {
        int sib = gameObject.transform.parent.GetSiblingIndex();
        Sprite lastSprite = null;
        if (sib != 0)
        {
             lastSprite = gameObject.transform.parent.parent.GetChild(sib - 1).GetChild(0).GetComponent<SpriteRenderer>().sprite;    
        }

        if (lastSprite != null)
        {
            Sprite maybeThis = Utility.GetRandom(Options);
            while (lastSprite.name == maybeThis.name)
            {
                maybeThis = Utility.GetRandom(Options);
            }
            GetComponent<SpriteRenderer>().sprite = maybeThis;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = Utility.GetRandom(Options);    
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
