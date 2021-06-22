using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundGameManager : MonoBehaviour
{
    [SerializeField]
    List<SoundInstance> sounds;

    [System.Serializable]
    public class SoundInstance
    {
        [System.Serializable]
        public enum SoundType
        {
            _2D,
            _3D
        }
        public string SoundName;
        public SoundType type;
        [Tooltip("Gameobject for the sound to be attached if the sound is 3D")]
        public GameObject attachment;
        [FMODUnity.EventRef]
        public string fmodSoundEvent;
        public FMOD.Studio.EventInstance Sound;
        [Range(0, 1)]
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
        EventQueue.eventQueue.Subscribe(EventType.PLAYTUTORIALSOUND, OnPlayTutorialSound);
        EventQueue.eventQueue.Subscribe(EventType.PLAYSCOREINCREASESOUND, OnPlayScoreInscreaseSound);
        EventQueue.eventQueue.Subscribe(EventType.PLAYSPRAYPARTICLESSOUND, OnPlaySprayParticlesSound);
    }

    private void Update()
    {
        for (int i = 0; i < sounds.Count; i++)
        {
            if (sounds[i].type == SoundInstance.SoundType._3D && sounds[i].attachment != null)
            {
                Debug.Log(sounds[i].SoundName+" attach 3d atributes");
                sounds[i].Sound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(sounds[i].attachment));
            }
        }
    }

    public void OnPlayTutorialSound(EventData eventData)
    {
        if (eventData is PlayTutorialSoundEventData)
        {
            Debug.Log("play tutorial popup sound");
            PlaySoundWhileCheckingPlayBackState("TutorialPopupSound");
        }
    }

    public void OnPlaySprayParticlesSound(EventData eventData)
    {
        if (eventData is PlaySprayParticlesSoundEventData)
        {
            Debug.Log("play spray bottle sound");
            PlaySoundWithoutCheckingPlayBackState("SprayBottleSound");
        }
    }

    public void OnPlayScoreInscreaseSound(EventData eventData)
    {
        if (eventData is PlayScoreIncreaseSoundEventData)
        {
            Debug.Log("play score increase sound");
            PlaySoundWhileCheckingPlayBackState("ScoreInscreaseSound");
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
                    PlaySoundWhileCheckingPlayBackState("StartMinigameSound");
                    break;
                case MinigameSounds.Lose:
                    Debug.Log("play lose sound");
                    PlaySoundWhileCheckingPlayBackState("LoseMinigameSound");
                    break;
                case MinigameSounds.Win:
                    Debug.Log("play win sound");
                    PlaySoundWhileCheckingPlayBackState("WinMinigameSound");
                    break;
                case MinigameSounds.Pass:
                    Debug.Log("play pass sound");
                    PlaySoundWhileCheckingPlayBackState("PassMinigameSound");
                    break;
            }
        }
    }
    private void PlaySoundWhileCheckingPlayBackState(string soundName)
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

    private void PlaySoundWithoutCheckingPlayBackState(string soundName)
    {

        for (int i = 0; i < sounds.Count; i++)
        {
            if (sounds[i].SoundName == soundName)
            {
                Debug.Log("play  sound");
                //sounds[i].ChangeParameter();
                sounds[i].Sound.start();
                break;
            }
        }
    }
}
