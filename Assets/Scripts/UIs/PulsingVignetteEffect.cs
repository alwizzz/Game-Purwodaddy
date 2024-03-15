using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PulsingVignetteEffect : MonoBehaviour
{
    [SerializeField] private bool isPulsing;

    [Header("Params")]
    [SerializeField] private float pulseSpeed;
    [SerializeField] private float minAlpha;
    [SerializeField] private float maxAlpha;
    [SerializeField] private Color color;

    [Header("Original Params")]
    // assumes the minAlpha is the originalIntensity
    // [SerializeField] private float originalIntensity; 
    [SerializeField] private Color originalColor;

    private PostProcessVolume volume;
    private Vignette vignetteLayer;

    // Start is called before the first frame update
    void Awake()
    {
        volume = GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out vignetteLayer);
    }

    private void Update()
    {
        if (!isPulsing) return;

        Pulse();
    }

    public void StartPulsing()
    {
        vignetteLayer.color.value = color;
        vignetteLayer.intensity.value = minAlpha;
        isPulsing = true;
    }

    public void StopPulsing()
    {
        vignetteLayer.color.value = originalColor;
        vignetteLayer.intensity.value = minAlpha;
        isPulsing = false;
    }

    private void Pulse()
    {
        float value = Mathf.PingPong(Time.time * pulseSpeed, maxAlpha - minAlpha) + minAlpha;
        vignetteLayer.intensity.value = value;
    }
}
