using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SwitchToggle : MonoBehaviour
{
    [SerializeField] RectTransform uiHandleReactTransform;
    [SerializeField] Color backgroundDeactiveColor;
    [SerializeField] Color handleDeactiveColor;

    Image backgroundImage, handleImage;

    Color backgroundDefaultColor, handleDefaultColor;

    Toggle toggle;

    Vector2 handlePosition;

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
            OnSwitch(true);
    }

    void OnSwitch(bool on)
    {
        //uiHandleReactTransform.anchoredPosition = on ? handlePosition * -1 : handlePosition;
        uiHandleReactTransform.DOAnchorPos(on ? handlePosition : handlePosition*-1, .4f).SetEase(Ease.InOutBack);
        //backgroundImage.color = on ? backgroundActiveColor : backgroundDefaultColor;
        backgroundImage.DOColor(on ? backgroundDefaultColor : backgroundDeactiveColor, .6f);
        //handleImage.color = on ? handleActiveColor : handleDefaultColor;
        handleImage.DOColor(on ? handleDefaultColor : handleDeactiveColor, .4f);
    }

    void OnDestroy()
    {
        
    }
}
