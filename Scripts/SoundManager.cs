using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MasterSingleton<SoundManager>
{
  
    private AudioSource musicSource;
    private AudioSource musicsource2;
    private AudioSource sfxSource;
    private float musicVolume=1;

    private bool FirstMusicSourcePlaying;

    public override void Init()//initilaizing 
    {
        base.Init();
        // dont destroy this instance
        DontDestroyOnLoad(this.gameObject);
        //create audio sources and save as references
        musicSource = this.gameObject.AddComponent<AudioSource>();
        musicsource2 = this.gameObject.AddComponent<AudioSource>();
        sfxSource = this.gameObject.AddComponent<AudioSource>();

        //looping music track
        musicSource.loop = true;
        musicsource2.loop = true;
    }


    public void PlayMusic(AudioClip musicClip)
    {
        //determing the active source
        AudioSource activeSource = (FirstMusicSourcePlaying) ? musicSource : musicsource2;

        activeSource.clip = musicClip;
        activeSource.volume = 1;
        activeSource.Play();
    }

    public void PlayMusicWithFade(AudioClip newClip, float transitionTime=1f)
    {
        AudioSource activeSource = (FirstMusicSourcePlaying) ? musicSource : musicsource2;
        StartCoroutine(UpdateMusicWithFade(activeSource, newClip, transitionTime));
    }

    public void PlayMusicWithCrossFade(AudioClip musicClip, float transitionTime = 1f)
    {
        //determing which source is active
        AudioSource activeSource = (FirstMusicSourcePlaying) ? musicSource : musicsource2;
        AudioSource newSource = (FirstMusicSourcePlaying) ? musicsource2 : musicSource;
        //swapping source
        FirstMusicSourcePlaying = !FirstMusicSourcePlaying;

        //setting fields of audio source, then coroutine to crossfade
        newSource.clip = musicClip;
        newSource.Play();
        StartCoroutine(UpdateMusicWithCrossFade(activeSource, newSource, transitionTime));
    }

    private IEnumerator UpdateMusicWithFade(AudioSource activeSource, AudioClip newClip, float transitionTime)
    {
        //make sure source is active and playing
        if (!activeSource.isPlaying)
        {
            activeSource.Play();
        }
        float t = 0f;
        //fade out
        for ( t = 0; t < transitionTime; t+=Time.deltaTime)
        {
            activeSource.volume = (musicVolume - (t / transitionTime)*musicVolume);

            yield return null;
        }
        activeSource.Stop();
        activeSource.clip = newClip;
        activeSource.Play();
        //fade in
        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            activeSource.volume = (t / transitionTime)*musicVolume;

            yield return null;
        }
    }

    private IEnumerator UpdateMusicWithCrossFade(AudioSource orginal,AudioSource newSource,float transitionTime)
    {
        float t = 0f;
        for ( t = 0; t <= transitionTime; t+=Time.deltaTime)
        {
            orginal.volume = (musicVolume - (t / transitionTime) * musicVolume);
            newSource.volume = (t / transitionTime) * musicVolume;
            yield return null;

        }
        orginal.Stop();
    }

    public void PlaySfx(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);//playoneshot plays with other sfx clips whilst play would cancel the others out//this is good for overlapping sounds
    }

    public void PlaySfx(AudioClip clip,float volume)
    {
        sfxSource.PlayOneShot(clip,volume);
    }

    public void SetMusicVolume(float volume)
    {
        sfxSource.volume = volume;
        musicsource2.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
