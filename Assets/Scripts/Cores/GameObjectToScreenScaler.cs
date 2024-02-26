using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectToScreenScaler : MonoBehaviour
{
    [SerializeField] private int preferredScreenHeight;
    [SerializeField] private int preferredScreenWidth;
    [SerializeField] private int currentScreenHeight;
    [SerializeField] private int currentScreenWidth;

    [Header("Ratios")]
    [SerializeField] private float preferredScreenRatio;
    [SerializeField] private float currentScreenRatio;
    
    [Tooltip("csr as in Current Screen Ratio and psr as in Preferred Screen Ratio")]
    [SerializeField] private float csrToPsrRatio;

    [Header("ParticleSystem")]
    [SerializeField] private List<ParticleSystem> affectedParticleSystems;

    private void Awake()
    {
        preferredScreenRatio = (float)preferredScreenWidth / (float)preferredScreenHeight;
    }

    private void Update()
    {
        bool screenSizeIsAdjusted = CheckScreenSizeAdjustment();
        if(screenSizeIsAdjusted == false) { return; }

        Scale();
    }

    private bool CheckScreenSizeAdjustment()
    {
        if (currentScreenHeight != Screen.height || currentScreenWidth != Screen.width)
        {
            currentScreenHeight = Screen.height;
            currentScreenWidth = Screen.width;

            currentScreenRatio = (float)currentScreenWidth / (float)currentScreenHeight;
            csrToPsrRatio = currentScreenRatio / preferredScreenRatio;

            return true;
        }

        return false;
    }

    private void Scale()
    {
        // scaling to smaller gameobject only occurs if current screen ratio is less than preferred screen ratio

        // while current screen ratio bigger than preferred screen ratio would not obstruct the gameobject, 
        // hence no need for scaling process

        //Vector3 scaleVector = Vector3.zero;
        Vector3 scaleVector = (
            csrToPsrRatio >= 1f ?
            Vector3.one 
            :
            new Vector3(csrToPsrRatio, csrToPsrRatio, csrToPsrRatio)
        );
        //if (csrToPsrRatio >= 1f)
        //{
        //    // if current screen ratio exceeds preferred screen ratio, set the scale to default 1x1x1
        //    scaleVector = Vector3.one;
        //} else
        //{
        //    scaleVector = new Vector3(csrToPsrRatio, csrToPsrRatio, csrToPsrRatio);
        //}

        transform.localScale = scaleVector;
        affectedParticleSystems.ForEach(
            e =>
            {
                var shapeModule = e.shape;
                shapeModule.scale = scaleVector;
            }
        );
    }
}
