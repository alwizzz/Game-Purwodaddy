using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public enum InteractionType
    {
        TALK,
        TAKE,
        INSPECT
    }

    public InteractionType interactionType;
    protected bool isFocusedByPlayer;
    public abstract void Interact();

    public void SetIsFocusedByPlayer(bool value) { isFocusedByPlayer = value; }
    public bool IsFocusedByPlayer() => isFocusedByPlayer;
}
