using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField]
    AudioClip MenuSound;

    [SerializeField]
    AudioClip SuccessSound;

    [SerializeField]
    AudioClip[] ClickSounds = new AudioClip[4];

    [SerializeField]
    AudioClip[] LoFiBeats;

    void Awake()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    public void StartMusic(int num)
    {
        audioSource.clip = LoFiBeats[num];
        audioSource.Play();
    }
    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void PlayMenuSound()
    {
        audioSource.PlayOneShot(MenuSound, 1f);
    }

    public void PlaySuccessSound()
    {
        audioSource.PlayOneShot(SuccessSound, 1f);
    }

    public void PlayClickSound()
    {
        int num = Random.Range(0, ClickSounds.Length - 1);
        audioSource.PlayOneShot(ClickSounds[num], .5f);
    }

}
