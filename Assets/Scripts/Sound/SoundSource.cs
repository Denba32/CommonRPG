using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SoundSource : MonoBehaviour
{
    public Define.Sound soundType = Define.Sound.BGM;

    public AudioSource audioSource;

    private Coroutine coSoundChecker = null;

    public void Init(AudioClip clip, float volume, float pitch, Define.Sound soundType = Define.Sound.BGM)
    {
        if(soundType == Define.Sound.BGM)
        {
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.pitch = pitch;
            audioSource.loop = true;
            audioSource.Play();
        }
        else if(soundType == Define.Sound.EFFECT || soundType == Define.Sound.VOICE)
        {
            if(audioSource.isPlaying)
                audioSource.Stop();
            audioSource.pitch = pitch;
            audioSource.loop = false;

            audioSource.PlayOneShot(clip, volume);            
        }

        if (soundType == Define.Sound.BGM)
            return;

        if (audioSource.clip == null) return;
        if (coSoundChecker != null)
            StopCoroutine(coSoundChecker);

        coSoundChecker = StartCoroutine(CheckSoundStopped());
    }
    private void OnEnable()
    {
        if(soundType == Define.Sound.BGM)
        {
            SoundManager.Instance.onBgmVolumeChanged += OnVolumeChange;
            SoundManager.Instance.onStopBgm += StopSound;
        }
        else if(soundType == Define.Sound.EFFECT)
        {
            SoundManager.Instance.onEffectVolumeChanged += OnVolumeChange;
            SoundManager.Instance.onStopEffect += StopSound;

        }
        else if(soundType == Define.Sound.VOICE)
        {
            SoundManager.Instance.onVoiceVolumeChanged += OnVolumeChange;
            SoundManager.Instance.onStopVoice += StopSound;

        }
    }



    private void OnDisable()
    {
        if (soundType == Define.Sound.BGM)
        {
            SoundManager.Instance.onBgmVolumeChanged -= OnVolumeChange;
            SoundManager.Instance.onStopBgm -= StopSound;
        }
        else if (soundType == Define.Sound.EFFECT)
        {
            SoundManager.Instance.onEffectVolumeChanged -= OnVolumeChange;
            SoundManager.Instance.onStopEffect -= StopSound;

        }
        else if (soundType == Define.Sound.VOICE)
        {
            SoundManager.Instance.onVoiceVolumeChanged -= OnVolumeChange;
            SoundManager.Instance.onStopVoice -= StopSound;

        }

        if (soundType == Define.Sound.BGM)
            return;

        if (coSoundChecker != null)
            StopCoroutine(coSoundChecker);
    }

    private void StopSound()
    {
        audioSource.Stop();
    }
    private void OnVolumeChange(float volume)
    {
        audioSource.volume = volume;
    }
    private IEnumerator CheckSoundStopped()
    {
       if(soundType == Define.Sound.EFFECT || soundType == Define.Sound.VOICE)
        {
            while(audioSource.isPlaying)
            {
                yield return null;
            }
            PoolManager.Instance.Push<SoundSource>(this);

        }
    }
}
