using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;
using UnityEngine.UIElements;

public class RandomTileSelector : MonoBehaviour
{
    public GameObject YellowLightning;
    public GameObject BlueLightning;
    
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


        if (Utility.RandomInRange(0, 100) / 100f < 0.125f)
        {
            bool isYellow = Random.value > 0.5f;
            GameObject lighting =
                Instantiate(isYellow ? YellowLightning : BlueLightning,
                    new Vector3(transform.position.x, Utility.RandomInRange(-2, 1), transform.position.z), Quaternion.identity);
            lighting.transform.SetParent(transform);
            lighting.name = $"lightning-{isYellow}";
            SpriteRenderer r = lighting.GetComponent<SpriteRenderer>();
            //r.sortingOrder = Sortin
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
