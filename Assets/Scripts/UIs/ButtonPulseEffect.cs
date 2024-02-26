using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonPulseEffect : MonoBehaviour
{
    [SerializeField] private bool isPulsing;
    [SerializeField] private float pulseSpeed;
    [SerializeField] private float minAlpha;
    [SerializeField] private float maxAlpha;

    [SerializeField] private PlayerSensor playerSensor;
    [SerializeField] private bool affectInteractability;

    private Image image;
    private Button button;

    // unexplained by the script name, but this script also adds option of adaptive interactability according
    // to whether if playerSensor.interactableDetected is true or false

    private void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();

        StopPulsing();
    }

    private void Update()
    {
        UpdateButton();

        if (!isPulsing) return;
        Pulse();
    }

    private void UpdateButton()
    {
        bool hasInteractableNearby = playerSensor.InteractableDetected();
        if(hasInteractableNearby == true && isPulsing == false)
        {
            StartPulsing();
        } else if(hasInteractableNearby == false && isPulsing == true)
        {
            StopPulsing();
        }

    }

    private void Pulse()
    {
        float alpha = Mathf.PingPong(Time.time * pulseSpeed, maxAlpha - minAlpha) + minAlpha;
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }

    public void StartPulsing()
    {
        if (affectInteractability)
        {
            button.interactable = true;
        }

        isPulsing = true;
        Reset();
    }

    public void StopPulsing()
    {
        isPulsing = false;
        Reset();

        if(affectInteractability)
        {
            button.interactable = false;
        }
    }

    private void Reset()
    {
        Color color = image.color;
        color.a = maxAlpha;
        image.color = color;
    }


}
