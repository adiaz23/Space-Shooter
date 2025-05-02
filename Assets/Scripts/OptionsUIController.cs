using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsUIController : MonoBehaviour
{
  
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider[] sliders;

    private void Start(){
        if(PlayerPrefs.HasKey("musicVolume")){
            LoadVolume();  
        } else{
           SetMusicVolume();
           SetSFXVolume();
        }
    }

    public void SetMusicVolume(){
        float volume = sliders[0].value;
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSFXVolume(){
         float volume = sliders[1].value;
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("sFXVolume", volume);
    }

    private void LoadVolume(){
        sliders[0].value = PlayerPrefs.GetFloat("musicVolume");
        sliders[1].value = PlayerPrefs.GetFloat("sFXVolume");
    }
}
