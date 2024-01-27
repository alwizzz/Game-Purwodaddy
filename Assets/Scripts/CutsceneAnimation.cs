using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneAnimation : MonoBehaviour
{
    [SerializeField] public CutsceneManager cutsceneManager;

    public Animator animate;
    
    public void progressToCutscene2()
    {
        cutsceneManager.GetComponent<CutsceneManager>().change_to_cutscene2();
    }

    public void progressToCutscene3()
    {
        cutsceneManager.GetComponent<CutsceneManager>().change_to_cutscene3();
    }

    public void progressToCutscene4()
    {
        cutsceneManager.GetComponent<CutsceneManager>().change_to_cutscene4();
    }

    public void progressToCutscene5()
    {
        cutsceneManager.GetComponent<CutsceneManager>().change_to_cutscene5();
    }
}
