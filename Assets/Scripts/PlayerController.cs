using System.Collections.Generic;
using UnityEngine;

public class PlayerController : EntityController
{
    //[SerializeField] private List<Ability> abilities;
    [SerializeField] private float manaRegen;
    private float _manaTimer;
    
    protected override void Update()
    {
        base.Update();

        if (_manaTimer < 1)
            _manaTimer += Time.deltaTime;

        if (CurrentMana < MaxMana && _manaTimer >= 1)
        {
            if((CurrentMana += manaRegen) > MaxMana)
            {
                CurrentMana = MaxMana;
            }
            OnManaChanged?.Invoke(CurrentMana);
            _manaTimer = 0;
        }
        
    }

    public bool TrySpendMana(float manaCost)
    {
        if(manaCost > CurrentMana)
            return false;
        
        CurrentMana -= manaCost;
        OnManaChanged?.Invoke(CurrentMana);
        return true;
    }
    protected override void AutoAttack(EntityController target,  float damage)
    {
        base.AutoAttack(target, damage * DamageFactor);
        
        if (Random.Range(0f, 100f) <= 30f)
        {
            target.GetStunned();
        }
    }

    public EntityController GetTarget()
    {
        return attackTarget;
    }
    public void SelectNewTarget(EntityController target)
    {
        (attackTarget as EnemyController)?.targetIcon.SetActive(false);
        attackTarget = target;
        (attackTarget as EnemyController)?.targetIcon.SetActive(true);
    }
}
