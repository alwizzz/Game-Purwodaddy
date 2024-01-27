using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectInteraction : Interactable
{
    [SerializeField] private string informationKey;
    [SerializeField] private bool inspectable;


    public override void Interact()
    {
        Inspect();
    }

    private void Inspect()
    {
        InformationSystem.Instance().AddInformation(informationKey);
        SetInspectable(false);
    }

    public void SetInspectable(bool value)
    { 
        inspectable = value;
        
        var collider = GetComponent<Collider2D>();
        if(inspectable)
        {
            collider.enabled = true;
        } else
        {
            collider.enabled = false;
        }
    }
}
