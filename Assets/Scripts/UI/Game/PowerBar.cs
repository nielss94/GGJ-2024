using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour
{
    [SerializeField] private Thrower thrower;
    [SerializeField] private Image fillImage;

    private void Awake()
    {
        thrower.OnUpdateThrowForce += UpdatePower;
    }
    
    private void UpdatePower(float power)
    {
        fillImage.fillAmount = power;
    }
}
