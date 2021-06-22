using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound3DTest : MonoBehaviour
{

    [Tooltip("Gameobject for the sound to be attached if the sound is 3D")]
    public GameObject attachment;
    [FMODUnity.EventRef]
    public string fmodSoundEvent;
    public FMOD.Studio.EventInstance Sound; 
    // Start is called before the first frame update
    void Start()
    {
        Sound = FMODUnity.RuntimeManager.CreateInstance(fmodSoundEvent);
    }

    // Update is called once per frame
    void Update()
    {
        Sound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(attachment));

        if (Input.GetKeyDown(KeyCode.O))
        {
           
            //FMOD.Studio.PLAYBACK_STATE state;
           // Sound.getPlaybackState(out state);

            //if (state != FMOD.Studio.PLAYBACK_STATE.PLAYING)
           //{
                Debug.Log("play sound");
                Sound.start();
           // }

        }
    }
}
