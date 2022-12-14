using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void SetStartingHealth(float startingHealth)
    {
        slider.maxValue = startingHealth;
        slider.value = startingHealth;
    }

    public void TakeDamage(float damage)
    {
        slider.value = slider.value - damage;
    }
}
