using System;
using Unity.VisualScripting;
using UnityEngine;

public class HealthBar : ResourceBarController
{
    private void OnEnable()
    {
        entity.OnHealthChanged += OnHealthChanged;
        bar.maxValue = entity.MaxHealth;
        bar.value = bar.maxValue;
    }
    private void OnDisable()
    {
        entity.OnHealthChanged -= OnHealthChanged;
    }
    private void OnHealthChanged(float value)
    {
        bar.value = value;
    }
}
