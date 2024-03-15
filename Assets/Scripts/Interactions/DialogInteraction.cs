using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogInteraction : Interactable
{
    [SerializeField] private DialogData dialogData;
    [SerializeField] private UnityEvent eventsBeforeDialog;
    public override void Interact()
    {
        eventsBeforeDialog.Invoke();
        Dialog();
    }

    private void Dialog()
    {
        SoundEffectManager.Instance().PlayOneShotDialogSFX();
        DialogSystem.Instance().NextDialog(dialogData);
    }

    public void SetDialogData(DialogData data)
    {
        dialogData = data;
    }
}
