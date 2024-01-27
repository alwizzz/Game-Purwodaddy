using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header("Key Inputs")]
    [SerializeField] private KeyCode interactKey;
    
    [SerializeField] public string nextScene;
    public Animator transition;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(interactKey))
        {
            CallSceneChange();
        }
    }

    public void CallSceneChange()
    {
        StartCoroutine(SceneChange(nextScene));
    }

    IEnumerator SceneChange(string scene)
    {
        //play animation
        transition.SetTrigger("ChangeScene");

        //wait time
        yield return new WaitForSeconds(0.5f);

        //change scene
        SceneManager.LoadScene(scene);
    }

}
