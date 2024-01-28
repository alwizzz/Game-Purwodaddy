using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField] private DialogData dialogData;
    
    public void Play()
    {
        DialogSystem.Instance().NextDialog(dialogData);
    }
}
