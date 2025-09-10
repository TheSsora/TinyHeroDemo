using System.Collections;
using UnityEngine;

public class PowerBuffAbility : Ability
{
    [field: SerializeField] public float damageFactor { get; private set; }
    [field: SerializeField] public float attackSpeedFactor { get; private set; }
    [field:SerializeField] public float duration { get; private set; }

    public override bool TryActivate()
    {
        if(cooldownTimer > 0 || !_playerController.TrySpendMana(manaCost))
            return false;

        _playerController.SetDamageFactor(damageFactor);
        _playerController.SetAttackSpeedFactor(attackSpeedFactor);
        
        cooldownTimer = cooldown;
        StartCoroutine(BuffAction());
        
        return true;
    }

    private IEnumerator BuffAction()
    {
        yield return new WaitForSeconds(duration);
        EndBuff();
    }

    private void EndBuff()
    {
        _playerController.SetDamageFactor(-damageFactor);
        _playerController.SetAttackSpeedFactor(-attackSpeedFactor);
    }
}
