using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOptions : MonoBehaviour
{

    FMOD.Studio.Bus Music;
    FMOD.Studio.Bus Sound;

    private void Awake()
    {
        Music = FMODUnity.RuntimeManager.GetBus("bus:/Master/Music"); // Bus lication and name is not determined - NEEDS CHANGE
        Sound = FMODUnity.RuntimeManager.GetBus("bus:/Master/Sound");
    }
    public void SetMusicVolume(float value)
    {
        Music.setVolume(value);
    }

    public void SetSoundVolume(float value)
    {
        Sound.setVolume(value);
    }
}
