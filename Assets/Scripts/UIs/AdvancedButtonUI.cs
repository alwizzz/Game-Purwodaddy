using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class AdvancedButtonUI : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private bool isButtonDown; 

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void PressButton()
    {
        if (isButtonDown) { return; }

        //print($"PressButton() [{gameObject}] isButtonDown before: {isButtonDown}");
        isButtonDown = true;
        //print($"PressButton() [{gameObject}] isButtonDown after: {isButtonDown}");
    }

    private void ReleaseButton()
    {
        if(!isButtonDown) { return; }

        //print($"ReleaseButton() [{gameObject}] isButtonDown before: {isButtonDown}");
        isButtonDown = false;
        //print($"ReleaseButton() [{gameObject}] isButtonDown after: {isButtonDown}");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //print("hello");
        ReleaseButton();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PressButton();
    }

    public bool IsButtonDown() => isButtonDown;
    public Button GetButton() => button;
}
