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
    public abstract void Interact();
}
