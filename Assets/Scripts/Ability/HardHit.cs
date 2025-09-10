using UnityEngine;

public class HardHit : Ability
{
    [field:SerializeField] public float Damage { get; private set; }
    public override bool TryActivate()
    {
        if(cooldownTimer > 0 || _playerController.GetTarget() is null || _playerController.GetTarget().IsDead || !_playerController.TrySpendMana(manaCost))
            return false;

        cooldownTimer = cooldown;
        _playerController.GetTarget().TakeDamage(Damage);

        return true;
    }
}
