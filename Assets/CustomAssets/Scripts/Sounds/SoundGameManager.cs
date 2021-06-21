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
        public float Adjust;

        public void ChangeParameter()
        {
            Sound.setParameterByName("Minigame adjust", Adjust);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < sounds.Count; i++)
        {
            sounds[i].Sound = FMODUnity.RuntimeManager.CreateInstance(sounds[i].fmodSoundEvent);
        }
        EventQueue.eventQueue.Subscribe(EventType.PLAYMINIGAMESOUND, OnPlayMinigameSound);
    }

    public void OnPlayMinigameSound(EventData eventData)
    {
        if(eventData is PlayMinigameSoundEventData)
        {
            PlayMinigameSoundEventData e = eventData as PlayMinigameSoundEventData;
            switch (e.type)
            {
                case MinigameSounds.Start:
                    Debug.Log("play start sound");
                    PlaySound("StartMinigameSound");
                    break;
                case MinigameSounds.Lose:
                    Debug.Log("play lose sound");
                    PlaySound("LoseMinigameSound");
                    break;
                case MinigameSounds.Win:
                    Debug.Log("play win sound");
                    PlaySound("WinMinigameSound");
                    break;
                case MinigameSounds.Pass:
                    Debug.Log("play pass sound");
                    PlaySound("PassMinigameSound");
                    break;
            }
        }
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