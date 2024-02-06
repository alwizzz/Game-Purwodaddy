using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CutsceneAnimation : MonoBehaviour
{
    [SerializeField] private CutsceneManager cutsceneManager;
    [SerializeField] private SceneLoader sceneLoader;

    public Animator animate;
    
    public void progressToCutscene2()
    {
        cutsceneManager.change_to_cutscene2();
    }

    public void progressToCutscene3()
    {
        cutsceneManager.change_to_cutscene3();
    }

    public void progressToCutscene4()
    {
        cutsceneManager.change_to_cutscene4();
    }

    public void progressToCutscene5()
    {
        cutsceneManager.change_to_cutscene5();
    }

    public void progressToCutscene6()
    {
        cutsceneManager.change_to_cutscene6();
    }

    public void progressToNextGameScene()
    {
        gameObject.SetActive(false);
        sceneLoader.CallSceneChange();
    }
}
