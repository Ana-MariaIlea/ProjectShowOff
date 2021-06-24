using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class GameEndScript : MonoBehaviour
{
    [SerializeField]
    VideoClip englishCutscene;
    [SerializeField]
    VideoClip dutchCutscene;
    VideoPlayer player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<VideoPlayer>();
        switch (LocalisationSystem.language)
        {
            case LocalisationSystem.Language.English:
                player.clip = englishCutscene;
                break;
            case LocalisationSystem.Language.Dutch:
                player.clip = dutchCutscene;
                break;
        }
    }

    public void PlayCutscene()
    {
        player.Play();
        
    }
}
