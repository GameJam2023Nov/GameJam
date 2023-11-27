using SL;
using UnityEngine;

public class MusicAndSfxService : ServiceCustom, IMusicAndSfxService
{
    [SerializeField] private int countLayers = 10;
    [SerializeField] private AudioSource[] _layers;
    private AudioSource _sfx;
    private bool _isMute;
    private float _volumeMusic;
    private float _volumeSfx;

    private void Reset()
    {
        PlayerPrefs.SetFloat("VolumeMusic", 1);
        PlayerPrefs.SetFloat("VolumeSfx", 1);
        _layers = new AudioSource[countLayers];
        for (var i = 0; i < countLayers; i++)
        {
            _layers[i] = gameObject.AddComponent<AudioSource>();
            _layers[i].loop = true;
            _layers[i].playOnAwake = false;
        }
        _sfx = gameObject.AddComponent<AudioSource>();
        _sfx.playOnAwake = false;
    }

    protected override void CustomStart()
    {
        base.CustomStart();
        _volumeMusic = PlayerPrefs.GetFloat("VolumeMusic", 1);
        _volumeSfx = PlayerPrefs.GetFloat("VolumeSfx", 1);
        PlayMusic();
    }

    public void PlayMusic()
    {
        foreach (var t in _layers)
        {
            t.volume = _volumeMusic;
            t.Play();
        }
    }

    public void PlaySfx(AudioClip sfx)
    {
        _sfx.PlayOneShot(sfx);
    }

    public void Mute()
    {
        _isMute = !_isMute;
        foreach (var t in _layers)
        {
            t.mute = _isMute;
        }
        _sfx.mute = _isMute;
    }

    public void SetVolumeMusic(float volume)
    {
        _volumeMusic = volume;
        foreach (var t in _layers)
        {
            t.volume = _volumeMusic;
        }
        PlayerPrefs.SetFloat("VolumeMusic", _volumeMusic);
    }

    public void SetVolumeSfx(float volume)
    {
        _volumeSfx = volume;
        _sfx.volume = _volumeSfx;
        PlayerPrefs.SetFloat("VolumeSfx", _volumeSfx);
    }

    protected override bool Validation()
    {
        return FindObjectsOfType<MusicAndSfxService>().Length > 1;
    }

    protected override void RegisterService()
    {
        ServiceLocator.Instance.RegisterService<IMusicAndSfxService>(this);
    }

    protected override void RemoveService()
    {
        ServiceLocator.Instance.RemoveService<IMusicAndSfxService>();
    }
}