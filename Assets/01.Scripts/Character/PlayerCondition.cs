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
    Condition stamina { get { return uiCondition.stamina; } } // 스테미나 소비 및 회복 기능 추가 예정

    public event Action onTakeDamage;

    private void Update()
    {
        stamina.Add(stamina.passiveValue * Time.deltaTime);

        if(health.curValue <= 0f) // 체력 0 이하 사망
        {
            Die();
        }
    }

    public void HealStat(ConsumableType type, float amount)
    {
        if (type == ConsumableType.Health)
        {
            health.Add(amount);
        }
        else if (type == ConsumableType.Stamina)
        {
            stamina.Add(amount);
        }
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

    public bool UseStamina(float amount)
    {
        if(stamina.curValue - amount < 0)
        {
            return false;
        }
        stamina.Subtract(amount);
        return true;
    }
}
