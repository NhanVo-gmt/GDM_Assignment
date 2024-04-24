using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyUI : MonoBehaviour
{
    private Slider slider;

    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
    }

    public void UpdateEnergyUI(float percentValue)
    {
        slider.value = percentValue;
    }
}
