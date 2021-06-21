using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundGameManager : MonoBehaviour
{
    //public string soundLose;
    //public string soundPass;
    //public string soundWin;
    //public string soundStart;
    //FMOD.Studio.EventInstance QTESoundLose;
    //FMOD.Studio.EventInstance QTESoundPass;
    //FMOD.Studio.EventInstance QTESoundWin;
    //FMOD.Studio.EventInstance QTESoundStart;
    [SerializeField]
    List<SoundInstance> sounds;

    [System.Serializable]
    public class SoundInstance
    {
        public string SoundName;
        [FMODUnity.EventRef]
        public string fmodSoundEvent;
        public FMOD.Studio.EventInstance Sound;
        [Range(0,1)]
        public float soundAdjust;

        public void ChangeParameter()
        {
            Sound.setParameterByName("Minigame adjust", soundAdjust);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < sounds.Count; i++)
        {
            sounds[i].Sound = FMODUnity.RuntimeManager.CreateInstance(sounds[i].fmodSoundEvent);
        }
        // QTESoundLose = FMODUnity.RuntimeManager.CreateInstance("event:/" + soundLose);
        //QTESoundPass = FMODUnity.RuntimeManager.CreateInstance("event:/" + soundPass);
        // QTESoundWin = FMODUnity.RuntimeManager.CreateInstance("event:/" + soundWin);
        //QTESoundStart = FMODUnity.RuntimeManager.CreateInstance("event:/" + soundStart);
    }

    public void PlayStartMinigameSound()
    {
        Debug.Log("play start sound");
        PlaySound("StartMinigameSound");
    }
    public void PlayLoseMinigameSound()
    {
        Debug.Log("play lose sound");
        PlaySound("LoseMinigameSound");
    }

    public void PlayPassMinigameSound()
    {
        Debug.Log("play pass sound");
        PlaySound("PassMinigameSound");
    }

    public void PlayWinMinigameSound()
    {
        Debug.Log("play win sound");
        PlaySound("WinMinigameSound");
    }
    private void PlaySound(string soundName)
    {

        for (int i = 0; i < sounds.Count; i++)
        {
            if (sounds[i].SoundName == soundName)
            {
                FMOD.Studio.PLAYBACK_STATE state;
                sounds[i].Sound.getPlaybackState(out state);
                sounds[i].ChangeParameter();

                if (state != FMOD.Studio.PLAYBACK_STATE.PLAYING)
                {
                    sounds[i].Sound.start();
                }
                break;
            }
        }
    }
}
