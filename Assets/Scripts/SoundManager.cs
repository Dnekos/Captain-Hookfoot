using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum Sound // Sound enu names MUST MATCH file under Resourses/Sounds, use CTR+R to rename if on VS
{
    ButtonHover, // done on clickables
    TextScroll, // added to scroll
    Pickup, // added to pickup interact
    LockedInteract, // added to unlocking the lock by well
    Drawer, // added to lock update
    Rotary, // added to rotor click
    Explosion, // added to explosion event
    ItemCombining, // added to combining code
    Anchor // added to event
}
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField]
    AudioSource sfxSource, musicSource, ambientSource;

    [Header("Music")]
    [SerializeField]
    AudioClip MenuMusic;
    [SerializeField]
    AudioClip LevelMusic;

    [Header("Ambience")]
    [SerializeField]
    AudioClip Exterior;
    [SerializeField]
    AudioClip Cave;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        if (this != instance)
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.activeSceneChanged += instance.ChangedActiveScene;
    }


    public static void PlaySound(Sound sound)
    {
        instance.PlaySoundSFX(sound);
    }

    private void PlaySoundSFX(Sound sound)
    {
        Debug.Log("playing Sounds/" + sound);
        //if (!Application.isEditor)
            sfxSource.PlayOneShot(Resources.Load<AudioClip>("Sounds/"+ sound));
    }

    private void ChangedActiveScene(Scene current, Scene next)
    {

        switch (next.buildIndex)
        {
            case 0: // main menu
            case 2: // controls
            case 1: // credits
                if (!(musicSource.isPlaying && musicSource.clip == MenuMusic))
                {
                    musicSource.clip = MenuMusic;
                    musicSource.Play();
                    ambientSource.Stop();
                }
                break;
            case 4: // beach
            case 5: // ship
                if (!(musicSource.isPlaying && musicSource.clip == LevelMusic))
                {
                    musicSource.clip = LevelMusic;
                    musicSource.Play();
                }
                if (!(ambientSource.isPlaying && ambientSource.clip == Exterior))
                {
                    ambientSource.clip = Exterior;
                    ambientSource.Play();
                }
                break;

            default: // interior rooms
                if (!(musicSource.isPlaying && musicSource.clip == LevelMusic))
                {
                    musicSource.clip = LevelMusic;
                    musicSource.Play();
                }
                if (!(ambientSource.isPlaying && ambientSource.clip == Cave))
                {
                    ambientSource.clip = Cave;
                    ambientSource.Play();
                }
                break;
        };

    }
}
