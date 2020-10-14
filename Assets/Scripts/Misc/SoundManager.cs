using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public AudioClip audioClip;
    public string soundName;
}

public class SoundManager : MonoBehaviour
{
    [Header("Random Sounds")]
    [SerializeField] private Sound[] sounds;

    [Header("Often Used")]
    [SerializeField] private AudioClip footStep;

    [Header("Music")]
    [SerializeField] private AudioClip introSong;
    [SerializeField] private AudioClip villageSong;
    [SerializeField] private AudioClip interiorSong;
    [SerializeField] private AudioClip bossSong;

    private AudioSource audioSource;
    private string currentSong;

    // TODO: PreLoadAudioCLips on awake so it can be played faster
    // TODO: Use singleton

    private void Awake()
    {
        // needs to be in awake because other scripts might run before this script
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string soundName)
    {
        FindSoundInArrayAndPlay(soundName);
    }

    public void PlaySoundRandom(string[] soundNames)
    {
        string soundName = soundNames[Random.Range(0, soundNames.Length)];
        FindSoundInArrayAndPlay(soundName);
    }

    private void FindSoundInArrayAndPlay(string soundName)
    {
        foreach (Sound sound in sounds)
        {
            if (sound.soundName == soundName)
            {
                audioSource.PlayOneShot(sound.audioClip);
                break;
            }
        }
    }

    public void PlayFootStep()
    {
        audioSource.PlayOneShot(footStep);
    }

    public void PlayMusic(string song)
    {
        if(audioSource != null && currentSong != song)
        {
            switch (song)
            {
                case "introSong":
                    audioSource.clip = introSong;
                    break;
                case "villageSong":
                    audioSource.clip = villageSong;
                    break;
                case "interiorSong":
                    audioSource.clip = interiorSong;
                    break;
                case "bossSong":
                    audioSource.clip = bossSong;
                    break;
            }

            audioSource.Play();
            currentSong = song;
        }
    }

    public void StopMusic()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
            audioSource.clip = null;
        }
    }
}
