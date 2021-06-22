using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundGameManager : MonoBehaviour
{
    [SerializeField]
    List<_2DSoundInstance> _2DSounds;

    [SerializeField]
    List<_3DSoundInstance> _3DSounds;

    [System.Serializable]
    public class _2DSoundInstance
    {
        public string SoundName;
        [FMODUnity.EventRef]
        public string fmodSoundEvent;
        public FMOD.Studio.EventInstance Sound;
        [Range(0, 1)]
        public float Adjust=1;

        public void ChangeParameter()
        {
            Sound.setParameterByName("Minigame adjust", Adjust);
        }
    }

    [System.Serializable]
    public class _3DSoundInstance
    {
        public string SoundName;
        public FMODUnity.StudioEventEmitter Sound;
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _2DSounds.Count; i++)
        {
            _2DSounds[i].Sound = FMODUnity.RuntimeManager.CreateInstance(_2DSounds[i].fmodSoundEvent);
        }
        EventQueue.eventQueue.Subscribe(EventType.PLAYMINIGAMESOUND, OnPlayMinigameSound);
        EventQueue.eventQueue.Subscribe(EventType.PLAYTUTORIALSOUND, OnPlayTutorialSound);
        EventQueue.eventQueue.Subscribe(EventType.PLAYSCOREINCREASESOUND, OnPlayScoreInscreaseSound);
        EventQueue.eventQueue.Subscribe(EventType.PLAYSPRAYPARTICLESSOUND, OnPlaySprayParticlesSound);
    }

    public void OnPlayTutorialSound(EventData eventData)
    {
        if (eventData is PlayTutorialSoundEventData)
        {
            Debug.Log("play tutorial popup sound");
            Play2DSound("TutorialPopupSound");
        }
    }

    public void OnPlaySprayParticlesSound(EventData eventData)
    {
        if (eventData is PlaySprayParticlesSoundEventData)
        {
            Debug.Log("play spray bottle sound");
            Play3DSound("SprayBottleSound");
        }
    }

    public void OnPlayScoreInscreaseSound(EventData eventData)
    {
        if (eventData is PlayScoreIncreaseSoundEventData)
        {
            Debug.Log("play score increase sound");
            Play2DSound("ScoreInscreaseSound");
        }
    }

    public void OnPlayMinigameSound(EventData eventData)
    {
        if (eventData is PlayMinigameSoundEventData)
        {
            PlayMinigameSoundEventData e = eventData as PlayMinigameSoundEventData;
            switch (e.type)
            {
                case MinigameSounds.Start:
                    Debug.Log("play start sound");
                    Play2DSound("StartMinigameSound");
                    break;
                case MinigameSounds.Lose:
                    Debug.Log("play lose sound");
                    Play2DSound("LoseMinigameSound");
                    break;
                case MinigameSounds.Win:
                    Debug.Log("play win sound");
                    Play2DSound("WinMinigameSound");
                    break;
                case MinigameSounds.Pass:
                    Debug.Log("play pass sound");
                    Play2DSound("PassMinigameSound");
                    break;
            }
        }
    }
    private void Play2DSound(string soundName)
    {

        for (int i = 0; i < _2DSounds.Count; i++)
        {
            if (_2DSounds[i].SoundName == soundName)
            {
                FMOD.Studio.PLAYBACK_STATE state;
                _2DSounds[i].Sound.getPlaybackState(out state);
                _2DSounds[i].ChangeParameter();

                if (state != FMOD.Studio.PLAYBACK_STATE.PLAYING)
                {
                    _2DSounds[i].Sound.start();
                }
                break;
            }
        }
    }

    private void Play3DSound(string soundName)
    {

        for (int i = 0; i < _3DSounds.Count; i++)
        {
            if (_3DSounds[i].SoundName == soundName)
            {
                Debug.Log("play  sound");
                //sounds[i].ChangeParameter();
                _3DSounds[i].Sound.Play();
                break;
            }
        }
    }
}
