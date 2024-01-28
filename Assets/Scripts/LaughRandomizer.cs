using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class LaughRandomizer : MonoBehaviour
{
    [SerializeField] private List<AudioClip> laughClips;
    [SerializeField] private AudioSource laughSource;

    private int currentNum;
    private AudioClip currentClip;

    // Update is called once per frame
    public void PlayLaughClip()
    {

        if (!laughSource.isPlaying)
        {
            //Get a random AudioClip from the list, without repeat the same clip as before
            GetLaughClip();

            laughSource.PlayOneShot(currentClip);
        }
    }

    private AudioClip GetLaughClip()
    {
        int num;
        do
        {
            num = Randomizer();
        } while (num == currentNum);

        currentNum = num;

        return currentClip = laughClips[currentNum];
    }
    private int Randomizer()
    {
        int num;

        num = Random.Range(0, 6);
        
        return num;
    }
}
