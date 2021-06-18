using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTESounds : MonoBehaviour
{
    public string soundLose;
    public string soundPass;
    public string soundWin;
    FMOD.Studio.EventInstance QTESoundLose;
    FMOD.Studio.EventInstance QTESoundPass;
    FMOD.Studio.EventInstance QTESoundWin;
    // Start is called before the first frame update
    void Start()
    {
        QTESoundLose = FMODUnity.RuntimeManager.CreateInstance("event:/" + soundLose);
        QTESoundPass = FMODUnity.RuntimeManager.CreateInstance("event:/" + soundPass);
        QTESoundWin = FMODUnity.RuntimeManager.CreateInstance("event:/" + soundWin);
    }


    public void PlayLoseSound()
    {
        PlaySound(QTESoundLose);
    }

    public void PlayPassSound()
    {
        PlaySound(QTESoundPass);
    }

    public void PlayWinSound()
    {
        PlaySound(QTESoundWin);
    }
    private void PlaySound(FMOD.Studio.EventInstance Sound)
    {
        FMOD.Studio.PLAYBACK_STATE state;
        Sound.getPlaybackState(out state);
        if (state != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            Sound.start();
        }
    }
}
