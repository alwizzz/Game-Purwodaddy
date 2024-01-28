using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : StaticReference<SoundEffectManager>
{
    [SerializeField] private List<AudioClip> laughClips;
    [SerializeField] private AudioClip lastClip;
    private AudioSource audioSource;

    [SerializeField] private float delay;
    [SerializeField] private bool isPlayingContinous;

    private void Awake()
    {
        BaseAwake(this);

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayLaughSoundEffect()
    {
        var copy = new List<AudioClip>(laughClips);
        if(lastClip != null)
        {
            copy.Remove(lastClip);
        }

        var randomIndex = Random.Range(0, copy.Count);

        lastClip = copy[randomIndex]; ;
        audioSource.clip = lastClip;
        audioSource.Play();

        print("played " + lastClip.name);
    }


    public void PlayContinous()
    {
        isPlayingContinous = true;
        StartCoroutine(PlayContinousWithDelay());
    }

    private IEnumerator PlayContinousWithDelay()
    {
        while (isPlayingContinous)
        {
            PlayLaughSoundEffect();
            yield return new WaitForSeconds(delay);
        }
    }

    public void StopPlayContinous()
    {
        isPlayingContinous = false;
    }



    private void OnDestroy()
    {
        BaseOnDestroy();
    }
}
