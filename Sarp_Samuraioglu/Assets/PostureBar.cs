using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostureBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public bool a;

    public void SetMaxPosture(float maxPosture)
    {
        slider.maxValue = maxPosture;
        slider.value = 0;
    }

    public void TakePostureDamage(float posturedamage)
    {
        slider.value = slider.value + posturedamage;
    }

    public void PostureBarMaxtoZero()
    {
        StartCoroutine(anan());
        Debug.Log("anan");
    }

    IEnumerator anan()
    {
        yield return new WaitForSeconds(1.4f);
        a = true;
    }

    private void Update()
    {
        if (a)
        {
            if (slider.value < 0.4)
            {
                fill.color = gradient.Evaluate(0f);
                slider.value = 0;
                a = false;
            }
            slider.value = Mathf.Lerp(slider.value, 0, .011f);
        }
    }
}
