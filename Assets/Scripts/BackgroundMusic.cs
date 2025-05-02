using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] private BackgroundMusic bgMusic;
    [SerializeField] private AudioMixer audioMixer;

    void Awake(){
        if(FindObjectsByType<BackgroundMusic>(FindObjectsSortMode.None).Length > 1)
           Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        float savedMusicVolume = PlayerPrefs.GetFloat("musicVolume", 1f);
        float savedSFXVolume = PlayerPrefs.GetFloat("sFXVolume", 1f);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(savedMusicVolume) * 20);
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(savedSFXVolume) * 20);    
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Game")
           Destroy(gameObject);
    }
}
