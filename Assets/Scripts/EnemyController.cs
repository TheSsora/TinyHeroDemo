using System.Collections;
using UnityEngine;

public class EnemyController : EntityController
{
    [field:SerializeField] public GameObject targetIcon { get; private set; }
    protected override void AutoAttack(EntityController target, float damage)
    {
        base.AutoAttack(target, damage);

        if (Random.Range(0f, 100f) <= 25f)
        {
            if ((CurrentHealth += 5f) > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
            OnHealthChanged?.Invoke(CurrentHealth);
        }
    }

    protected override void Dead()
    {
        base.Dead();
        
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2f);
        animator.SetBool("IsDead", false);
        yield return new WaitForSeconds(1f);
        CurrentHealth = MaxHealth;
        OnHealthChanged?.Invoke(CurrentHealth);
        IsDead = false;
    }
    
}
