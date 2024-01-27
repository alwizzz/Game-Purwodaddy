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
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(nextKey))
        {
            cutScene2.SetActive(false);
        }
    }
}
