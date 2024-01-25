using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PouchcellController : MonoBehaviour
{
    private Animator _animator;
    
    private GameObject _animateButtonGameObject;
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.enabled = true;
        
        _animateButtonGameObject = GameObject.Find("Open Cell"); 
        _animateButtonGameObject.GetComponent<Button>().onClick.AddListener(triggerAnimation);
    }

    // Update is called once per frame
    private void triggerAnimation()
    {
        AnimatorStateInfo animatorState = _animator.GetCurrentAnimatorStateInfo(0);
        Debug.Log(animatorState.fullPathHash);
        if (animatorState.IsName("isOpenPouchcell") || animatorState.IsName("OpenPouchcell"))
        {
            _animator.ResetTrigger("open");
            _animator.SetTrigger("close");
        }
        else
        {
            _animator.ResetTrigger("close");
            _animator.SetTrigger("open");
        }
    }
}
