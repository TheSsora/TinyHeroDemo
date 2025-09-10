using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityUIController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Ability ability;
    [SerializeField] private Image image;

    private void OnEnable()
    {
        ability.OnCooldown += OnCooldown;
    }
    private void OnCooldown(float time)
    {
        image.fillAmount = time/ability.cooldown;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        ability.TryActivate();
    }

    private void OnDisable()
    {
        ability.OnCooldown -= OnCooldown;
    }
}
