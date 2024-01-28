using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneTrigger : MonoBehaviour
{
    [Header("Key Inputs")]
    [SerializeField] private KeyCode ContinueKey;
    [SerializeField] private SceneLoader sceneLoader;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(ContinueKey))
        {
            sceneLoader.CallSceneChange();
        }
    }
}
