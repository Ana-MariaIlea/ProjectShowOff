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
        Debug.Log("play lose sound");
        PlaySound(QTESoundLose);
    }

    public void PlayPassSound()
    {
        Debug.Log("play pass sound");
        PlaySound(QTESoundPass);
    }

    public void PlayWinSound()
    {
        Debug.Log("play win sound");
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
