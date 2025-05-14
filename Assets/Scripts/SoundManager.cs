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

    // 사운드 종류
    public Sound[] bgmSounds;
    public Sound[] sfxSounds;

    // 오디오 소스 풀
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

            // 오디오 소스 초기화
            InitAudioSources();
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void InitAudioSources()
    {
        // BGM 오디오 소스 생성
        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.loop = true;

        // SFX 오디오 소스 풀 생성
        sfxSources = new AudioSource[sfxChannelCount];
        for (int i = 0; i < sfxChannelCount; i++)
        {
            sfxSources[i] = gameObject.AddComponent<AudioSource>();
            sfxSources[i].loop = false;
        }
    }

    // BGM 재생
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
            Debug.LogWarning("BGM을 찾을 수 없습니다: " + name);
        }
    }

    // 효과음 재생
    public void PlaySFX(string name)
    {
        Sound sfx = FindSound(name, sfxSounds);
        if (sfx != null)
        {
            // 다음 사용 가능한 오디오 소스 채널 찾기
            AudioSource source = GetNextSfxSource();
            source.clip = sfx.clip;
            source.Play();
        }
        else
        {
            Debug.LogWarning("효과음을 찾을 수 없습니다: " + name);
        }
    }

    // 점프 소리 재생
    public void PlayJumpSound()
    {
        PlaySFX("Jump");
    }

    // 코인 획득 소리 재생
    public void PlayCoinSound()
    {
        PlaySFX("Coin");
    }

    // 사운드 중지
    public void StopBGM()
    {
        bgmSource.Stop();
    }

    // 볼륨 조절
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

    // 다음 사용할 SFX 오디오 소스 반환
    private AudioSource GetNextSfxSource()
    {
        // 현재 재생 중이지 않은 오디오 소스를 먼저 찾음
        for (int i = 0; i < sfxSources.Length; i++)
        {
            if (!sfxSources[i].isPlaying)
            {
                return sfxSources[i];
            }
        }

        // 모든 소스가 재생 중이면 라운드 로빈 방식으로 다음 소스 선택
        AudioSource source = sfxSources[currentSfxIndex];
        currentSfxIndex = (currentSfxIndex + 1) % sfxSources.Length;
        return source;
    }

    // 사운드 이름으로 찾기
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
