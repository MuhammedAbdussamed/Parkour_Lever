using UnityEngine;

public class Doors_Animations : MonoBehaviour
{
    private Animator doorAnimator;
    private Level_Door? doorScript;

    void Awake()
    {
        doorScript = GetComponent<Level_Door>();
        doorAnimator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        doorScript.OnAnimationEvent += SetAnimation;
    }

    void OnDisable()
    {
        doorScript.OnAnimationEvent -= SetAnimation;
    }

    void SetAnimation()
    {
        doorAnimator.SetBool("Active", doorScript.isActive);

    }
}
