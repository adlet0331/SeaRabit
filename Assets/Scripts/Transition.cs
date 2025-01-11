using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    [SerializeField] private Image transitionImage;
    [SerializeField] private Animator transitionAnimator;
    
    private readonly int _fadeOutHash = Animator.StringToHash("FadeOut");
    private readonly int _fadeClearHash = Animator.StringToHash("FadeClear");

    public void FadeOut()
    {
        transitionAnimator.SetTrigger(_fadeOutHash);
    }

    public void FadeClear()
    {
        transitionAnimator.SetTrigger(_fadeClearHash);
    }
}
