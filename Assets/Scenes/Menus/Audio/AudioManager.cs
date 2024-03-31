using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public AudioSource backgroundMusic;
    public AudioSource menuSoundsAudioSource; // Separate audio source for menu sounds
    public AudioClip buttonHoverSound;
    public AudioClip buttonClickSound;
    private Slider backgroundMusicVolumeSlider; // Reference to background music volume slider
    private Slider menuSoundsVolumeSlider; // Reference to menu sounds volume slider

    void Awake()
    {
        // Ensure only one instance of AudioManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Prevent AudioManager from being destroyed on scene change
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates
        }
    }

    void Start()
    {
        // Start playing background music when the first scene starts
        PlayBackgroundMusic();

        // Find and assign the sliders dynamically
        backgroundMusicVolumeSlider = GameObject.FindWithTag("BackgroundMusicSlider").GetComponent<Slider>();
        menuSoundsVolumeSlider = GameObject.FindWithTag("MenuSoundsSlider").GetComponent<Slider>();

        // Initialize background music volume slider
        if (backgroundMusicVolumeSlider != null)
        {
            backgroundMusicVolumeSlider.value = backgroundMusic.volume;
            backgroundMusicVolumeSlider.onValueChanged.AddListener(OnBackgroundMusicVolumeChanged);
        }

        // Initialize menu sounds volume slider
        if (menuSoundsVolumeSlider != null)
        {
            float initialMenuSoundsVolume = PlayerPrefs.GetFloat("MenuSoundsVolume", 0.5f); // Default volume if not set
            menuSoundsVolumeSlider.value = initialMenuSoundsVolume;
            menuSoundsVolumeSlider.onValueChanged.AddListener(OnMenuSoundsVolumeChanged);
        }
    }

    void PlayBackgroundMusic()
    {
        if (!backgroundMusic.isPlaying)
        {
            backgroundMusic.Play();
        }
    }

    void OnBackgroundMusicVolumeChanged(float volume)
    {
        backgroundMusic.volume = volume;
    }

    void OnMenuSoundsVolumeChanged(float volume)
    {
        // Save menu sounds volume to PlayerPrefs for future sessions
        PlayerPrefs.SetFloat("MenuSoundsVolume", volume);

        // Apply volume to menu sounds
        menuSoundsAudioSource.volume = volume;
    }

    public void PlayButtonHoverSound()
    {
        if (buttonHoverSound != null)
        {
            menuSoundsAudioSource.PlayOneShot(buttonHoverSound);
        }
    }

    public void PlayButtonClickSound()
    {
        if (buttonClickSound != null)
        {
            menuSoundsAudioSource.PlayOneShot(buttonClickSound);
        }
    }
}