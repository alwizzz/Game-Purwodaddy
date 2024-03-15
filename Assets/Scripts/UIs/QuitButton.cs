using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuitButton : MonoBehaviour
{

    public void Quit()
    {
        Application.Quit();
    }

    // Call this method to deselect the button
    public void DeselectButton()
    {
        // Check if this GameObject is currently selected
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            // Deselect the button
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}
