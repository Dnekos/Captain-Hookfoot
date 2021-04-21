using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    AudioSource sfxSource, musicSource;

    [SerializeField]
    AudioClip MenuMusic, LevelMusic;

    [SerializeField]
    AudioClip ButtonSFX, PickupSFX;

    private void Awake()
    {
//        SceneManager.activeSceneChanged += ChangedActiveScene;
    }

    public void PlayPickupSFX()
    {
        //if (!Application.isEditor)
        sfxSource.PlayOneShot(PickupSFX);
    }

    public void PlayButtonSFX()
    {
        //if (!Application.isEditor)
        sfxSource.PlayOneShot(ButtonSFX);
    }


    private void ChangedActiveScene(Scene current, Scene next)
    {

        switch (next.buildIndex)
        {
            case 0: // main menu
            case 1: // controls
            case 3: // credits
                if (!(musicSource.isPlaying && musicSource.clip == MenuMusic))
                {
                    musicSource.clip = MenuMusic;
                    musicSource.Play();
                }
                break;
            case 2: // main game
                musicSource.clip = LevelMusic;
                musicSource.Play();
                break;
        };

    }
}
