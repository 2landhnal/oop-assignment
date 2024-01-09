using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField]
    private AudioSource menuMusic, stageMusic;
    [SerializeField]
    private AudioSource[] sfxs;
    public AudioMixer mainMixer;
    public float musicOn, musicOff;
    public float sfxOn, sfxOff;
    private bool musicMute, sfxMute;
    public string sfx, music;
    protected override void Awake()
    {
        MakeSingleton(true);
    }
    /*    private void Start()
        {
            MusicVolume(Data.GetMusicVolume());
            SFXVolume(Data.GetSFXVolume());
        }*/

    public void PlaySFX(int number)
    {
        if (0 <= number && number <= sfxs.Length - 1)
        {
            sfxs[number].Stop();
            sfxs[number].Play();
        }
    }
    public void MenuMusicButton(bool isOn)
    {
        if (isOn)
        {
            stageMusic.Stop();  
            menuMusic.Play();
        }
        else
        {
            menuMusic.Stop();
        }
    }
    public void StageMusicButton(bool isOn)
    {
        if (isOn)
        {
            menuMusic.Stop();
            stageMusic.Play();
        }
        else
        {
            stageMusic.Stop();
        }
    }

    public void StopSFX(int number)
    {
        if (0 <= number && number <= sfxs.Length - 1)
        {
            sfxs[number].Stop();
        }
    }
    public void PlaySFXAdjusted(int number)
    {
        if (0 <= number && number <= sfxs.Length - 1)
        {
            sfxs[number].pitch = Random.Range(.8f, 1.2f);
            sfxs[number].Stop();
            sfxs[number].Play();
        }
    }
    public void MusicMuteUnMute()
    {
        //PlaySFX(6);
        musicMute = !musicMute;
        if (musicMute)
        {
            mainMixer.SetFloat(music, musicOff);
        }
        else
        {
            mainMixer.SetFloat(music, musicOn);
        }
    }

    public void MusicVolume(float value)
    {
        mainMixer.SetFloat(music, 30 * (value - 1) == -30 ? -80 : 30 * (value - 1));
    }

    public void SFXVolume(float value)
    {
        mainMixer.SetFloat(sfx, 30 * (value - 1) == -30 ? -80 : 30 * (value - 1));
    }

    public void SFXMuteUnMute()
    {
        if (!sfxMute)
        {
            mainMixer.SetFloat(sfx, sfxOff);
            sfxMute = true;
        }
        else
        {
            float volume = 1;
            if (PlayerPrefs.HasKey("sfxVolume"))
            {
                volume = PlayerPrefs.GetFloat("sfxVolume") / 80 + 1;
            }
            mainMixer.SetFloat(sfx, volume);
            sfxMute = false;
        }
    }
}
