using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;

    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SoundManager>();

                if (_instance == null)
                {
                    GameObject go = new GameObject("SoundManager");
                    _instance = go.AddComponent<SoundManager>();
                }
            }
            return _instance;
        }
    }

    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
    }

    // ���� ����
    public Sound[] bgmSounds;
    public Sound[] sfxSounds;

    // ����� �ҽ� Ǯ
    public int sfxChannelCount = 6;
    private AudioSource bgmSource;
    private AudioSource[] sfxSources;
    private int currentSfxIndex = 0;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);

            // ����� �ҽ� �ʱ�ȭ
            InitAudioSources();
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void InitAudioSources()
    {
        // BGM ����� �ҽ� ����
        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.loop = true;

        // SFX ����� �ҽ� Ǯ ����
        sfxSources = new AudioSource[sfxChannelCount];
        for (int i = 0; i < sfxChannelCount; i++)
        {
            sfxSources[i] = gameObject.AddComponent<AudioSource>();
            sfxSources[i].loop = false;
        }
    }

    // BGM ���
    public void PlayBGM(string name)
    {
        Sound bgm = FindSound(name, bgmSounds);
        if (bgm != null)
        {
            if (bgmSource.clip != bgm.clip)
            {
                bgmSource.clip = bgm.clip;
                bgmSource.Play();
            }
        }
        else
        {
            Debug.LogWarning("BGM�� ã�� �� �����ϴ�: " + name);
        }
    }

    // ȿ���� ���
    public void PlaySFX(string name)
    {
        Sound sfx = FindSound(name, sfxSounds);
        if (sfx != null)
        {
            // ���� ��� ������ ����� �ҽ� ä�� ã��
            AudioSource source = GetNextSfxSource();
            source.clip = sfx.clip;
            source.Play();
        }
        else
        {
            Debug.LogWarning("ȿ������ ã�� �� �����ϴ�: " + name);
        }
    }

    // ���� �Ҹ� ���
    public void PlayJumpSound()
    {
        PlaySFX("Jump");
    }

    // ���� ȹ�� �Ҹ� ���
    public void PlayCoinSound()
    {
        PlaySFX("Coin");
    }

    // ���� ����
    public void StopBGM()
    {
        bgmSource.Stop();
    }

    // ���� ����
    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        foreach (AudioSource source in sfxSources)
        {
            source.volume = volume;
        }
    }

    // ���� ����� SFX ����� �ҽ� ��ȯ
    private AudioSource GetNextSfxSource()
    {
        // ���� ��� ������ ���� ����� �ҽ��� ���� ã��
        for (int i = 0; i < sfxSources.Length; i++)
        {
            if (!sfxSources[i].isPlaying)
            {
                return sfxSources[i];
            }
        }

        // ��� �ҽ��� ��� ���̸� ���� �κ� ������� ���� �ҽ� ����
        AudioSource source = sfxSources[currentSfxIndex];
        currentSfxIndex = (currentSfxIndex + 1) % sfxSources.Length;
        return source;
    }

    // ���� �̸����� ã��
    private Sound FindSound(string name, Sound[] sounds)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name)
            {
                return s;
            }
        }
        return null;
    }

   





}
