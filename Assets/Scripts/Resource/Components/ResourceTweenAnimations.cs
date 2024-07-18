using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ResourceTweenAnimations
{
    private List<Tween> _tweens;

    public void AddTween(Tween tween)
    {
        if(_tweens == null)
        {
            _tweens = new List<Tween>();
        }

        _tweens.Add(tween);
    }

    public void ClearAllTweens()
    {
        if (_tweens == null)
        {
            _tweens = new List<Tween>();
        }

        foreach (var tween in _tweens)
        {
            tween.Kill();
        }

        _tweens.Clear();
    }
}