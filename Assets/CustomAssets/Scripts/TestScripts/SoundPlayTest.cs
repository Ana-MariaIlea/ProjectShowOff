using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayTest : MonoBehaviour
{
    public string name;
    FMOD.Studio.EventInstance TestSound;

    private void Awake()
    {
        TestSound = FMODUnity.RuntimeManager.CreateInstance("event:/"+name);
    }
    public void PlaySound()
    {
        FMOD.Studio.PLAYBACK_STATE state;
        TestSound.getPlaybackState(out state);
        if(state!= FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            TestSound.start();
        }
    }

}
