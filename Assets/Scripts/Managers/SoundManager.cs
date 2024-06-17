using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public float BGMVolume { get; private set; } = 1f;
    public float EffectVolume { get; private set; } = 1f;
    public float VoiceVolume { get; private set; } = 1f;

    public event Action<float> onBgmVolumeChanged;
    public event Action<float> onEffectVolumeChanged;
    public event Action<float> onVoiceVolumeChanged;

    public event Action onStopBgm;
    public event Action onStopEffect;
    public event Action onStopVoice;
    private void Start()
    {
        // 생성 안되어있으면 생성
        if (!PoolManager.Instance.Exists("EffectSoundSource", typeof(SoundSource)))
        {
            GameObject go = Managers.GetService<ResourceManager>().Load<SoundSource>("Prefabs/Sound/EffectSoundSource").gameObject;

            PoolManager.Instance.CreatePool<SoundSource>(go, 10);
        }
        else if(!PoolManager.Instance.Exists("BGMSoundSource", typeof(SoundSource)))
        {
            GameObject go = Managers.GetService<ResourceManager>().Load<SoundSource>("Prefabs/Sound/BGMSoundSource").gameObject;

            PoolManager.Instance.CreatePool<SoundSource>(go, 3);
        }
        else if (!PoolManager.Instance.Exists("VoiceSoundSource", typeof(SoundSource)))
        {
            GameObject go = Managers.GetService<ResourceManager>().Load<SoundSource>("Prefabs/Sound/VoiceSoundSource").gameObject;

            PoolManager.Instance.CreatePool<SoundSource>(go, 10);
        }
    }

    public void Play(string path, float volume = 1.0f, float pitch = 1.0f, Define.Sound type = Define.Sound.BGM)
    {
        AudioClip clip = Managers.GetService<ResourceManager>().Load<AudioClip>(path);
        if(clip != null)
            Play(clip, volume, pitch, type);

    }
    public void Play(AudioClip clip, float volume = 1.0f, float pitch = 1.0f, Define.Sound type = Define.Sound.BGM)
    {
        if(clip != null)
        {
            switch(type)
            {
                case Define.Sound.BGM:
                    SoundSource bgmSource = Managers.GetService<ResourceManager>().Load<SoundSource>("Prefabs/Sound/BGMSoundSource");
                    PoolManager.Instance.Pop<SoundSource>(bgmSource.gameObject).Init(clip, volume, pitch, type);
                    break;

                case Define.Sound.EFFECT:
                    SoundSource effectSource = Managers.GetService<ResourceManager>().Load<SoundSource>("Prefabs/Sound/EffectSoundSource");
                    PoolManager.Instance.Pop<SoundSource>(effectSource.gameObject).Init(clip, volume, pitch, type);
                    break;

                case Define.Sound.VOICE:
                    SoundSource voiceSource = Managers.GetService<ResourceManager>().Load<SoundSource>("Prefabs/Sound/VoiceSoundSource");
                    PoolManager.Instance.Pop<SoundSource>(voiceSource.gameObject).Init(clip, volume, pitch, type);
                    break;

                default: return;

            }
        }
    }

    public void Stop(Define.Sound type = Define.Sound.BGM)
    {
        switch(type)
        {
            case Define.Sound.BGM:
                onStopBgm?.Invoke();
                break;
            case Define.Sound.EFFECT:
                onStopEffect?.Invoke();
                break;
            case Define.Sound.VOICE:
                onStopVoice?.Invoke();
                break;
        }
    }
    public void SetVolume(float volume, Define.Sound type)
    {
        switch(type)
        {
            case Define.Sound.BGM:
                BGMVolume = volume;
                onBgmVolumeChanged?.Invoke(BGMVolume);
                break;
            case Define.Sound.EFFECT:
                EffectVolume = volume;
                onEffectVolumeChanged?.Invoke(EffectVolume);
                break;
            case Define.Sound.VOICE:
                VoiceVolume = volume;
                onVoiceVolumeChanged?.Invoke(VoiceVolume);
                break;
            default: break;
        }
    }
}
