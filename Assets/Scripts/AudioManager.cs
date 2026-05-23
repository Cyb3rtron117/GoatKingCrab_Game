using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    [Header("Music")]
    public AudioClip[] musicTracks;

    [Header("SFX")]
    public AudioClip[] sfxClips;

    [Header("Ambience")]
    public AudioClip[] ambienceClips;

    [Header("Volume")]
    [Range(0, 1f)] public float musicVolume;
    [Range(0, 1f)] public float sfxVolume;
    [Range(0, 1f)] public float ambienceVolume;

    private AudioSource musicSource;
    private AudioSource sfxSource;
    private AudioSource ambienceSource;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        musicSource = CreateSource("Music", musicVolume, true);
        sfxSource = CreateSource("SFX", musicVolume, false);
        ambienceSource = CreateSource("Ambience", musicVolume, true);
    }

    AudioSource CreateSource(string sourceName, float volume, bool loop)
    {
        var go = new GameObject(sourceName);
        go.transform.SetParent(transform);
        var src = go.AddComponent<AudioSource>();
        src.volume = volume;
        src.loop = loop;
        return src;
    }

    public void PlayMusic(int index)
    {
        if (index < 0 || index >= musicTracks.Length)
        {
            return;
        }

        musicSource.clip = musicTracks[index];
        musicSource.Play();
    }

    public void StopMusic() => musicSource.Stop();
    public void PauseMusic() => musicSource.Pause();
    public void ResumeMusic() => musicSource.UnPause();

    public void PlaySFX(int index)
    {
        if (index < 0 || index >= sfxClips.Length)
        {
            return;
        }

        sfxSource.PlayOneShot(sfxClips[index]);
    }

    public void PlayAmbience(int index)
    {
        if (index < 0 || index >= ambienceClips.Length)
        {
            return;
        }

        ambienceSource.clip = ambienceClips[index];
        ambienceSource.Play();
    }

    public void StopAmbience() => ambienceSource.Stop();

    public void SetMusicVolume (float v) { musicVolume = v; musicSource.volume = v; }
    public void SetSFXVolume(float v) { sfxVolume = v; sfxSource.volume = v; }
    public void SetAmbienceVolume(float v) { ambienceVolume = v; ambienceSource.volume = v; }
}
