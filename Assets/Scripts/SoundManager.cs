using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum Sound // Sound enu names MUST MATCH file under Resourses/Sounds, use CTR+R to rename if on VS
{
    ButtonHover, // done on clickables
    TextScroll, // added to scroll
    Pickup, // added to pickup interact
    LockedInteract,
    Drawer, // added to lock update
    Rotary, // added to rotor click
    Explosion,
    ItemCombining,
    Anchor // added to event
}
public class SoundManager : MonoBehaviour
{
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
        //        SceneManager.activeSceneChanged += ChangedActiveScene;
    }

    public void PlayPickupSFX()
    {
        //if (!Application.isEditor)
        //sfxSource.PlayOneShot(PickupSFX);
    }

    public void PlaySoundSFX(Sound sound)
    {
        if (!Application.isEditor)
            sfxSource.PlayOneShot(Resources.Load<AudioClip>("Sounds/"+ Sound.ButtonHover));
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
                if (!(musicSource.isPlaying && musicSource.clip == MenuMusic))
                {
                    musicSource.clip = LevelMusic;
                    musicSource.Play();

                    ambientSource.clip = Exterior;
                    ambientSource.Play();

                }
                break;

            default: // interior rooms
                if (!(musicSource.isPlaying && musicSource.clip == MenuMusic))
                {
                    musicSource.clip = LevelMusic;
                    musicSource.Play();
                    ambientSource.clip = Cave;
                    ambientSource.Play();
                }
                break;
        };

    }
}
