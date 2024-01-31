using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;
    
    private GameObject _animateStartOpenGameObject;
    private GameObject _animateStartCloseGameObject;
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.enabled = true;
        
        FindHiddenOpenCloseContainer();
        
        _animateStartOpenGameObject = GameObject.Find("OpenButton"); 
        _animateStartCloseGameObject = GameObject.Find("CloseButton"); 
        _animateStartOpenGameObject.GetComponent<Button>().onClick.AddListener(triggerAnimation);
        _animateStartCloseGameObject.GetComponent<Button>().onClick.AddListener(triggerAnimation);
    }

    // Update is called once per frame
    private void triggerAnimation()
    {
        AnimatorStateInfo animatorState = _animator.GetCurrentAnimatorStateInfo(0);
        if (animatorState.IsName("opening") || animatorState.IsName("closing"))
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

    private void FindHiddenOpenCloseContainer()
    {
        foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            Debug.Log(go.name);
            if (go.name == "OpenCloseAnimation")
                go.SetActive(true);
        }
    }
}
