using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogInteraction : Interactable
{
    [SerializeField] private DialogData dialogData;
    public override void Interact()
    {
        Dialog();
    }

    private void Dialog()
    {
        DialogSystem.Instance().NextDialog(dialogData);
    }
}
