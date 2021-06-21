using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOptions : MonoBehaviour
{
   // public string nameTestSoundSFX;
    FMOD.Studio.Bus Music;
    FMOD.Studio.Bus Sound;
    [FMODUnity.EventRef]
    public string fmodSoundEvent;
    FMOD.Studio.EventInstance SFXTestSound;

    private void Awake()
    {
        Music = FMODUnity.RuntimeManager.GetBus("bus:/Master/Background music"); 
        Sound = FMODUnity.RuntimeManager.GetBus("bus:/Master/SFX");
        SFXTestSound = FMODUnity.RuntimeManager.CreateInstance(fmodSoundEvent);
        Debug.Log(Music);
        Debug.Log(Sound);
    }
    public void SetMusicVolume(float value)
    {
        Music.setVolume(value);
    }

    public void SetSoundVolume(float value)
    {
        Sound.setVolume(value);

        FMOD.Studio.PLAYBACK_STATE state;
        SFXTestSound.getPlaybackState(out state);
        if (state != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            SFXTestSound.start();
        }
    }
}
