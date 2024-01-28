using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class CutsceneManager : MonoBehaviour
{
    [Header("Key Inputs")]
    [SerializeField] private KeyCode nextKey;
    
    [Header("Cutscenes")]
    [SerializeField] private GameObject cutScene1;
    [SerializeField] private GameObject cutScene2;
    [SerializeField] private GameObject cutScene3;
    [SerializeField] private GameObject cutScene4;
    [SerializeField] private GameObject cutScene5;
    [SerializeField] private GameObject cutScene6;

    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private AudioSource audioSource;

    void Start()
    {
        audioSource.PlayDelayed(0.7f);
    }
    void Update()
    {
        if (Input.GetKeyDown(nextKey))
        {
            sceneLoader.CallSceneChange();
        }
    }

    public void change_to_cutscene2()
    {
        cutScene2.SetActive(true);
        cutScene1.SetActive(false);
    }

    public void change_to_cutscene3()
    {
        cutScene3.SetActive(true);
        cutScene2.SetActive(false);
    }

    public void change_to_cutscene4()
    {
        cutScene4.SetActive(true);
        cutScene3.SetActive(false);
    }

    public void change_to_cutscene5()
    {
        cutScene5.SetActive(true);
        cutScene4.SetActive(false);
    }

    public void change_to_cutscene6()
    {
        cutScene6.SetActive(true);
        cutScene5.SetActive(false);
    }
}
