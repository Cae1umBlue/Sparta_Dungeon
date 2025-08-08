using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float curValue; // 현재 값
    public float maxValue; // 최대 값
    public float startValue; // 시작 값
    public float passiveValue; // 증감 값
    public Image uiBar;

    private void Start()
    {
        curValue = startValue;
    }

    private void Update()
    {
        uiBar.fillAmount = GetPercentage();
    }

    public void Add(float amount) // 둘 중 작은 값
    {
        curValue = Mathf.Min(curValue + amount, maxValue);
    }

    public void Subtract(float amount) // 둘 중 큰 값
    {
        curValue = Mathf.Max(curValue - amount, 0.0f);
    }

    public float GetPercentage() // 체력 퍼센트 반환(0-1 사이의 값)
    {
        return curValue / maxValue;
    }
}
