using System;
using UnityEngine;

public class ManaBar : ResourceBarController
{
    private void OnEnable()
    {
        entity.OnManaChanged += OnManaChanged;
        bar.maxValue = entity.MaxMana;
        bar.value = bar.maxValue;
    }

    private void OnDisable()
    {
        entity.OnManaChanged -= OnManaChanged;
    }

    private void OnManaChanged(float value)
    {
        bar.value = value;
    }
}
