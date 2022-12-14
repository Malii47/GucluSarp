using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostureBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxPosture(float maxPosture)
    {
        slider.maxValue = maxPosture;
        slider.value = 0;
    }

    public void TakePostureDamage(float posturedamage)
    {
        slider.value = slider.value + posturedamage;
    }
}
