using UnityEngine;

public class AO_Animations : MonoBehaviour
{
    private Animator objectAnimator;
    private BaseActivatable objectScript;

    void Awake()
    {
        objectScript = GetComponent<BaseActivatable>();
        objectAnimator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        objectScript.OnAnimationEvent += SetAnimation;
    }

    void OnDisable()
    {
        objectScript.OnAnimationEvent -= SetAnimation;
    }

    void SetAnimation()
    {

        objectAnimator.SetBool("Active", objectScript.isActive);
        
    }
}
