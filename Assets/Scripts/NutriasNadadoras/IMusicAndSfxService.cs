using UnityEngine;

public interface IMusicAndSfxService
{
    void PlayMusic();
    void PlaySfx(AudioClip sfx);
    void Mute();
    void SetVolumeMusic(float volume);
    void SetVolumeSfx(float volume);
}