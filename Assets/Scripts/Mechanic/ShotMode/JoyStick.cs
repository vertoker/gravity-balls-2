using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// Скрипт виртуального джостика для UI
public class JoyStick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public Image bgImg;
    public Image jsImg;
    private Vector2 inputVector;

    public void OnPointerDown(PointerEventData p) { OnDrag(p); }

    public void OnDrag(PointerEventData p)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform, p.position, p.pressEventCamera, out pos))
        {
            pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);

            inputVector = new Vector2(pos.x * 2, pos.y * 2);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            float k = 2f;
            float x = inputVector.x * (bgImg.rectTransform.sizeDelta.x / k);
            float y = inputVector.y * (bgImg.rectTransform.sizeDelta.y / k);

            jsImg.rectTransform.anchoredPosition = new Vector2(x, y);
        }
    }

    public void OnPointerUp(PointerEventData p)
    {
        jsImg.rectTransform.anchoredPosition = new Vector2();
        inputVector = Vector2.zero;
    }

    public void ResetJoystick()
    {
        jsImg.rectTransform.anchoredPosition = new Vector2();
        inputVector = Vector2.zero;
    }

    public Vector2 Joy() { return inputVector; }
}