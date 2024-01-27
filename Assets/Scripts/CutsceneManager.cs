using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class CutsceneManager : MonoBehaviour
{
    [Header("Key Inputs")]
    [SerializeField] private KeyCode nextKey;
    
    [SerializeField] public GameObject cutScene1;
    [SerializeField] public GameObject cutScene2;
    [SerializeField] public GameObject cutScene3;
    [SerializeField] public GameObject cutScene4;
    [SerializeField] public GameObject cutScene5;

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
}
