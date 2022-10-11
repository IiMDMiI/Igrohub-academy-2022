using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinLoseUI : MonoBehaviour
{
    private const string ShowAnimaton = "Down";
    private const string HideAnimaton = "Up";
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _win;
    [SerializeField] private GameObject _lose;

    public void Show(bool win)
    {
        _win.SetActive(win);
        _lose.SetActive(!win);
        
        _animator.Play(ShowAnimaton);

    }

    public void Hide() =>
        _animator.Play(HideAnimaton);



}
