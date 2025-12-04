using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public Button newGameButton;
    public Slider musicSlider;
    public Slider sfxSlider;
    public AudioMixer audioMixer;
    public AudioSource backgroundMusicSource; 

    void Start()
    {
        newGameButton.onClick.AddListener(StartNewGame);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        float savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
        
        musicSlider.value = savedMusicVolume;
        sfxSlider.value = savedSFXVolume;
        SetMusicVolume(savedMusicVolume);
        SetSFXVolume(savedSFXVolume);

        if (backgroundMusicSource != null && !backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.Play();
        }
    }

    void StartNewGame()
    {
        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.Stop(); 
        }

        SceneManager.LoadScene("SampleScene"); 
    }

    void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
}