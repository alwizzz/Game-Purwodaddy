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
        SoundEffectManager.Instance().PlayOneShotDialogSFX();
        DialogSystem.Instance().NextDialog(dialogData);
    }

    public void SetDialogData(DialogData data)
    {
        dialogData = data;
    }
}
