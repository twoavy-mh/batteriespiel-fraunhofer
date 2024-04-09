using System.Collections;
using System.Collections.Generic;
using Crystal;
using Helpers;
using UnityEngine;
using UnityEngine.UI;

public class BracketLength : MonoBehaviour
{
    public RectTransform upper;
    public RectTransform lower;
    public RectTransform center;

    private RectTransform _self;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        _self = GetComponent<RectTransform>();
        if (Utility.GetDevice() == Device.Desktop || Utility.GetDevice() == Device.Mobile)
        {
            float spriteWidth = GetComponent<Image>().sprite.rect.width;
            _self.localPosition = new Vector3(_self.localPosition.x, center.localPosition.y +
                (center.sizeDelta.y / 2f), _self.position.z);
            _self.sizeDelta = new Vector2(spriteWidth,
                (upper.localPosition.y - lower.localPosition.y + lower.sizeDelta.y));
        }
    }
}