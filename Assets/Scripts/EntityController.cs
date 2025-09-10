using System;
using System.Collections;
using UnityEngine;

public abstract class EntityController : MonoBehaviour
{
    public Action<float> OnHealthChanged;
    public Action<float> OnManaChanged;
    
    public float MaxHealth;
    protected float CurrentHealth;
    
    public float MaxMana;
    protected float CurrentMana;

    public float Damage;
    [field:SerializeField] public float DamageFactor { get; private set; }

    public float BaseAttackSpeed;
    
    protected float AttackSpeed;
    [field:SerializeField] public float AttackSpeedFactor { get; protected set; }

    
    protected float attackTimer = 0;
    public bool IsStunned { get; private set; } = false;
    public bool IsDead { get; protected set; } = false;
    
    private Coroutine _stunCoroutine;

    public EntityController attackTarget;
    
    [SerializeField] protected Animator animator;
    [SerializeField] private ParticleSystem stunnedVFX;
    public void SetDamageFactor(float factor)
    {
        DamageFactor += factor;
    }
    public void SetAttackSpeedFactor(float factor)
    {
        AttackSpeedFactor += factor;
    }
    protected void OnEnable()
    {
        CurrentHealth = MaxHealth;
        CurrentMana = MaxMana;
        AttackSpeed = BaseAttackSpeed;
    }
    protected virtual void Update()
    {
        if (IsDead || IsStunned)
            return;
        
        if (attackTimer >= AttackSpeed * AttackSpeedFactor && attackTarget != null)
        {
            AutoAttack(attackTarget, Damage);
            attackTimer = 0;
        }
        attackTimer += Time.deltaTime;
    }
    protected virtual void AutoAttack(EntityController target, float damage)
    {
        if (target.IsDead)
            return;
        animator.SetTrigger("Attack");
        target?.TakeDamage(damage);
    }

    public void TakeDamage(float damage)
    {
        if(IsDead)
            return;
        
        CurrentHealth -= damage;
        OnHealthChanged?.Invoke(CurrentHealth);
        if (CurrentHealth <= 0)
        {
            Dead();
        }
    }

    public void GetStunned()
    {
        IsStunned = true;
        stunnedVFX.Play();
        if(_stunCoroutine != null)
            StopCoroutine(_stunCoroutine);
        _stunCoroutine = StartCoroutine(Stunned());
    }
    private IEnumerator Stunned()
    {
        yield return new WaitForSeconds(2f);
        IsStunned = false;
        stunnedVFX.Stop();
        _stunCoroutine = null;
    }
    protected virtual void Dead()
    {
        IsDead = true;
        animator.SetBool("IsDead", true);
        Debug.Log("Dead");
    }
}
