using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void TakePhysicalDamage(int damageAmount);
}

public class PlayerCondition : MonoBehaviour, IDamagable
{
    public UICondition uiCondition;

    Condition health { get { return uiCondition.health; } }
    Condition stamina { get { return uiCondition.Stamina; } } // 스테미나 소비 및 회복 기능 추가 예정

    public event Action onTakeDamage;

    private void Update()
    {
        if(health.curValue <= 0f) // 체력 0 이하 사망
        {
            Die();
        }
    }

    public void Heal(float amount) // 체력 회복
    {
        health.Add(amount);
    }

    void Die() // 사망
    {
        Debug.Log("You Die");
    }

    public void TakePhysicalDamage(int damageAmount)
    {
        health.Subtract(damageAmount);
        onTakeDamage?.Invoke();
    }
}
