using System;
using UnityEngine;

[Serializable]
public abstract class Ability : MonoBehaviour
{
    public Action<float>  OnCooldown;
    [SerializeField] protected PlayerController _playerController;
    [field:SerializeField] public float cooldown { get; private set; }
    [field:SerializeField] public float manaCost { get; private set; }

    protected float cooldownTimer = 0;
    protected virtual void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            OnCooldown?.Invoke(cooldownTimer);
        }
           
    }

    public abstract bool TryActivate();
}
