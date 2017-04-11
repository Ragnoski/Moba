using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Power : MonoBehaviour
{
    [HideInInspector] public int MaxValue;
    public int BaseRegen;
    public RectTransform PowerUI;
    public Text PowerText;

    public int Value { get { return _currentValue; } }


    private int _currentValue;


    public void Initialize()
    {
        _currentValue = MaxValue;
        UpdateUI();
    }

    public void Increase(int value)
    {
        _currentValue += value;
        UpdateUI();
    }

    public void Decrease(int value)
    {
        _currentValue -= value;
        UpdateUI();
    }

    public bool HasValue(int value)
    {
        return _currentValue >= value;
    }

    public void UpdateUI()
    {
        float newpowerscale = (float)_currentValue * 1f / (float)MaxValue;
        PowerUI.localScale = new Vector3(newpowerscale, PowerUI.localScale.y, PowerUI.localScale.z);
        PowerText.text = _currentValue + " / " + MaxValue;
    }
}
