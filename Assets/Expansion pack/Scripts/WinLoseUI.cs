using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinLoseUI : MonoBehaviour
{
    private const string ShowAnimaton = "Down";
    private const string HideAnimaton = "Hide";
    [SerializeField] private Animator _animator;
    [SerializeField] private Image _win;
    [SerializeField] private Image _lose;

    public void Show(bool win)
    {
        _win.enabled = win;
        _lose.enabled = !win;
        
        _animator.Play(ShowAnimaton);

    }

    public void Hide() =>
        _animator.Play(HideAnimaton);



}
