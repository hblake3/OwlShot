using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    // Singleton instance
    public static SoundManager Instance { get; private set; }

    // Audio source components for different types of audio
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    // Keep track of mute state
    private bool isMuted = false;

    // Store volume levels to restore when unmuting
    private float musicVolumeBeforeMute;
    private float sfxVolumeBeforeMute;

    private void Start()
    {

    }

    // Called when the script instance is being loaded
    private void Awake()
    {
        // Singleton pattern implementation
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Make this object persistent between scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    // Method to play a sound effect
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
            sfxSource.PlayOneShot(clip);
    }

    // Method to play music
    public void PlayMusic(AudioClip clip)
    {
        if (clip != null)
        {
            musicSource.clip = clip;
            musicSource.Play();
        }
    }

    // Example method to stop music
    public void StopMusic()
    {
        musicSource.Stop();
    }

    // Toggle mute status for all audio
    public void ToggleMute()
    {
        if (isMuted)
            UnmuteAll();
        else
            MuteAll();
    }

    // Mute all audio
    public void MuteAll()
    {
        if (!isMuted)
        {
            // Store current volumes before muting
            musicVolumeBeforeMute = musicSource.volume;
            sfxVolumeBeforeMute = sfxSource.volume;

            // Set volumes to 0
            musicSource.volume = 0f;
            sfxSource.volume = 0f;

            isMuted = true;
        }
    }

    // Unmute all audio and restore previous volumes
    public void UnmuteAll()
    {
        if (isMuted)
        {
            // Restore previous volumes
            musicSource.volume = musicVolumeBeforeMute;
            sfxSource.volume = sfxVolumeBeforeMute;

            isMuted = false;
        }
    }

    // Check if audio is currently muted
    public bool IsMuted()
    {
        return isMuted;
    }
}