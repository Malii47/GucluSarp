using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SwitchTogglePaused : MonoBehaviour
{
    [SerializeField] RectTransform uiHandleReactTransform;
    [SerializeField] Color backgroundDeactiveColor;
    [SerializeField] Color handleDeactiveColor;

    Image backgroundImage, handleImage;

    Color backgroundDefaultColor, handleDefaultColor;

    Toggle toggle;

    Vector2 handlePosition;

    GameObject SaveToggle;

    public float rate;

    void Start()
    {
        toggle = GetComponent<Toggle>();
        handlePosition = uiHandleReactTransform.anchoredPosition;
        backgroundImage = uiHandleReactTransform.parent.GetComponent<Image>();
        handleImage = uiHandleReactTransform.GetComponent<Image>();
        backgroundDefaultColor = backgroundImage.color;
        handleDefaultColor = handleImage.color;

        toggle.onValueChanged.AddListener(OnSwitch);

        if (toggle.isOn == true)
        {
            OnSwitch(true);
        }
    }

    void OnSwitch(bool on)
    {
        //uiHandleReactTransform.anchoredPosition = on ? handlePosition : handlePosition * -1;

        if (on)
        {
            uiHandleReactTransform.anchoredPosition = handlePosition;

        }
        else
        {
            uiHandleReactTransform.anchoredPosition = handlePosition * -1;
        }

        //backgroundImage.color = on ? backgroundDefaultColor : backgroundDeactiveColor;

        if (on)
        {
            backgroundImage.color = backgroundDefaultColor;


        }
        else
        {
            backgroundImage.color = backgroundDeactiveColor;

        }

        //handleImage.color = on ? handleDefaultColor : handleDeactiveColor; 

        if (on)
        {
            handleImage.color = handleDefaultColor;

        }
        else
        {
            handleImage.color = handleDeactiveColor;

        }
    }

    void OnDestroy()
    {

    }
}
